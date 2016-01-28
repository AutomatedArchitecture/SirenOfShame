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
        private const string CI_NAME = @"AppVeyor";
        private const string APP_VEYOR_API_BASE_URL = @"https://ci.appveyor.com/api";
        internal const string APP_VEYOR_UI_BASE_URL = @"https://ci.appveyor.com";

        public ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings,
            CiEntryPointSetting ciEntryPointSetting)
        {
            ciEntryPointSetting.Url = APP_VEYOR_API_BASE_URL;
            return new ConfigureAppVeyor(settings, this, ciEntryPointSetting);
        }

        public string Name
        {
            get { return CI_NAME; }
        }

        public string DisplayName
        {
            get { return CI_NAME; }
        }

        public WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            return new AppVeyorWatcher(settings, this);
        }
    }
}