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
        public long? CumulativeBuildTime { get; set; }

        private TimeSpan? MyCumulativeBuildTime
        {
            get { return CumulativeBuildTime == null ? (TimeSpan?)null : new TimeSpan(CumulativeBuildTime.Value); }
            set { CumulativeBuildTime = value == null ? (long?)null : value.Value.Ticks; }
        }

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

            if (build.FinishedTime != null && build.StartedTime != null)
            {
                TimeSpan? buildDuration = build.FinishedTime.Value - build.StartedTime.Value;
                MyCumulativeBuildTime = MyCumulativeBuildTime == null ? buildDuration : MyCumulativeBuildTime + buildDuration;
            }

            if (!HasAchieved(AchievementEnum.Apprentice) && reputation >= 25)
                yield return AchievementEnum.Apprentice;
            if (!HasAchieved(AchievementEnum.Neophyte) && reputation >= 100)
                yield return AchievementEnum.Neophyte;
            if (!HasAchieved(AchievementEnum.Master) && reputation >= 250)
                yield return AchievementEnum.Master;
            if (!HasAchieved(AchievementEnum.GrandMaster) && reputation >= 500)
                yield return AchievementEnum.GrandMaster;
            if (!HasAchieved(AchievementEnum.Legend) && reputation >= 1000)
                yield return AchievementEnum.Legend;
            if (!HasAchieved(AchievementEnum.TimeWarrior) && MyCumulativeBuildTime != null && MyCumulativeBuildTime.Value.TotalHours >= 24)
                yield return AchievementEnum.TimeWarrior;
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