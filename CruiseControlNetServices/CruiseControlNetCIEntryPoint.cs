using System.ComponentModel.Composition;
using CruiseControlNetServices.ServerConfiguration;
using SirenOfShame.Lib;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace CruiseControlNetServices
{
    [Export(typeof(ICiEntryPoint))]
    public class CruiseControlNetCIEntryPoint : ICiEntryPoint
    {
        public ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting)
        {
            return new ConfigureCruiseControlNet(settings, this, ciEntryPointSetting);
        }

        public string Name
        {
            get { return "CruiseControl.Net"; }
        }

        public string DisplayName
        {
            get { return "CruiseControl.Net"; }
        }

        public WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            return new CruiseControlNetWatcher(settings, this);
        }
    }
}
