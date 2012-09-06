using System.Collections.Generic;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.StatCalculators
{
    public abstract class StatCalculatorBase
    {
        public abstract void SetStats(
            PersonSetting personSetting,
            List<BuildStatus> currentBuildDefinitionOrderedChronoligically,
            List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically
            );
    }
}
