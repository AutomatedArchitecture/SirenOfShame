using System.Collections.Generic;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Watcher
{
    public class RefreshStatusEventArgs {
        public IEnumerable<BuildStatusListViewItem> BuildStatusListViewItems { get; set; }

        public void RefreshDisplayNames(SirenOfShameSettings settings)
        {
            foreach (var buildStatusListViewItem in BuildStatusListViewItems)
            {
                buildStatusListViewItem.SetDisplayName(settings);
            }
        }
    }
}