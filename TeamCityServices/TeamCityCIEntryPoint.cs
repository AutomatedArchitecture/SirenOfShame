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
        public ConfigureServerBase CreateConfigurationWindow(SirenOfShameSettings settings)
        {
            return new ConfigureTeamCity(settings, this);
        }

        public string Name
        {
            get { return "Team City"; }
        }

        public WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            return new TeamCityWatcher(settings, this);
        }
    }
}
