using System.Collections.Generic;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Watcher
{
    public class RefreshStatusEventArgs {
        public IEnumerable<BuildStatusDto> BuildStatusListViewItems { get; set; }

        public void RefreshDisplayNames(SirenOfShameSettings settings)
        {
            foreach (var buildStatusDto in BuildStatusListViewItems)
            {
                buildStatusDto.SetDisplayName(settings);
            }
        }
    }
}