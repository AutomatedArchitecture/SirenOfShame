using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib
{
    public interface ICiEntryPoint
    {
        ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting);
        string Name { get; }
        string DisplayName { get; }
        WatcherBase GetWatcher(SirenOfShameSettings settings);
    }
}
