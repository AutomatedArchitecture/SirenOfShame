using System.Collections.Generic;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Achievements
{
    public class CiNinja : AchievementBase
    {
        private readonly int _howManyTimesHasFixedSomeoneElsesBuild;

        public CiNinja(PersonSetting personSetting, int howManyTimesHasFixedSomeoneElsesBuild)
            : base(personSetting)
        {
            _howManyTimesHasFixedSomeoneElsesBuild = howManyTimesHasFixedSomeoneElsesBuild;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.CiNinja; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            return _howManyTimesHasFixedSomeoneElsesBuild >= 1;
        }

        public static int HowManyTimesHasFixedSomeoneElsesBuild(List<BuildStatus> currentBuildDefinitionOrderedChronoligically, string rawName)
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
