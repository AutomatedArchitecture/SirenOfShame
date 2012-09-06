using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class SpeedDaemon : AchievementBase
    {
        private readonly int _howManyTimesHasPerformedBackToBackBuilds;

        public SpeedDaemon(PersonSetting personSetting) : base(personSetting)
        {
            _howManyTimesHasPerformedBackToBackBuilds = personSetting.NumberOfTimesPerformedBackToBackBuilds;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.SpeedDaemon; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            return _howManyTimesHasPerformedBackToBackBuilds >= 10;
        }
    }
}
