using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class Critical : AchievementBase
    {
        private readonly double? _lowestBuildRatio;

        public Critical(PersonSetting personSetting)
            : base(personSetting)
        {
            _lowestBuildRatio = personSetting.LowestBuildRatioAfter50Builds;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.Critical; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            return _lowestBuildRatio != null && _lowestBuildRatio <= 0.10;
        }
    }
}
