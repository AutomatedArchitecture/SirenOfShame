using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class Legend : AchievementBase
    {
        private readonly int _reputation;

        public Legend(PersonSetting personSetting, int reputation)
            : base(personSetting)
        {
            _reputation = reputation;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.Legend; }
        }

        protected override bool MeetsAchievementCriteria(PersonSetting personSetting)
        {
            return _reputation >= 1000;
        }
    }
}
