using System.Linq;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class ConfigureServers : FormBase
    {
        private readonly SirenOfShameSettings _settings;

        public ConfigureServers(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();
            _servers.DataSource = settings.CiEntryPointSettings;
            _servers.DisplayMember = "Url";
        }

        public static void Show(SirenOfShameSettings settings)
        {
            ConfigureServers configureServers = new ConfigureServers(settings);
            if (!settings.CiEntryPointSettings.Any())
            {
                 configureServers.AddServer();
            } else
            {
                configureServers.ShowDialog();
            }
        }

        private void AddClick(object sender, System.EventArgs e)
        {
            AddServer();
        }

        private void AddServer()
        {
            ConfigureServer.Show(_settings, null);
            Close();
        }

        private void CloseClick(object sender, System.EventArgs e)
        {
            Close();
        }

        private void ServersDoubleClick(object sender, System.EventArgs e)
        {
            if (_servers.SelectedItem != null)
            {
                var ciEntryPointSetting = (CiEntryPointSetting) _servers.SelectedItem;
                ConfigureServer.Show(_settings, ciEntryPointSetting);
                Close();
            }
        }

        private void DeleteClick(object sender, System.EventArgs e)
        {
            if (_servers.SelectedItem != null)
            {
                var ciEntryPointSetting = (CiEntryPointSetting) _servers.SelectedItem;
                _settings.CiEntryPointSettings.Remove(ciEntryPointSetting);
                _settings.Save();
                Close();
            }
        }
    }
}