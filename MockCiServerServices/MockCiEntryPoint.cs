using System.ComponentModel.Composition;
using SirenOfShame.Lib;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace MockCiServerServices
{
    [Export(typeof(ICiEntryPoint))]
    public class MockCiEntryPoint : ICiEntryPoint
    {
        public ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting)
        {
            return new ConfigureMock(settings, this, ciEntryPointSetting);
        }

        public string Name
        {
            get { return "Mock"; }
        }

        public string DisplayName
        {
            get { return "Mock"; }
        }

        public WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            return new MockWatcher(settings, this);
        }
    }
}
