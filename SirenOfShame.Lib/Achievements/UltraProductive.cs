using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class UltraProductive : AchievementBase
    {
        private readonly int _maxBuildsInOneDay;

        public UltraProductive(PersonSetting personSetting, int maxBuildsInOneDay)
            : base(personSetting)
        {
            _maxBuildsInOneDay = maxBuildsInOneDay;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.UltraProductive; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            return _maxBuildsInOneDay >= 10;
        }
    }
}
