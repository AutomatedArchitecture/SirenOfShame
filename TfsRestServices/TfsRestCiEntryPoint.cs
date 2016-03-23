using System.ComponentModel.Composition;
using SirenOfShame.Lib;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using TfsRestServices.ServerConfiguration;

namespace TfsRestServices
{
    [Export(typeof(ICiEntryPoint))]
    public class TfsRestCiEntryPoint : ICiEntryPoint
    {
        public ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting)
        {
            return new ConfigureTfsRest(settings, this, ciEntryPointSetting);
        }

        public string Name => "TfsRest";

        public string DisplayName => "Microsoft TFS 2015+";

        public WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            return new TfsRestWatcher(settings, this);
        }
    }
}
