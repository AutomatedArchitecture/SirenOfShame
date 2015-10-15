using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Achievements;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.StatCalculators;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class PersonSetting : PersonBase
    {
        public override string RawName { get; set; }
        public override string DisplayName { get; set; }
        public int TotalBuilds { get; set; }
        public int FailedBuilds { get; set; }
        public bool Hidden { get; set; }
        public List<AchievementSetting> Achievements { get; set; }
        public long? CumulativeBuildTime { get; set; }
        private readonly SosDb _sosDb = new SosDb();
        public int? AvatarId { get; set; }
        public int NumberOfTimesFixedSomeoneElsesBuild { get; set; }
        public int NumberOfTimesPerformedBackToBackBuilds { get; set; }
        public int MaxBuildsInOneDay { get; set; }
        public double CurrentBuildRatio { get; set; }
        public double? LowestBuildRatioAfter50Builds { get; set; }
        public int CurrentSuccessInARow { get; set; }
        public string Email { get; set; }

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

        public override int GetAvatarId(ImageList avatarImageList)
        {
            if (AvatarId.HasValue) return AvatarId.Value;
            return new GravatarService().DownloadGravatarFromEmailAndAddToImageList(Email, avatarImageList);
        }

        public IEnumerable<AchievementLookup> CalculateNewAchievements(SirenOfShameSettings settings, BuildStatus build)
        {
            List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically = _sosDb
                .ReadAll(settings.GetAllActiveBuildDefinitions())
                .OrderBy(i => i.StartedTime)
                .ToList();

            return CalculateNewAchievements(settings, build, allActiveBuildDefinitionsOrderedChronoligically);
        }

        public IEnumerable<AchievementLookup> CalculateNewAchievements(SirenOfShameSettings settings, BuildStatus build, List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically)
        {
            return from achievementEnum in CalculateNewAchievementEnums(settings, build, allActiveBuildDefinitionsOrderedChronoligically)
                   join achievement in AchievementSetting.AchievementLookups on achievementEnum equals achievement.Id
                   select achievement;
        }

        private IEnumerable<AchievementEnum> CalculateNewAchievementEnums(SirenOfShameSettings settings, BuildStatus build, List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically)
        {
            int reputation = GetReputation();

            List<BuildStatus> currentBuildDefinitionOrderedChronoligically = allActiveBuildDefinitionsOrderedChronoligically
                .Where(i => i.BuildDefinitionId == build.BuildDefinitionId)
                .ToList();

            if (build.FinishedTime != null && build.StartedTime != null)
            {
                TimeSpan? buildDuration = build.FinishedTime.Value - build.StartedTime.Value;
                MyCumulativeBuildTime = MyCumulativeBuildTime == null ? buildDuration : MyCumulativeBuildTime + buildDuration;
            }

            CalculateStats(allActiveBuildDefinitionsOrderedChronoligically);

            List<AchievementBase> possibleAchievements = new List<AchievementBase>
            {
                new Apprentice(this, reputation),
                new Neophyte(this, reputation),
                new Master(this, reputation),
                new GrandMaster(this, reputation),
                new Legend(this, reputation),
                new JonSkeet(this, reputation),
                new TimeWarrior(this),
                new ChronMaster(this),
                new ChronGrandMaster(this),
                new CiNinja(this),
                new Assassin(this),
                new LikeLightning(this, currentBuildDefinitionOrderedChronoligically),
                new ReputationRebound(this, allActiveBuildDefinitionsOrderedChronoligically),
                new ArribaArribaAndaleAndale(this),
                new SpeedDaemon(this),
                new InTheZone(this),
                new Terminator(this),
                new AndGotAwayWithIt(this, currentBuildDefinitionOrderedChronoligically),
                new Critical(this),
                new Perfectionist(this),
                new Macgyver(this, currentBuildDefinitionOrderedChronoligically),
                new Napoleon(this, settings.People),
                new ShamePusher(this, settings)
            };

            return possibleAchievements
                .Where(i => i.HasJustAchieved())
                .Select(i => i.AchievementEnum);
        }

        public void CalculateStats(List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically) {
            List<StatCalculatorBase> statCalculators = new List<StatCalculatorBase>
            {
                new FixedSomeoneElsesBuild(),
                new BackToBackBuilds(),
                new MaxBuildsInOneDay(),
                new BuildRatio(),
                new SuccessInARow(),
            };

            statCalculators.ForEach(i => i.SetStats(this, allActiveBuildDefinitionsOrderedChronoligically));
        }

        public bool HasAchieved(AchievementEnum achievement)
        {
            return Achievements.Any(i => i.AchievementId == (int)achievement);
        }

        public void AddAchievements(IEnumerable<AchievementLookup> newAchievements)
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

        public string GetBothDisplayAndRawNames()
        {
            return HasDisplayName() ? string.Format("{0} ({1})", DisplayName, RawName) : RawName;
        }

        private bool HasDisplayName()
        {
            return !string.IsNullOrWhiteSpace(DisplayName) && DisplayName != RawName;
        }
    }
}