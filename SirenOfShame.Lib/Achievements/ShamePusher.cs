using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class ShamePusher : AchievementBase
    {
        private readonly PersonSetting _personSetting;
        private readonly SirenOfShameSettings _settings;

        public ShamePusher(PersonSetting personSetting, SirenOfShameSettings settings) : base(personSetting)
        {
            _personSetting = personSetting;
            _settings = settings;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.ShamePusher; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            bool iJustCheckedIn = _personSetting.RawName == _settings.MyRawName;
            if (!iJustCheckedIn) return false;
            return _settings.SirenEverConnected;
        }
    }
}
