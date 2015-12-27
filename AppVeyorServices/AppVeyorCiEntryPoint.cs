using System.ComponentModel.Composition;
using AppVeyorServices.ServerConfiguration;
using SirenOfShame.Lib;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace AppVeyorServices
{
    [Export(typeof (ICiEntryPoint))]
    public class AppVeyorCiEntryPoint : ICiEntryPoint
    {
        internal const string CiName = "AppVeyor";
        private const string AppVeyorApiBaseUrl = "https://ci.appveyor.com/api";
        internal const string AppVeyorUiBaseUrl = "https://ci.appveyor.com";

        public ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings,
            CiEntryPointSetting ciEntryPointSetting)
        {
            ciEntryPointSetting.Url = AppVeyorApiBaseUrl;
            return new ConfigureAppVeyor(settings, this, ciEntryPointSetting);
        }

        public string Name
        {
            get { return CiName; }
        }

        public string DisplayName
        {
            get { return CiName; }
        }

        public WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            return new AppVeyorWatcher(settings, this);
        }
    }
}