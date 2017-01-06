using System;
using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace SirenOfShame.Lib.StatCalculators
{
    public class BuildRatio : StatCalculatorBase
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(BuildRatio));

        public override void SetStats(PersonSetting personSetting, List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically)
        {
            var buildRatio = CalculateLowestBuildRatioAfter50Builds(personSetting, allActiveBuildDefinitionsOrderedChronoligically);
            personSetting.CurrentBuildRatio = CalculateCurrentBuildRatio(personSetting, allActiveBuildDefinitionsOrderedChronoligically);
            personSetting.LowestBuildRatioAfter50Builds = buildRatio;
        }

        public static double CalculateCurrentBuildRatio(PersonSetting personSetting, IEnumerable<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically)
        {
            var currentUserBuilds = allActiveBuildDefinitionsOrderedChronoligically.Where(i => i.RequestedBy == personSetting.RawName).ToList();
            var totalBuilds = currentUserBuilds.Count;
            var unsuccessfulBuilds = currentUserBuilds.Count(i => i.CurrentBuildStatus == BuildStatusEnum.Broken);
            return totalBuilds == 0 ? 0 : (double)unsuccessfulBuilds/totalBuilds;
        }

        public static double? CalculateLowestBuildRatioAfter50Builds(PersonSetting personSetting, IEnumerable<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically)
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
                if (buildStatus.CurrentBuildStatus == BuildStatusEnum.Broken)
                    unsuccessfulBuilds++;
                currentRatio = (double)unsuccessfulBuilds / totalBuilds;
                if (totalBuilds == 50)
                {
                    lowestRatio = currentRatio;
                    lowestRatioAchieved = buildStatus.StartedTime;
                }
                if (totalBuilds > 50)
                {
                    var oldLowestRatio = lowestRatio;
                    lowestRatio = Math.Min(currentRatio, lowestRatio);
                    if (Math.Abs(oldLowestRatio - lowestRatio) > .001)
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
