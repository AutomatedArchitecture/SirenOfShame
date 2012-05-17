using System;
using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class PersonSetting
    {
        public string RawName { get; set; }
        public string DisplayName { get; set; }
        public int TotalBuilds { get; set; }
        public int FailedBuilds { get; set; }
        public bool Hidden { get; set; }
        public List<AchievementSetting> Achievements { get; set; }

        public PersonSetting()
        {
            Achievements = new List<AchievementSetting>();
        }

        public int GetReputation()
        {
            return TotalBuilds - (FailedBuilds*5);
        }

        public IEnumerable<AchievementLookup> CalculateNewAchievements(BuildStatus build)
        {
            return from achievementEnum in CalculateNewAchievementEnums(build)
                   join achievement in AchievementSetting.AchievementLookups on achievementEnum equals achievement.Id
                   select achievement;
        }

        private IEnumerable<AchievementEnum> CalculateNewAchievementEnums(BuildStatus build)
        {
            int reputation = GetReputation();
            if (!HasAchieved(AchievementEnum.Apprentice) && reputation >= 25)
                yield return AchievementEnum.Apprentice;
        }



        private bool HasAchieved(AchievementEnum achievement)
        {
            return Achievements.Any(i => i.AchievementId == (int)achievement);
        }

        public void AddAchievements(IList<AchievementLookup> newAchievements)
        {
            foreach (var achievementLookup in newAchievements)
            {
                Achievements.Add(new AchievementSetting { AchievementId = (int)achievementLookup.Id, DateAchieved = DateTime.Now });
            }
        }
    }
}