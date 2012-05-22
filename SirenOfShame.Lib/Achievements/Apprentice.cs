using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class Apprentice : AchievementBase
    {
        private readonly int _reputation;

        public Apprentice(PersonSetting personSetting, int reputation) : base(personSetting)
        {
            _reputation = reputation;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.Apprentice; }
        }

        protected override bool MeetsAchievementCriteria(PersonSetting personSetting)
        {
            return _reputation >= 25;
        }
    }
}
