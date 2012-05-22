using System.Collections.Generic;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Achievements
{
    public class ReputationRebound : AchievementBase
    {
        private readonly List<BuildStatus> _allActiveBuildDefinitionsOrderedChronoligically;

        public ReputationRebound(PersonSetting personSetting, List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically) : base(personSetting)
        {
            _allActiveBuildDefinitionsOrderedChronoligically = allActiveBuildDefinitionsOrderedChronoligically;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.ReputationRebound; }
        }

        protected override bool MeetsAchievementCriteria(PersonSetting personSetting)
        {
            int consecutiveFailedBuilds = 0;
            bool achievedThreeConsecurtiveFails = false;
            int buildsSinceThreeConsecutiveFails = 0;
            int failedSinceThreeConsecutiveFails = 0;
            foreach (var buildStatus in _allActiveBuildDefinitionsOrderedChronoligically)
            {
                if (achievedThreeConsecurtiveFails && buildStatus.RequestedBy == PersonSetting.RawName)
                {
                    buildsSinceThreeConsecutiveFails++;
                    if (buildStatus.BuildStatusEnum == BuildStatusEnum.Broken)
                        failedSinceThreeConsecutiveFails++;
                }
                
                if (buildStatus.RequestedBy == PersonSetting.RawName && buildStatus.BuildStatusEnum == BuildStatusEnum.Broken)
                {
                    consecutiveFailedBuilds++;
                    if (consecutiveFailedBuilds >= 3)
                        achievedThreeConsecurtiveFails = true;
                } 
                else
                {
                    consecutiveFailedBuilds = 0;
                }
            }

            return achievedThreeConsecurtiveFails &&
                   PersonSetting.GetReputation(buildsSinceThreeConsecutiveFails, failedSinceThreeConsecutiveFails) >= 12;
        }
    }
}
