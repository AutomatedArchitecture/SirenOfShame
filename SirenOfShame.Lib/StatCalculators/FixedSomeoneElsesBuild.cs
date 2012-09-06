using System.Collections.Generic;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.StatCalculators
{
    public class FixedSomeoneElsesBuild : StatCalculatorBase
    {
        public override void SetStats(PersonSetting personSetting, List<BuildStatus> currentBuildDefinitionOrderedChronoligically, List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically)
        {
            personSetting.NumberOfTimesFixedSomeoneElsesBuild = HowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically, personSetting.RawName);
        }

        public static int HowManyTimesHasFixedSomeoneElsesBuild(IEnumerable<BuildStatus> currentBuildDefinitionOrderedChronoligically, string rawName)
        {
            int fixedSomeoneElsesBuildCount = 0;
            string buildInitiallyBrokenBy = null;
            foreach (var buildStatus in currentBuildDefinitionOrderedChronoligically)
            {
                bool newlyBroken = buildStatus.BuildStatusEnum == BuildStatusEnum.Broken && buildInitiallyBrokenBy == null;
                bool wasBrokenLastTime = buildInitiallyBrokenBy != null;
                bool newlyFixed = wasBrokenLastTime && buildStatus.BuildStatusEnum == BuildStatusEnum.Working;

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
