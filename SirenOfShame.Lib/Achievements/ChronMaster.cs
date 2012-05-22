using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class ChronMaster : AchievementBase
    {
        public ChronMaster(PersonSetting personSetting) : base(personSetting)
        {
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.ChronMaster; }
        }

        protected override bool MeetsAchievementCriteria(PersonSetting personSetting)
        {
            return PersonSetting.MyCumulativeBuildTime != null && PersonSetting.MyCumulativeBuildTime.Value.TotalHours >= 48;
        }
    }
}
