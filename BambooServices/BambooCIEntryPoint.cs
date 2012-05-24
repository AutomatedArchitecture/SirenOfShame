using System.ComponentModel.Composition;
using BambooServices.ServerConfiguration;
using SirenOfShame.Lib;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace BambooServices
{
    [Export(typeof(ICiEntryPoint))]
    public class BambooCIEntryPoint : ICiEntryPoint
    {
        public ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting)
        {
            return new ConfigureBamboo(settings, this, ciEntryPointSetting);
        }

        public string Name
        {
            get { return "Bamboo"; }
        }

        public string DisplayName
        {
            get { return "Bamdoo"; }
        }

        public WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            return new BambooWatcher(settings, this);
        }
    }
}
