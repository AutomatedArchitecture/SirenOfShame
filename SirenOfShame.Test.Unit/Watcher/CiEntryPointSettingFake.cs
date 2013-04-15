using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    public class CiEntryPointSettingFake : CiEntryPointSetting
    {
        private readonly WatcherFake _watcherFake;

        public CiEntryPointSettingFake(SirenOfShameSettings settings)
        {
            _watcherFake = new WatcherFake(settings);
            Url = "http://fake";
        }

        public WatcherFake WatcherFake
        {
            get { return _watcherFake; }
        }

        public override WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            return _watcherFake;
        }
    }
}