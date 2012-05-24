using System.ComponentModel.Composition;
using SirenOfShame.Lib;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using TeamCityServices.ServerConfiguration;
using TeamCityServices.Watcher;

namespace TeamCityServices
{
    [Export(typeof(ICiEntryPoint))]
    public class TeamCityCiEntryPoint : ICiEntryPoint
    {
        public ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting)
        {
            return new ConfigureTeamCity(settings, this, ciEntryPointSetting);
        }

        public string Name
        {
            get { return "Team City"; }
        }

        public string DisplayName
        {
            get { return "Team City"; }
        }

        public WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            return new TeamCityWatcher(settings, this);
        }
    }
}
