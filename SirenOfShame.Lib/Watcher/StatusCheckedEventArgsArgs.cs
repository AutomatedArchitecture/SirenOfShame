using System.Collections.Generic;

namespace SirenOfShame.Lib.Watcher
{
    public class StatusCheckedEventArgsArgs
    {
        public IList<BuildStatus> BuildStatuses { get; set; }
    }
}