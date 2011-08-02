using System.ComponentModel.Composition;
using SirenOfShame.Lib;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using TfsServices.Configuration;

namespace TfsServices
{
    [Export(typeof(ICiEntryPoint))]
    public class TfsCiEntryPoint : ICiEntryPoint
    {
        public ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting)
        {
            return new ConfigureTfs(settings, this, ciEntryPointSetting);
        }

        public string Name
        {
            get { return "TFS"; }
        }

        public WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            return new TfsWatcher(settings, this);
        }
    }
}
