using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Achievements
{
    public class Napoleon : AchievementBase
    {
        private readonly List<PersonSetting> _people;

        public Napoleon(PersonSetting personSetting, List<PersonSetting> people) : base(personSetting)
        {
            _people = people;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.Napoleon; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            var myReputation = PersonSetting.GetReputation();
            var everyoneElseOnTeam = _people.Where(i => i.RawName != PersonSetting.RawName).ToList();
            if (everyoneElseOnTeam.Count == 0) return false;
            return everyoneElseOnTeam.All(i => i.GetReputation() <= (myReputation - 100));
        }
    }
}
