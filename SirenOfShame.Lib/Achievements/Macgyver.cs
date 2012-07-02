using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace SirenOfShame.Lib.Achievements
{
    public class Macgyver : AchievementBase
    {
        private readonly List<BuildStatus> _currentBuildDefinitionOrderedChronoligically;
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(Macgyver));

        public Macgyver(PersonSetting personSetting, List<BuildStatus> currentBuildDefinitionOrderedChronoligically) : base(personSetting)
        {
            _currentBuildDefinitionOrderedChronoligically = currentBuildDefinitionOrderedChronoligically;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.Macgyver; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            int count = _currentBuildDefinitionOrderedChronoligically.Count;
            if (count < 2) return false;
            var currentBuild = _currentBuildDefinitionOrderedChronoligically[count - 1];
            if (currentBuild.BuildStatusEnum != BuildStatusEnum.Working)
            {
                _log.Debug("Only working builds are eligible for Macgyver achievements");
                return false;
            }
            var lastSuccessfulBuild = _currentBuildDefinitionOrderedChronoligically
                .Take(count - 1)
                .LastOrDefault(i => i.BuildStatusEnum == BuildStatusEnum.Working && i.FinishedTime != null && i.StartedTime != null);
            if (lastSuccessfulBuild == null)
            {
                _log.Debug("Could not find a previous build that was working with a start and end time for " + currentBuild.BuildDefinitionId);
                return false;
            }
            if (currentBuild.StartedTime == null || currentBuild.FinishedTime == null || lastSuccessfulBuild.FinishedTime == null || lastSuccessfulBuild.StartedTime == null) return false;
            // ReSharper disable PossibleInvalidOperationException
            var lastDuration = lastSuccessfulBuild.GetDuration().Value;
            var currentDuration = currentBuild.GetDuration().Value;
            // ReSharper restore PossibleInvalidOperationException
            double percentDecrease = 1 - (currentDuration.TotalSeconds / lastDuration.TotalSeconds);
            _log.Debug(string.Format("Build time was decreased by {0:p}", percentDecrease));
            return percentDecrease >= .15;
        }
    }
}
