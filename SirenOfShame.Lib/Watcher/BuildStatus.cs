using System;
using System.Collections.Generic;
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
        Triangle = 7
    }

    public class BuildStatus
    {
        public BuildStatus()
        {
            LocalStartTime = DateTime.Now;
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
        public int BuildId { get; set; }
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

        public BallsEnum BallIndex
        {
            get
            {
                if (BuildStatusEnum == BuildStatusEnum.Working) return BallsEnum.Green;
                if (BuildStatusEnum == BuildStatusEnum.Broken) return BallsEnum.Red;
                if (BuildStatusEnum == BuildStatusEnum.Unknown) return BallsEnum.Triangle;
                return BallsEnum.Gray;
            }
        }

        public BuildStatusListViewItem AsBuildStatusListViewItem(DateTime now, IDictionary<string, BuildStatus> previousWorkingOrBrokenBuildStatus, SirenOfShameSettings settings)
        {
            BuildStatus previousStatus;
            previousWorkingOrBrokenBuildStatus.TryGetValue(BuildDefinitionId, out previousStatus);

            string duration = GetDurationAsString(FinishedTime, StartedTime, now, previousStatus);
            string startTime = StartedTime == null ? "" : StartedTime.Value.ToString("M/d h:mm tt");
            long startTimeTicks = StartedTime == null ? 0 : StartedTime.Value.Ticks;
            string requestedBy = RequestedBy == null ? "" : RequestedBy.Split('\\').LastOrDefault();

            var result = new BuildStatusListViewItem
            {
                ImageIndex = (int)BallIndex,
                StartTime = startTime,
                StartTimeTicks = startTimeTicks,
                Duration = duration,
                RequestedBy = requestedBy,
                Comment = Comment,
                Id = BuildDefinitionId,
                Name = Name,
                Url = Url,
                BuildId = BuildId
            };
            result.SetDisplayName(settings);
            return result;
        }

        private string GetDurationAsString(DateTime? finishedTime, DateTime? startedTime, DateTime now, BuildStatus previousStatus)
        {
            TimeSpan? duration = GetDuration(startedTime, finishedTime, previousStatus, now);
            if (duration == null) return "";
            if (duration.Value.Ticks < 0) return string.Format("OT: {0}:{1:00}", 0 - (int)duration.Value.TotalMinutes, 0 - duration.Value.Seconds);
            return string.Format("{0}:{1:00}", (int)duration.Value.TotalMinutes, duration.Value.Seconds);
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

        public void Changed(BuildStatusEnum? previousStatus, RulesEngine rulesEngine, List<Rule> rules)
        {
            var rule = rules
                .Where(r => r.IsMatch(this, previousStatus))
                .OrderByDescending(r => r.PriorityId)
                .FirstOrDefault();

            if (rule != null)
                rule.FireEvent(rulesEngine, previousStatus, this);

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
    }
}