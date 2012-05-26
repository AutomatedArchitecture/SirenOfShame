using System.Collections.Generic;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace SirenOfShame.Lib.Achievements
{
    public class ArribaArribaAndaleAndale : AchievementBase
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(BuildStatus));
        private readonly int _howManyTimesHasPerformedBackToBackBuilds;

        public ArribaArribaAndaleAndale(PersonSetting personSetting, int howManyTimesHasPerformedBackToBackBuilds)
            : base(personSetting)
        {
            _howManyTimesHasPerformedBackToBackBuilds = howManyTimesHasPerformedBackToBackBuilds;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.ArribaArribaAndaleAndale; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            return _howManyTimesHasPerformedBackToBackBuilds >= 5;
        }

        public static int HowManyTimesHasPerformedBackToBackBuilds(PersonSetting activePerson, List<BuildStatus> currentBuildDefinitionOrderedChronoligically)
        {
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
