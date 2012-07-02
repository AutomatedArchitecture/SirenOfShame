using System.Collections.Generic;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Achievements
{
    public class AndGotAwayWithIt : AchievementBase
    {
        private readonly List<BuildStatus> _currentBuildDefinitionOrderedChronoligically;

        public AndGotAwayWithIt(PersonSetting personSetting, List<BuildStatus> currentBuildDefinitionOrderedChronoligically) : base(personSetting)
        {
            _currentBuildDefinitionOrderedChronoligically = currentBuildDefinitionOrderedChronoligically;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.AndGotAwayWithIt; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            int count = _currentBuildDefinitionOrderedChronoligically.Count;
            if (count < 2) return false;
            var currentBuild = _currentBuildDefinitionOrderedChronoligically[count - 1];
            var previousBuild = _currentBuildDefinitionOrderedChronoligically[count - 2];
            bool currentAndLastBuildByCurrentUser = previousBuild.RequestedBy == PersonSetting.RawName && currentBuild.RequestedBy == PersonSetting.RawName;
            bool wasJustFixed = previousBuild.BuildStatusEnum == BuildStatusEnum.Broken && currentBuild.BuildStatusEnum == BuildStatusEnum.Working;
            bool fixedWithinSixtySeconds = previousBuild.IsBackToBackWithNextBuild(currentBuild, 60);
            return currentAndLastBuildByCurrentUser && wasJustFixed && fixedWithinSixtySeconds;
        }
    }
}
