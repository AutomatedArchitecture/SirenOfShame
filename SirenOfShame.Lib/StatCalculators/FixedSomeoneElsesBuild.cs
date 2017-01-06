using System.Collections.Generic;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using System.Linq;

namespace SirenOfShame.Lib.StatCalculators
{
    public class FixedSomeoneElsesBuild : StatCalculatorBase
    {
        public override void SetStats(PersonSetting personSetting, List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically)
        {
            personSetting.NumberOfTimesFixedSomeoneElsesBuild = HowManyTimesFixedSomeoneElsesBuildForAllBuilds(allActiveBuildDefinitionsOrderedChronoligically, personSetting.RawName);
        }

        public static int HowManyTimesFixedSomeoneElsesBuildForAllBuilds(IEnumerable<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically, string rawName)
        {
            var sumOfNumberOfTimesFixedSomeoneElsesBuildByProject = allActiveBuildDefinitionsOrderedChronoligically
                .GroupBy(i => i.BuildDefinitionId)
                .Select(i => HowManyTimesHasFixedSomeoneElsesBuildForBuild(i, rawName))
                .Aggregate(0, (i, j) => i + j);
            return sumOfNumberOfTimesFixedSomeoneElsesBuildByProject;
        }

        public static int HowManyTimesHasFixedSomeoneElsesBuildForBuild(IEnumerable<BuildStatus> currentBuildDefinitionOrderedChronoligically, string rawName)
        {
            int fixedSomeoneElsesBuildCount = 0;
            string buildInitiallyBrokenBy = null;
            foreach (var buildStatus in currentBuildDefinitionOrderedChronoligically)
            {
                bool newlyBroken = buildStatus.CurrentBuildStatus == BuildStatusEnum.Broken && buildInitiallyBrokenBy == null;
                bool wasBrokenLastTime = buildInitiallyBrokenBy != null;
                bool newlyFixed = wasBrokenLastTime && buildStatus.CurrentBuildStatus == BuildStatusEnum.Working;

                if (newlyBroken)
                {
                    buildInitiallyBrokenBy = buildStatus.RequestedBy;
                }
                if (newlyFixed && buildInitiallyBrokenBy != rawName && buildStatus.RequestedBy == rawName)
                {
                    fixedSomeoneElsesBuildCount++;
                }
                if (newlyFixed)
                {
                    buildInitiallyBrokenBy = null;
                }
            }
            return fixedSomeoneElsesBuildCount;
        }
    }
}
