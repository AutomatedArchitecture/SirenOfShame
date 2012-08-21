using System.Collections.Generic;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Watcher
{
    public class RefreshStatusEventArgs {
        public IList<BuildStatusDto> BuildStatusDtos { get; set; }

        public void RefreshDisplayNames(SirenOfShameSettings settings)
        {
            foreach (var buildStatusDto in BuildStatusDtos)
            {
                buildStatusDto.SetDisplayName(settings);
            }
        }
    }
}