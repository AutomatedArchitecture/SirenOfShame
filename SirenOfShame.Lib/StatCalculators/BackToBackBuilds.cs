using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace SirenOfShame.Lib.StatCalculators
{
    public class BackToBackBuilds : StatCalculatorBase
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(BackToBackBuilds));

        public override void SetStats(PersonSetting activePerson, List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically)
        {
            var backToBackBuilds = GetBackToBackBuilds(activePerson, allActiveBuildDefinitionsOrderedChronoligically);
            activePerson.NumberOfTimesPerformedBackToBackBuilds = backToBackBuilds;
        }

        private static int GetBackToBackBuilds(PersonSetting activePerson, IEnumerable<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically)
        {
            return HowManyTimesHasPerformedBackToBackBuildsAcrossBuilds(activePerson, allActiveBuildDefinitionsOrderedChronoligically);
        }

        public static int HowManyTimesHasPerformedBackToBackBuildsAcrossBuilds(PersonSetting activePerson, IEnumerable<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically)
        {
            return allActiveBuildDefinitionsOrderedChronoligically
                .GroupBy(i => i.BuildDefinitionId)
                .Select(i => HowManyTimesHasPerformedBackToBackBuildsForABuild(activePerson, i))
                .Aggregate(0, (i, j) => i + j);
        }

        public static int HowManyTimesHasPerformedBackToBackBuildsForABuild(PersonSetting activePerson, IEnumerable<BuildStatus> currentBuildDefinitionOrderedChronoligically) {
            BuildStatus lastBuild = null;

            int backToBack = 0;

            foreach (var buildStatus in currentBuildDefinitionOrderedChronoligically)
            {
                bool lastBuildWasByActivePerson = lastBuild != null && lastBuild.RequestedBy == activePerson.RawName;
                bool currentBuildIsByActivePerson = buildStatus.RequestedBy == activePerson.RawName;
                if (lastBuildWasByActivePerson && currentBuildIsByActivePerson)
                {
                    bool lastBuildPassed = lastBuild.CurrentBuildStatus == BuildStatusEnum.Working;
                    bool currentBuildPassed = buildStatus.CurrentBuildStatus == BuildStatusEnum.Working;
                    bool wereBackToBack = lastBuild.IsBackToBackWithNextBuild(buildStatus);
                    if (lastBuildPassed && currentBuildPassed && wereBackToBack)
                    {
                        backToBack++;
                    }
                }

                lastBuild = buildStatus;
            }

            _log.Debug(activePerson.RawName + " has achieved back to back successful builds " + backToBack + " times");
            return backToBack;
        }
    }
}
