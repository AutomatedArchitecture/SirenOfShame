using System;
using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Achievements;
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

        // this either needs to stay private or find the attribute to not persist
        private TimeSpan? MyCumulativeBuildTime
        {
            get { return CumulativeBuildTime == null ? (TimeSpan?)null : new TimeSpan(CumulativeBuildTime.Value); }
            set { CumulativeBuildTime = value == null ? (long?)null : value.Value.Ticks; }
        }

        public TimeSpan? GetCumulativeBuildTime()
        {
            return MyCumulativeBuildTime;
        }

        public PersonSetting()
        {
            Achievements = new List<AchievementSetting>();
        }

        public static int GetReputation(int totalBuilds, int failedBuilds)
        {
            return totalBuilds - (failedBuilds * 5);
        }
        
        public int GetReputation()
        {
            return GetReputation(TotalBuilds, FailedBuilds);
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

            int howManyTimesHasFixedSomeoneElsesBuild = CiNinja.HowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically, RawName);
            int howManyTimesHasPerformedBackToBackBuilds = ArribaArribaAndaleAndale.HowManyTimesHasPerformedBackToBackBuilds(this, currentBuildDefinitionOrderedChronoligically);
            int maxBuildsInOneDay = InTheZone.MaxBuildsInOneDay(this, currentBuildDefinitionOrderedChronoligically);

            List<AchievementBase> possibleAchievements = new List<AchievementBase>
            {
                new Apprentice(this, reputation),
                new Neophyte(this, reputation),
                new Master(this, reputation),
                new GrandMaster(this, reputation),
                new Legend(this, reputation),
                new TimeWarrior(this),
                new ChronMaster(this),
                new ChronGrandMaster(this),
                new CiNinja(this, howManyTimesHasFixedSomeoneElsesBuild),
                new Assassin(this, howManyTimesHasFixedSomeoneElsesBuild),
                new LikeLightning(this, currentBuildDefinitionOrderedChronoligically),
                new ReputationRebound(this, allActiveBuildDefinitionsOrderedChronoligically),
                new ArribaArribaAndaleAndale(this, howManyTimesHasPerformedBackToBackBuilds),
                new SpeedDaemon(this, howManyTimesHasPerformedBackToBackBuilds),
                new InTheZone(this, maxBuildsInOneDay),
                new UltraProductive(this, maxBuildsInOneDay),
                new AndGotAwayWithIt(this, currentBuildDefinitionOrderedChronoligically)
            };

            return possibleAchievements
                .Where(i => i.HasJustAchieved())
                .Select(i => i.AchievementEnum);
        }

        public bool HasAchieved(AchievementEnum achievement)
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

        public override string ToString()
        {
            return RawName;
        }
    }
}