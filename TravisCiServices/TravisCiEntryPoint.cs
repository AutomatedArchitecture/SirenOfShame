using System.ComponentModel.Composition;
using SirenOfShame.Lib;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using TravisCiServices.ServerConfiguration;

namespace TravisCiServices
{
    [Export(typeof(ICiEntryPoint))]
    public class TravisCiEntryPoint : ICiEntryPoint
    {
        public ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting)
        {
            return new ConfigureTravisCi(settings, this, ciEntryPointSetting);
        }

        public string Name
        {
            get { return "TravisCi"; }
        }

        public string DisplayName
        {
            get { return "Travis CI"; }
        }

        public WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            return new TravisCiWatcher(settings, this);
        }
    }
}
