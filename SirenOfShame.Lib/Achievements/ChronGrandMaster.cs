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

        protected override bool MeetsAchievementCriteria(PersonSetting personSetting)
        {
            return PersonSetting.MyCumulativeBuildTime != null && PersonSetting.MyCumulativeBuildTime.Value.TotalHours >= 96;
        }
    }
}
