using System;
using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace SirenOfShame.Lib.Achievements
{
    public class Critical : AchievementBase
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(Critical));
        
        private readonly double? _lowestBuildRatio;

        public Critical(PersonSetting personSetting, double? lowestBuildRatio)
            : base(personSetting)
        {
            _lowestBuildRatio = lowestBuildRatio;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.Critical; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            return _lowestBuildRatio != null && _lowestBuildRatio <= 0.10;
        }

        public static double? CalculateLowestBuildRatio(PersonSetting personSetting, List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically)
        {
            int totalBuilds = 0;
            int unsuccessfulBuilds = 0;
            double lowestRatio = 0.0;
            List<BuildStatus> buildStatuses = allActiveBuildDefinitionsOrderedChronoligically
                .Where(i => i.RequestedBy == personSetting.RawName)
                .ToList();
            if (buildStatuses.Count < 50) return null;
            double currentRatio = 0;
            DateTime? lowestRatioAchieved = null;
            foreach (var buildStatus in buildStatuses)
            {
                totalBuilds++;
                if (buildStatus.BuildStatusEnum == BuildStatusEnum.Broken)
                    unsuccessfulBuilds++;
                currentRatio = (double)unsuccessfulBuilds/totalBuilds;
                if (totalBuilds == 50)
                {
                    lowestRatio = currentRatio;
                    lowestRatioAchieved = buildStatus.StartedTime;
                }
                if (totalBuilds > 50)
                {
                    var oldLowestRatio = lowestRatio;
                    lowestRatio = Math.Min(currentRatio, lowestRatio);
                    if (oldLowestRatio != lowestRatio)
                    {
                        lowestRatioAchieved = buildStatus.StartedTime;
                    }
                }
            }
            _log.Debug(string.Format("{0}'s lowest build ratio is {1:p}. They achieved it on {3:d}. Their current ratio is {2:p}", personSetting.RawName, lowestRatio, currentRatio, lowestRatioAchieved));
            return lowestRatio;
        }
    }
}
