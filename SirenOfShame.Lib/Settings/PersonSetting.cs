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
        private readonly SosDb _sosDb = new SosDb();

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

        public IEnumerable<AchievementLookup> CalculateNewAchievements(SirenOfShameSettings settings, BuildStatus build)
        {
            return from achievementEnum in CalculateNewAchievementEnums(settings, build)
                   join achievement in AchievementSetting.AchievementLookups on achievementEnum equals achievement.Id
                   select achievement;
        }

        private IEnumerable<AchievementEnum> CalculateNewAchievementEnums(SirenOfShameSettings settings, BuildStatus build)
        {
            int reputation = GetReputation();

            if (build.FinishedTime != null && build.StartedTime != null)
            {
                TimeSpan? buildDuration = build.FinishedTime.Value - build.StartedTime.Value;
                MyCumulativeBuildTime = MyCumulativeBuildTime == null ? buildDuration : MyCumulativeBuildTime + buildDuration;
            }

            var allActiveBuildDefinitionsOrderedChronoligically = _sosDb
                .ReadAll(settings.GetAllActiveBuildDefinitions())
                .OrderBy(i => i.StartedTime)
                .ToList();

            var currentBuildDefinitionOrderedChronoligically = allActiveBuildDefinitionsOrderedChronoligically
                .Where(i => i.BuildDefinitionId == build.BuildDefinitionId)
                .ToList();

            int howManyTimesHasFixedSomeoneElsesBuild = HowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically);

            List<AchievementEnum> newAchievements = new List<AchievementEnum>();
            
            AppendIfHasNotAchievedYet(AchievementEnum.Apprentice, () => reputation >= 25, newAchievements);
            AppendIfHasNotAchievedYet(AchievementEnum.Neophyte, () => reputation >= 100, newAchievements);
            AppendIfHasNotAchievedYet(AchievementEnum.Master, () => reputation >= 250, newAchievements);
            AppendIfHasNotAchievedYet(AchievementEnum.GrandMaster, () => reputation >= 500, newAchievements);
            AppendIfHasNotAchievedYet(AchievementEnum.Legend, () => reputation >= 1000, newAchievements);
            AppendIfHasNotAchievedYet(AchievementEnum.TimeWarrior, () => MyCumulativeBuildTime != null && MyCumulativeBuildTime.Value.TotalHours >= 24, newAchievements);
            AppendIfHasNotAchievedYet(AchievementEnum.ChronMaster, () => MyCumulativeBuildTime != null && MyCumulativeBuildTime.Value.TotalHours >= 48, newAchievements);
            AppendIfHasNotAchievedYet(AchievementEnum.ChronGrandMaster, () => MyCumulativeBuildTime != null && MyCumulativeBuildTime.Value.TotalHours >= 96, newAchievements);
            AppendIfHasNotAchievedYet(AchievementEnum.CiNinja, () => howManyTimesHasFixedSomeoneElsesBuild >= 1, newAchievements);
            AppendIfHasNotAchievedYet(AchievementEnum.Assassin, () => howManyTimesHasFixedSomeoneElsesBuild >= 10, newAchievements);

            return newAchievements;
        }

        private void AppendIfHasNotAchievedYet(AchievementEnum achievementEnum, Func<bool> func, List<AchievementEnum> newAchievements)
        {
            if (!HasAchieved(achievementEnum) && func())
            {
                newAchievements.Add(achievementEnum);
            }
        }

        protected int HowManyTimesHasFixedSomeoneElsesBuild(List<BuildStatus> currentBuildDefinitionOrderedChronoligically)
        {
            int fixedSomeoneElsesBuildCount = 0;
            string buildInitiallyBrokenBy = null;
            foreach (var buildStatus in currentBuildDefinitionOrderedChronoligically)
            {
                bool newlyBroken = buildStatus.BuildStatusEnum == BuildStatusEnum.Broken && buildInitiallyBrokenBy == null;
                bool wasBrokenLastTime = buildInitiallyBrokenBy != null;
                bool newlyFixed = wasBrokenLastTime && buildStatus.BuildStatusEnum == BuildStatusEnum.Working;
                
                if (newlyBroken)
                {
                    buildInitiallyBrokenBy = buildStatus.RequestedBy;
                }
                if (newlyFixed && buildInitiallyBrokenBy != RawName && buildStatus.RequestedBy == RawName)
                {
                    fixedSomeoneElsesBuildCount++;
                }
                if (newlyFixed)
                {
                    buildInitiallyBrokenBy = null;
                }
            }
            return fixedSomeoneElsesBuildCount;
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