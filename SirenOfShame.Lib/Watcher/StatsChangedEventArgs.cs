using System.Collections.Generic;

namespace SirenOfShame.Lib.Watcher
{
    public class StatsChangedEventArgs
    {
        public IList<BuildStatus> ChangedBuildStatuses { get; set; }
    }
}