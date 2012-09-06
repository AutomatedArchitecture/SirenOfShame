using System;
using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace SirenOfShame.Lib.StatCalculators
{
    public class MaxBuildsInOneDay : StatCalculatorBase
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(MaxBuildsInOneDay));

        public override void SetStats(PersonSetting personSetting, List<BuildStatus> currentBuildDefinitionOrderedChronoligically, List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically)
        {
            personSetting.MaxBuildsInOneDay = GetMaxBuildsInOneDay(personSetting, currentBuildDefinitionOrderedChronoligically);
        }

        public static int GetMaxBuildsInOneDay(PersonSetting personSetting, List<BuildStatus> currentBuildDefinitionOrderedChronoligically)
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
