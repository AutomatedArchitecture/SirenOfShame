using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class Assassin : AchievementBase
    {
        private readonly int _howManyTimesHasFixedSomeoneElsesBuild;

        public Assassin(PersonSetting personSetting, int howManyTimesHasFixedSomeoneElsesBuild)
            : base(personSetting)
        {
            _howManyTimesHasFixedSomeoneElsesBuild = howManyTimesHasFixedSomeoneElsesBuild;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.Assassin; }
        }

        protected override bool MeetsAchievementCriteria(PersonSetting personSetting)
        {
            return _howManyTimesHasFixedSomeoneElsesBuild >= 10;
        }
    }
}
