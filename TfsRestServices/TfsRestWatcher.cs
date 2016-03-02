using System.Collections.Generic;
using System.ComponentModel.Composition;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace TfsRestServices
{
    public class TfsRestWatcher : WatcherBase
    {
        public TfsRestWatcher(SirenOfShameSettings settings, TfsRestCiEntryPoint tfsRestCiEntryPoint) : base(settings)
        {
            
        }

        protected override IList<BuildStatus> GetBuildStatus()
        {
            return new List<BuildStatus>();
        }

        public override void StopWatching()
        {
            
        }

        public override void Dispose()
        {
            
        }
    }
}
