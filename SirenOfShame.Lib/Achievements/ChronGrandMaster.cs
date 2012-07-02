using System;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class ChronGrandMaster : AchievementBase
    {
        public ChronGrandMaster(PersonSetting personSetting) : base(personSetting)
        {
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.ChronGrandMaster; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            TimeSpan? cumulativeBuildTime = PersonSetting.GetCumulativeBuildTime();
            return cumulativeBuildTime != null && cumulativeBuildTime.Value.TotalHours >= 96;
        }
    }
}
