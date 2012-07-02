using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class JonSkeet : AchievementBase
    {
        private readonly int _reputation;

        public JonSkeet(PersonSetting personSetting, int reputation)
            : base(personSetting)
        {
            _reputation = reputation;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.Legend; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            return _reputation >= 2500;
        }
    }
}
