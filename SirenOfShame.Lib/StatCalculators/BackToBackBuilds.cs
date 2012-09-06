using System.Collections.Generic;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace SirenOfShame.Lib.StatCalculators
{
    public class BackToBackBuilds : StatCalculatorBase
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(BackToBackBuilds));

        public override void SetStats(PersonSetting activePerson, List<BuildStatus> currentBuildDefinitionOrderedChronoligically, List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically)
        {
            var backToBackBuilds = GetBackToBackBuilds(activePerson, currentBuildDefinitionOrderedChronoligically);
            activePerson.NumberOfTimesPerformedBackToBackBuilds = backToBackBuilds;
        }

        private static int GetBackToBackBuilds(PersonSetting activePerson, IEnumerable<BuildStatus> currentBuildDefinitionOrderedChronoligically)
        {
            return HowManyTimesHasPerformedBackToBackBuilds(activePerson, currentBuildDefinitionOrderedChronoligically);
        }

        public static int HowManyTimesHasPerformedBackToBackBuilds(PersonSetting activePerson, IEnumerable<BuildStatus> currentBuildDefinitionOrderedChronoligically) {
            BuildStatus lastBuild = null;

            int backToBack = 0;

            foreach (var buildStatus in currentBuildDefinitionOrderedChronoligically)
            {
                bool lastBuildWasByActivePerson = lastBuild != null && lastBuild.RequestedBy == activePerson.RawName;
                bool currentBuildIsByActivePerson = buildStatus.RequestedBy == activePerson.RawName;
                if (lastBuildWasByActivePerson && currentBuildIsByActivePerson)
                {
                    bool lastBuildPassed = lastBuild.BuildStatusEnum == BuildStatusEnum.Working;
                    bool currentBuildPassed = buildStatus.BuildStatusEnum == BuildStatusEnum.Working;
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
