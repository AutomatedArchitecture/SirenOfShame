using System.Collections.Generic;
using SirenOfShame.Lib.Exceptions;
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

        protected override IList<BuildStatus> GetBuildStatus()
        {
            return new List<BuildStatus>();
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

        public new void InvokeServerUnavailable(ServerUnavailableException ex)
        {
            base.InvokeServerUnavailable(ex);
        }

        public new void InvokeStatusChecked(IList<BuildStatus> args) {
            base.InvokeStatusChecked(args);
        }

        public void InvokeStoppedWatching() {
            OnStoppedWatching();
        }
    }
}
