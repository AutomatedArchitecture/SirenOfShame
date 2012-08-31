using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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
        public BuildStatus()
        {
            LocalStartTime = DateTime.Now;
        }

        public static BuildStatus Parse(string[] lineFromSosDb)
        {
            Debug.Assert(lineFromSosDb.Length == 4);
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
                    BuildStatusEnum = (BuildStatusEnum)int.Parse(buildStatusStr),
                    RequestedBy = requestedByStr
                };
            } 
            catch (Exception ex)
            {
                _log.Error(string.Format("Error parsing a line in SosDb: {0}, {1}, {2}, {3}", startedTimeStr, finishedTimeStr, buildStatusStr, requestedByStr), ex);
                return null;
            }
        }

        private static readonly ILog _log = MyLogManager.GetLogger(typeof(BuildStatus));

        private static string BuildStatusToString(BuildStatusEnum buildStatus)
        {
            switch (buildStatus)
            {
                case BuildStatusEnum.Broken:
                    return "Broken";
                case BuildStatusEnum.Working:
                    return "Passing";
                case BuildStatusEnum.Unknown:
                    return "Unknown";
                case BuildStatusEnum.InProgress:
                    return "In Progress";
                default:
                    throw new Exception("Unknown Status " + buildStatus);
            }
        }

        public DateTime? StartedTime { get; set; }
        public DateTime? FinishedTime { get; set; }

        public string RequestedBy { get; set; }
        public DateTime LocalStartTime { get; set; }
        public string BuildDefinitionId { get; set; }
        public string Name { get; set; }
        public string BuildId { get; set; }
        public string Url { get; set; }
        public BuildStatusEnum BuildStatusEnum { get; set; }
        public string Comment { get; set; }

        public string BuildStatusDescription
        {
            get { return BuildStatusToString(BuildStatusEnum); }
        }

        public bool IsWorkingOrBroken()
        {
            return BuildStatusEnum == BuildStatusEnum.Working || BuildStatusEnum == BuildStatusEnum.Broken;
        }

        private BallsEnum BallIndex
        {
            get
            {
                if (BuildStatusEnum == BuildStatusEnum.Working) return BallsEnum.Green;
                if (BuildStatusEnum == BuildStatusEnum.Broken) return BallsEnum.Red;
                if (BuildStatusEnum == BuildStatusEnum.Unknown) return BallsEnum.Triangle;
                return BallsEnum.Gray;
            }
        }
        
        public BuildStatusDto AsBuildStatusDto(DateTime now, IDictionary<string, BuildStatus> previousWorkingOrBrokenBuildStatus, SirenOfShameSettings settings)
        {
            BuildStatus previousStatus;
            bool previousStatusExists = previousWorkingOrBrokenBuildStatus.TryGetValue(BuildDefinitionId, out previousStatus);

            string duration = GetDurationAsString(FinishedTime, StartedTime, now, previousStatus);
            PersonSetting personSetting = settings.FindAddPerson(RequestedBy);
            string requestedBy = personSetting == null ? RequestedBy : personSetting.DisplayName;

            var result = new BuildStatusDto
            {
                BuildStatusEnum = BuildStatusEnum,
                ImageIndex = (int)BallIndex,
                StartTimeShort = FormatAsDayMonthTime(StartedTime),
                LocalStartTime = !previousStatusExists && StartedTime.HasValue ? StartedTime.Value : LocalStartTime,
                Duration = duration,
                RequestedBy = requestedBy,
                Comment = Comment,
                BuildId = BuildId ?? "",
                Id = BuildDefinitionId,
                Name = Name,
                Url = Url,
            };
            result.SetDisplayName(settings);
            return result;
        }

        private static string FormatAsDayMonthTime(DateTime? nullableDate)
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
            if (BuildStatusEnum != BuildStatusEnum.InProgress)
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
            return BuildStatusEnum == BuildStatusEnum.Broken && (previousStatus == null || previousStatus == BuildStatusEnum.Working);
        }

        public bool IsNewlyFixed(BuildStatusEnum? previousStatus)
        {
            return BuildStatusEnum == BuildStatusEnum.Working && previousStatus != null && previousStatus == BuildStatusEnum.Broken;
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
                BuildStatusAsExport(BuildStatusEnum)
            };
            return string.Join(",", fieldsToExport);
        }

        private string BuildStatusAsExport(BuildStatusEnum buildStatusEnum)
        {
            return buildStatusEnum == BuildStatusEnum.Working ? "1" : "0";
        }

        private static string DateAsExport(DateTime? dateTime)
        {
            return dateTime == null ? "" : dateTime.Value.Ticks.ToString(CultureInfo.InvariantCulture);
        }

        public string GetBuildDataAsHash() {
            return string.Format("{0}-{1}-{2}-{3}", BuildDefinitionId, BuildId, StartedTime, RequestedBy);
        }

        public NewNewsItemEventArgs AsNewsItemEventArgs(BuildStatusEnum previousWorkingOrBrokenBuildStatus, SirenOfShameSettings settings)
        {
            var person = settings.FindAddPerson(RequestedBy);
            return new NewNewsItemEventArgs
            {
                Person = person,
                EventDate = DateTime.Now,
                Title = GetNewsItemTitle(previousWorkingOrBrokenBuildStatus),
                Project = Name,
                NewsItemType = GetNewsItemType(),
                ReputationChange = GetReputationChange(),
                BuildId = BuildId
            };
        }

        private int? GetReputationChange()
        {
            if (BuildStatusEnum == BuildStatusEnum.Working) return 1;
            if (BuildStatusEnum == BuildStatusEnum.Broken) return -4;
            return null;
        }

        private NewsItemTypeEnum GetNewsItemType()
        {
            if (BuildStatusEnum == BuildStatusEnum.Working) return NewsItemTypeEnum.BuildSuccess;
            if (BuildStatusEnum == BuildStatusEnum.Broken) return NewsItemTypeEnum.BuildFailed;
            if (BuildStatusEnum == BuildStatusEnum.InProgress) return NewsItemTypeEnum.BuildStarted;
            return NewsItemTypeEnum.BuildUnknown;
        }
        
        private string GetNewsItemTitle(BuildStatusEnum previousWorkingOrBrokenBuildStatus)
        {
            var wasBrokenNowWorking = previousWorkingOrBrokenBuildStatus == BuildStatusEnum.Broken && BuildStatusEnum == BuildStatusEnum.Working;
            var wasBrokenNowBroken = previousWorkingOrBrokenBuildStatus == BuildStatusEnum.Broken && BuildStatusEnum == BuildStatusEnum.Broken;
            var wasWorkingNowBroken = previousWorkingOrBrokenBuildStatus == BuildStatusEnum.Working && BuildStatusEnum == BuildStatusEnum.Broken;
            var inProgress = BuildStatusEnum == BuildStatusEnum.InProgress;

            if (inProgress) return string.Format("'{0}'", Comment);
            if (wasBrokenNowWorking) return string.Format("Fixed the broken build");
            if (wasWorkingNowBroken) return string.Format("Broke the build");
            if (wasBrokenNowBroken) return string.Format("Failed to fix the build");
            if (BuildStatusEnum == BuildStatusEnum.Working || BuildStatusEnum == BuildStatusEnum.Unknown) return string.Format("Successful build");
            
            // some other previous status? this should never happen
            if (BuildStatusEnum == BuildStatusEnum.Broken) return string.Format("Broke the build");

            throw new Exception("Unknown build status: " + BuildStatusEnum);
        }
    }
}