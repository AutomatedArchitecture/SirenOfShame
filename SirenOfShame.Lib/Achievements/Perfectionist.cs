using System;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class Perfectionist : AchievementBase
    {
        private readonly double? _lowestBuildRatio;

        public Perfectionist(PersonSetting personSetting, double? lowestBuildRatio) : base(personSetting)
        {
            _lowestBuildRatio = lowestBuildRatio;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.Perfectionist; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            return _lowestBuildRatio < .05;
        }
    }
}
