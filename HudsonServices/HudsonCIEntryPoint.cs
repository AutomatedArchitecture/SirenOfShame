
using System.ComponentModel.Composition;
using HudsonServices.ServerConfiguration;
using SirenOfShame.Lib;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace HudsonServices
{
    [Export(typeof(ICiEntryPoint))]
    public class HudsonCIEntryPoint : ICiEntryPoint
    {
        public ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting)
        {
            return new ConfigureHudson(settings, this, ciEntryPointSetting);
        }

        public string Name
        {
            get { return "Hudson/Jenkins"; }
        }

        public WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            return new HudsonWatcher(settings, this);
        }
    }
}
