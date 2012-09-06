using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class InTheZone : AchievementBase
    {
        private readonly int _maxBuildsInOneDay;

        public InTheZone(PersonSetting personSetting) : base(personSetting)
        {
            _maxBuildsInOneDay = personSetting.MaxBuildsInOneDay;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.InTheZone; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            return _maxBuildsInOneDay >= 5;
        }
    }
}
