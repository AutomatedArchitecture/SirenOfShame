using System;
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

        protected override bool MeetsAchievementCriteria()
        {
            int consecutiveFailedBuilds = 0;
            DateTime? achievedThreeConsecurtiveFails = null;
            int buildsSinceThreeConsecutiveFails = 0;
            int failedSinceThreeConsecutiveFails = 0;
            foreach (var buildStatus in _allActiveBuildDefinitionsOrderedChronoligically)
            {
                if (achievedThreeConsecurtiveFails.HasValue && buildStatus.RequestedBy == PersonSetting.RawName)
                {
                    buildsSinceThreeConsecutiveFails++;
                    if (buildStatus.BuildStatusEnum == BuildStatusEnum.Broken)
                        failedSinceThreeConsecutiveFails++;
                }
                
                if (buildStatus.RequestedBy == PersonSetting.RawName && buildStatus.BuildStatusEnum == BuildStatusEnum.Broken)
                {
                    consecutiveFailedBuilds++;
                    if (consecutiveFailedBuilds >= 3)
                        achievedThreeConsecurtiveFails = buildStatus.StartedTime ?? DateTime.Now;
                } 
                else
                {
                    consecutiveFailedBuilds = 0;
                }
            }

            if (!achievedThreeConsecurtiveFails.HasValue) return false;
            bool meetsAchievementCriteria = PersonSetting.GetReputation(buildsSinceThreeConsecutiveFails, failedSinceThreeConsecutiveFails) >= 12;
            if (!meetsAchievementCriteria)
                _log.Debug(PersonSetting.RawName + " did not meet reputation rebound criteria. They achieved 3 consecutive failed builds on " + achievedThreeConsecurtiveFails + " and since then they have build " + buildsSinceThreeConsecutiveFails + " times, of those " + failedSinceThreeConsecutiveFails + " were failures.");
            return meetsAchievementCriteria;
        }
    }
}
