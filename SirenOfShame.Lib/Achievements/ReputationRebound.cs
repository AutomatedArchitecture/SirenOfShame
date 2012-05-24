using System.Collections.Generic;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace SirenOfShame.Lib.Achievements
{
    public class ReputationRebound : AchievementBase
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(ReputationRebound));
        
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

            if (!achievedThreeConsecurtiveFails) return false;
            _log.Debug("Achieved 3 consecutive builds, buildsSinceThreeConsecutiveFails = " + buildsSinceThreeConsecutiveFails + ", failedSinceThreeConsecutiveFails = " + failedSinceThreeConsecutiveFails);
            return PersonSetting.GetReputation(buildsSinceThreeConsecutiveFails, failedSinceThreeConsecutiveFails) >= 12;
        }
    }
}
