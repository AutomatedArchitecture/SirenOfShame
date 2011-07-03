using System.Collections.Generic;

namespace SirenOfShame.Lib.Watcher
{
    public class RefreshStatusEventArgs {
        public IEnumerable<BuildStatusListViewItem> BuildStatusListViewItems { get; set; }
    }
}