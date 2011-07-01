using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib
{
    public interface ICiEntryPoint
    {
        ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings);
        string Name { get; }
        WatcherBase GetWatcher(SirenOfShameSettings settings);
    }
}
