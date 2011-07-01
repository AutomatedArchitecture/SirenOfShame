using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    public class WatcherFake : WatcherBase
    {
        public WatcherFake(SirenOfShameSettings settings)
            : base(settings)
        {
        }

        protected override IEnumerable<BuildStatus> GetBuildStatus()
        {
            return Enumerable.Empty<BuildStatus>();
        }

        public override void Dispose()
        {
            // do nothing
        }

        public override void StartWatching()
        {
            // do nothing
        }

        public override void StopWatching()
        {
            // do nothing
        }

        public new void InvokeServerUnavailable(ServerUnavailableEventArgs ex)
        {
            base.InvokeServerUnavailable(ex);
        }

        public new void InvokeStatusChecked(BuildStatus[] args) {
            base.InvokeStatusChecked(args);
        }
    }
}
