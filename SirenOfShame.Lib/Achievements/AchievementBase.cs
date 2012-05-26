using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public abstract class AchievementBase
    {
        private readonly PersonSetting _personSetting;

        public abstract AchievementEnum AchievementEnum { get; }

        protected PersonSetting PersonSetting
        {
            get { return _personSetting; }
        }

        protected abstract bool MeetsAchievementCriteria();

        protected AchievementBase(PersonSetting personSetting)
        {
            _personSetting = personSetting;
        }

        public bool HasJustAchieved()
        {
            return !_personSetting.HasAchieved(AchievementEnum) && MeetsAchievementCriteria();
        }
    }
}
