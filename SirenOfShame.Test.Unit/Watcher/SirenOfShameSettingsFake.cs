using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Test.Unit.Watcher
{
    public class SirenOfShameSettingsFake : SirenOfShameSettings {
        private readonly WatcherFake _watcherFake;
        
        public SirenOfShameSettingsFake() {
            _watcherFake = new WatcherFake(this);
        }

        public WatcherFake WatcherFake {
            get { return _watcherFake; }
        }
        
        public override void Save()
        {
            // do nothing
        }
    }
}
