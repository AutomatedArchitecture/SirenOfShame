using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class GrandMaster : AchievementBase
    {
        private readonly int _reputation;

        public GrandMaster(PersonSetting personSetting, int reputation)
            : base(personSetting)
        {
            _reputation = reputation;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.GrandMaster; }
        }

        protected override bool MeetsAchievementCriteria(PersonSetting personSetting)
        {
            return _reputation >= 500;
        }
    }
}
