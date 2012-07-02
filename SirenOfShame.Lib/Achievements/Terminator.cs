using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class Terminator : AchievementBase
    {
        private readonly int _maxBuildsInOneDay;

        public Terminator(PersonSetting personSetting, int maxBuildsInOneDay)
            : base(personSetting)
        {
            _maxBuildsInOneDay = maxBuildsInOneDay;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.Terminator; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            return _maxBuildsInOneDay >= 10;
        }
    }
}
