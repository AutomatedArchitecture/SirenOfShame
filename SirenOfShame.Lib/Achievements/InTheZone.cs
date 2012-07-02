using System;
using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace SirenOfShame.Lib.Achievements
{
    public class InTheZone : AchievementBase
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(InTheZone));
        private readonly int _maxBuildsInOneDay;

        public InTheZone(PersonSetting personSetting, int maxBuildsInOneDay) : base(personSetting)
        {
            _maxBuildsInOneDay = maxBuildsInOneDay;
        }

        public override AchievementEnum AchievementEnum
        {
            get { return AchievementEnum.InTheZone; }
        }

        protected override bool MeetsAchievementCriteria()
        {
            return _maxBuildsInOneDay >= 5;
        }

        public static int MaxBuildsInOneDay(PersonSetting personSetting, List<BuildStatus> currentBuildDefinitionOrderedChronoligically)
        {
            var buildsGroupedByDay = currentBuildDefinitionOrderedChronoligically
                .Where(i => i.RequestedBy == personSetting.RawName && i.StartedTime != null)
                .GroupBy(i => new DateTime(i.StartedTime.Value.Year, i.StartedTime.Value.Month, i.StartedTime.Value.Day))
                .ToList();
            if (buildsGroupedByDay.Count == 0) return 0; // rare: only if there's one build and it didn't have a start date
            int maxBuildsInOneDay = buildsGroupedByDay.Max(i => i.Count());
            _log.Debug("Max builds in one day for " + personSetting.RawName + " on " + currentBuildDefinitionOrderedChronoligically.First().BuildDefinitionId + " is " + maxBuildsInOneDay);
            return maxBuildsInOneDay;
        }
    }
}
