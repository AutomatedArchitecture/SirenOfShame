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

        public DateTime StartedTime { get; set; }
        public DateTime FinishedTime { get; set; }

        public string RequestedBy { get; set; }
        public DateTime LocalStartTime { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
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

        public BuildStatusListViewItem AsBuildStatusListViewItem()
        {
            bool noStartTime = StartedTime == DateTime.MinValue;
            string duration = GetDuration(FinishedTime, StartedTime);
            string startTime = noStartTime ? "" : StartedTime.ToString("M/d h:mm tt");
            string requestedBy = RequestedBy == null ? "" : RequestedBy.Split('\\').LastOrDefault();

            return new BuildStatusListViewItem
            {
                ImageIndex = (int) BallIndex,
                StartTime = startTime,
                Duration = duration,
                RequestedBy = requestedBy,
                Comment = Comment,
                Id = Id,
                Name = Name
            };
        }
        
        private string GetDuration(DateTime finishedTime, DateTime startedTime)
        {
            TimeSpan duration;
            if (BuildStatusEnum != BuildStatusEnum.InProgress)
            {
                if (startedTime == DateTime.MinValue || finishedTime == DateTime.MinValue)
                {
                    _log.Warn("Start time or stop time was null for " + Id + ", and the build was not in progress, this should only happen at startup");
                    return "";
                }
                duration = finishedTime - startedTime;
            } else
            {
                duration = DateTime.Now - LocalStartTime;
            }
            return string.Format("{0}:{1:00}", (int)duration.TotalMinutes, duration.Seconds);
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