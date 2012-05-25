using System.Collections.Generic;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Achievements
{
    public class ArribaArribaAndaleAndale : AchievementBase
    {
        private readonly int _currentBuildDefinitionOrderedChronoligically;

        public ArribaArribaAndaleAndale(PersonSetting personSetting, int currentBuildDefinitionOrderedChronoligically) : base(personSetting)
        {
            _currentBuildDefinitionOrderedChronoligically = currentBuildDefinitionOrderedChronoligically;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.ArribaArribaAndaleAndale; }
        }

        protected override bool MeetsAchievementCriteria(PersonSetting personSetting)
        {
            return _currentBuildDefinitionOrderedChronoligically >= 5;
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

            return backToBack;
        }
    }
}
