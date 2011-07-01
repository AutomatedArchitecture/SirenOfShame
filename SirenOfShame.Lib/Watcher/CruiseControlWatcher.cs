using System;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Watcher
{
    public class CruiseControlWatcher : WatcherBase
    {
        public CruiseControlWatcher(SirenOfShameSettings settings) : base(settings) { }

        protected override System.Collections.Generic.IEnumerable<BuildStatus> GetBuildStatus()
        {
            throw new NotImplementedException();
        }

        public override void StopWatching() {
        }

        public override void Dispose()
        {
        }
    }
}
