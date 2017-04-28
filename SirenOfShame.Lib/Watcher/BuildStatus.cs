using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SirenOfShame.Lib.Helpers;
using log4net;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Watcher
{
    public enum BallsEnum
    {
        Red = 0,
        Green = 1,
        Gray = 2,
        Triangle = 3
    }

    public class BuildStatus
    {
        public DateTime? StartedTime { get; set; }
        public DateTime? FinishedTime { get; set; }
        public string RequestedBy { get; set; }
        public DateTime LocalStartTime { get; set; }
        public string BuildDefinitionId { get; set; }
        public string Name { get; set; }
        public string BuildId { get; set; }
        public string Url { get; set; }
        public BuildStatusEnum? CurrentBuildStatus { get; set; }
        public string BuildStatusMessage { get; set; }
        public string Comment { get; set; }

        public BuildStatus()
        {
            LocalStartTime = DateTime.Now;
            CurrentBuildStatus = null;
        }

        public static BuildStatus Parse(string[] lineFromSosDb, string buildDefinitionId)
        {
            if (lineFromSosDb.Length != 4)
            {
                _log.Error("SosDb line was not parsable: " + lineFromSosDb);
                return null;
            }

            string startedTimeStr = lineFromSosDb[0];
            string finishedTimeStr = lineFromSosDb[1];
            string buildStatusStr = lineFromSosDb[2];
            string requestedByStr = lineFromSosDb[3];

            try
            {
                return new BuildStatus
                {
                    StartedTime = string.IsNullOrEmpty(startedTimeStr) ? (DateTime?)null : new DateTime(long.Parse(startedTimeStr)),
                    FinishedTime = string.IsNullOrEmpty(finishedTimeStr) ? (DateTime?)null : new DateTime(long.Parse(finishedTimeStr)),
                    CurrentBuildStatus = (BuildStatusEnum)int.Parse(buildStatusStr),
                    RequestedBy = requestedByStr,
                    BuildDefinitionId = buildDefinitionId
                };
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Error parsing a line in SosDb: {0}, {1}, {2}, {3}", startedTimeStr, finishedTimeStr, buildStatusStr, requestedByStr), ex);
                return null;
            }
        }

        private static readonly ILog _log = MyLogManager.GetLogger(typeof(BuildStatus));

        private static string BuildStatusToString(BuildStatusEnum? buildStatus)
        {
            if (buildStatus.HasValue)
            {
                switch (buildStatus)
                {
                    case BuildStatusEnum.Broken: return "Broken";
                    case BuildStatusEnum.Working: return "Passing";
                    case BuildStatusEnum.Unknown: return "Unknown";
                    case BuildStatusEnum.InProgress: return "In Progress";
                    default: throw new Exception("Unknown Status " + buildStatus);
                }
            }

            return "unknown";
        }

        public string BuildStatusDescription
        {
            get { return BuildStatusToString(CurrentBuildStatus); }
        }

        public bool IsWorkingOrBroken()
        {
            if (CurrentBuildStatus.HasValue)
                return CurrentBuildStatus == BuildStatusEnum.Working || CurrentBuildStatus == BuildStatusEnum.Broken;

            return false;
        }

        private BallsEnum BallIndex
        {
            get
            {
                if (CurrentBuildStatus.HasValue)
                {
                    if (CurrentBuildStatus == BuildStatusEnum.Working) return BallsEnum.Green;
                    if (CurrentBuildStatus == BuildStatusEnum.Broken) return BallsEnum.Red;
                    if (CurrentBuildStatus == BuildStatusEnum.Unknown) return BallsEnum.Triangle;
                }

                return BallsEnum.Gray;
            }
        }

        public BuildStatusDto AsBuildStatusDto(DateTime now, IDictionary<string, BuildStatus> previousWorkingOrBrokenBuildStatus, SirenOfShameSettings settings)
        {
            BuildStatus previousStatus;
            bool previousStatusExists = previousWorkingOrBrokenBuildStatus.TryGetValue(BuildDefinitionId, out previousStatus);

            string duration = GetDurationAsString(FinishedTime, StartedTime, now, previousStatus);

            var buildDisplayName = GetBuildDisplayName(settings, Name);

            var result = new BuildStatusDto
            {
                BuildStatusEnum = CurrentBuildStatus.Value,
                BuildStatusMessage = BuildStatusMessage,
                ImageIndex = (int)BallIndex,
                StartTimeShort = FormatAsDayMonthTime(StartedTime),
                LocalStartTime = !previousStatusExists && StartedTime.HasValue ? StartedTime.Value : LocalStartTime,
                Duration = duration,
                RequestedByRawName = RequestedBy,
                Comment = Comment,
                BuildId = BuildId ?? "",
                BuildDefinitionId = BuildDefinitionId,
                BuildDefinitionDisplayName = buildDisplayName,
                Url = Url,
            };
            result.SetDisplayName(settings);
            return result;
        }

        private string GetBuildDisplayName(SirenOfShameSettings settings, string buildDefinitionName)
        {
            if (!settings.AnyDuplicateBuildNames) return buildDefinitionName;
            
            var buildDisplayName = buildDefinitionName;

            var buildDefinitionSetting = settings
                    .CiEntryPointSettings
                    .FirstOrDefault(i => i.BuildDefinitionSettings.Any(j => j.Id == BuildDefinitionId));

            if (buildDefinitionSetting != null)
            {
                var buildDefUrlWithoutHttpPrefix = StringHelpers.RemoveUrlPrefix(buildDefinitionSetting.Url);
                buildDisplayName += " (" + buildDefUrlWithoutHttpPrefix + ")";
            }
            return buildDisplayName;
        }

        internal static string FormatAsDayMonthTime(DateTime? nullableDate)
        {
            if (nullableDate == null) return null;
            DateTime date = nullableDate.Value;
            var dayMonthPattern = GetDayMonthPattern();
            return date.ToString(dayMonthPattern + " h:mm tt");
        }

        private static string GetDayMonthPattern()
        {
            DateTimeFormatInfo dateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo;
            if (dateTimeFormatInfo == null) return "M/d";
            var shortDatePattern = dateTimeFormatInfo.ShortDatePattern;
            var dateSeparator = dateTimeFormatInfo.DateSeparator;
            string dayMonthPattern = shortDatePattern.TrimEnd(new[] { 'y', 'Y', dateSeparator[0] });
            return dayMonthPattern;
        }

        private string GetDurationAsString(DateTime? finishedTime, DateTime? startedTime, DateTime now, BuildStatus previousStatus)
        {
            TimeSpan? duration = GetDuration(startedTime, finishedTime, previousStatus, now);
            if (duration == null) return "";
            if (duration.Value.Ticks < 0) return string.Format("OT: {0}:{1:00}", 0 - (int)duration.Value.TotalMinutes, 0 - duration.Value.Seconds);
            return string.Format("{0}:{1:00}", (int)duration.Value.TotalMinutes, duration.Value.Seconds);
        }

        public TimeSpan? GetDuration()
        {
            if (FinishedTime == null || StartedTime == null) return null;
            return FinishedTime.Value - StartedTime.Value;
        }

        private TimeSpan? GetDuration(DateTime? startedTime, DateTime? finishedTime, BuildStatus previousStatus, DateTime now)
        {
            if (CurrentBuildStatus.HasValue)
            {
                if (CurrentBuildStatus != BuildStatusEnum.InProgress)
                {
                    if (startedTime == null || finishedTime == null)
                    {
                        _log.Warn("Start time or stop time was null for " + BuildDefinitionId + ", and the build was not in progress, this should only happen at startup");
                        return null;
                    }
                    return finishedTime.Value - startedTime.Value;
                }

                if (previousStatus == null || previousStatus.StartedTime == null || previousStatus.FinishedTime == null)
                {
                    // count up
                    return now - LocalStartTime;
                }

                // count down
                var previousDuration = previousStatus.FinishedTime.Value - previousStatus.StartedTime.Value;
                var currentDuration = now - LocalStartTime;
                return previousDuration - currentDuration;
            }

            return null;
        }

        public void FireApplicableRulesEngineEvents(BuildStatusEnum? previousWorkingOrBrokenStatus, BuildStatusEnum? previousStatus, RulesEngine rulesEngine, List<Rule> rules)
        {
            var rule = rules
                .Where(r => r.IsMatch(this, previousWorkingOrBrokenStatus))
                .OrderByDescending(r => r.PriorityId)
                .FirstOrDefault();

            if (rule != null)
                rule.FireEvent(rulesEngine, previousWorkingOrBrokenStatus, this);

            rules.ForEach(r => r.FireAnyUntilBuildPassesEvents(rulesEngine, this, previousStatus));
        }

        public bool IsNewlyBroken(BuildStatusEnum? previousStatus)
        {
            if(previousStatus.HasValue)
                return CurrentBuildStatus == BuildStatusEnum.Broken && (previousStatus == BuildStatusEnum.Working);

            return false;
        }

        public bool IsNewlyFixed(BuildStatusEnum? previousStatus)
        {
            if(previousStatus.HasValue)
                return CurrentBuildStatus == BuildStatusEnum.Working && previousStatus == BuildStatusEnum.Broken;

            return false;
        }

        public bool IsBackToBackWithNextBuild(BuildStatus nextBuild)
        {
            const int defaultSecondsForBackToBack = 10;
            return IsBackToBackWithNextBuild(nextBuild, defaultSecondsForBackToBack);
        }

        public bool IsBackToBackWithNextBuild(BuildStatus nextBuild, int seconds)
        {
            if (nextBuild.StartedTime == null || FinishedTime == null) return false;
            double secondsBetweenBuilds = (nextBuild.StartedTime.Value - FinishedTime.Value).TotalSeconds;
            return secondsBetweenBuilds > 0 && secondsBetweenBuilds < seconds;
        }

        public string AsSosOnlineExport()
        {
            var fieldsToExport = new[]
            {
                DateAsExport(StartedTime), 
                DateAsExport(FinishedTime),
                BuildStatusAsExport(CurrentBuildStatus)
            };
            return string.Join(",", fieldsToExport);
        }

        private string BuildStatusAsExport(BuildStatusEnum? status)
        {
            if(status.HasValue)
                return status == BuildStatusEnum.Working ? "1" : "0";

            return "0";
        }

        private static string DateAsExport(DateTime? dateTime)
        {
            return dateTime == null ? "" : dateTime.Value.Ticks.ToString(CultureInfo.InvariantCulture);
        }

        public string GetBuildDataAsHash()
        {
            return string.Format("{0}-{1}-{2}", BuildDefinitionId, BuildId, RequestedBy);
        }

        public NewNewsItemEventArgs AsNewsItemEventArgs(BuildStatusEnum previousWorkingOrBrokenBuildStatus, SirenOfShameSettings settings)
        {
            var person = settings.FindAddPerson(RequestedBy);
            return new NewNewsItemEventArgs
            {
                Person = person,
                EventDate = DateTime.Now,
                Title = GetNewsItemTitle(previousWorkingOrBrokenBuildStatus),
                BuildDefinitionId = BuildDefinitionId,
                NewsItemType = GetNewsItemType(),
                ReputationChange = GetReputationChange(),
                BuildId = BuildId
            };
        }

        private int? GetReputationChange()
        {
            if (CurrentBuildStatus.HasValue)
            {
                switch (CurrentBuildStatus.Value)
                {
                    case Watcher.BuildStatusEnum.Working: return 1;
                    case Watcher.BuildStatusEnum.Broken: return -4;
                }
            }

            return null;
        }

        private NewsItemTypeEnum GetNewsItemType()
        {
            if (CurrentBuildStatus.HasValue)
            {
                switch (CurrentBuildStatus.Value)
                {
                    case Watcher.BuildStatusEnum.Working: return NewsItemTypeEnum.BuildSuccess;
                    case Watcher.BuildStatusEnum.Broken: return NewsItemTypeEnum.BuildFailed;
                    case Watcher.BuildStatusEnum.InProgress: return NewsItemTypeEnum.BuildStarted;
                }
            }

            return NewsItemTypeEnum.BuildUnknown;
        }

        private string GetNewsItemTitle(BuildStatusEnum previousWorkingOrBrokenBuildStatus)
        {
            var wasBrokenNowWorking = previousWorkingOrBrokenBuildStatus == BuildStatusEnum.Broken && CurrentBuildStatus == BuildStatusEnum.Working;
            var wasBrokenNowBroken = previousWorkingOrBrokenBuildStatus == BuildStatusEnum.Broken && CurrentBuildStatus == BuildStatusEnum.Broken;
            var wasWorkingNowBroken = previousWorkingOrBrokenBuildStatus == BuildStatusEnum.Working && CurrentBuildStatus == BuildStatusEnum.Broken;
            var inProgress = CurrentBuildStatus == BuildStatusEnum.InProgress;

            if (inProgress) return string.Format("'{0}'", Comment);
            if (wasBrokenNowWorking) return string.Format("Fixed the broken build");
            if (wasWorkingNowBroken) return string.Format("Broke the build");
            if (wasBrokenNowBroken) return string.Format("Failed to fix the build");
            if (CurrentBuildStatus == BuildStatusEnum.Working || CurrentBuildStatus == BuildStatusEnum.Unknown) return string.Format("Successful build");

            // some other previous status? this should never happen
            if (CurrentBuildStatus == BuildStatusEnum.Broken) return string.Format("Broke the build");

            throw new Exception("Unknown build status: " + CurrentBuildStatus);
        }
    }
}