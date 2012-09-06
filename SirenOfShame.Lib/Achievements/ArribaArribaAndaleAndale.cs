using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class ArribaArribaAndaleAndale : AchievementBase
    {
        private readonly int _howManyTimesHasPerformedBackToBackBuilds;

        public ArribaArribaAndaleAndale(PersonSetting personSetting)
            : base(personSetting)
        {
            _howManyTimesHasPerformedBackToBackBuilds = personSetting.NumberOfTimesPerformedBackToBackBuilds;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.ArribaArribaAndaleAndale; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            return _howManyTimesHasPerformedBackToBackBuilds >= 5;
        }
    }
}
