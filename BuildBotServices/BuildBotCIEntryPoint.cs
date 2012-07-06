using System.ComponentModel.Composition;
using BuildBotServices.ServerConfiguration;
using SirenOfShame.Lib;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace BuildBotServices
{
    [Export(typeof(ICiEntryPoint))]
    public class BuildBotCIEntryPoint : ICiEntryPoint
    {
        public ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting)
        {
            return new ConfigureBuildBot(settings, this, ciEntryPointSetting);
        }

        public string Name
        {
            get { return "Buildbot"; }
        }

        public string DisplayName
        {
            get { return "Buildbot"; }
        }

        public WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            return new BuildBotWatcher(settings, this);
        }
    }
}
