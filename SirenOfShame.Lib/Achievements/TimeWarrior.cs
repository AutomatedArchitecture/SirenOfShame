using System;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class TimeWarrior : AchievementBase
    {
        public TimeWarrior(PersonSetting personSetting) : base(personSetting)
        {
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.TimeWarrior; }
        }

        protected override bool MeetsAchievementCriteria(PersonSetting personSetting)
        {
            return PersonSetting.MyCumulativeBuildTime != null && PersonSetting.MyCumulativeBuildTime.Value.TotalHours >= 24;
        }
    }
}
