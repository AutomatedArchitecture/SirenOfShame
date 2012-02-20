using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class ConfigureServers : FormBase
    {
        private readonly SirenOfShameSettings _settings;
        private bool _dirty = false;

        public ConfigureServers(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();
            _servers.DataSource = settings.CiEntryPointSettings;
            _servers.DisplayMember = "Url";
        }

        public static bool Show(SirenOfShameSettings settings)
        {
            ConfigureServers configureServers = new ConfigureServers(settings);
            if (!settings.CiEntryPointSettings.Any())
            {
                configureServers.AddServer();
                return true;
            }
            bool anyChanges = configureServers.ShowDialog() == DialogResult.OK;
            return anyChanges;
        }

        private void AddClick(object sender, System.EventArgs e)
        {
            AddServer();
        }

        private void AddServer()
        {
            bool anyChanges = ConfigureServer.Show(_settings, null);
            _dirty = anyChanges;
            Close();
        }

        private void ServersDoubleClick(object sender, System.EventArgs e)
        {
            if (_servers.SelectedItem != null)
            {
                var ciEntryPointSetting = (CiEntryPointSetting)_servers.SelectedItem;
                bool anyChanges = ConfigureServer.Show(_settings, ciEntryPointSetting);
                _dirty = anyChanges;
                Close();
            }
        }

        private void DeleteClick(object sender, System.EventArgs e)
        {
            if (_servers.SelectedItem != null)
            {
                _dirty = true;
                var ciEntryPointSetting = (CiEntryPointSetting)_servers.SelectedItem;
                _settings.CiEntryPointSettings.Remove(ciEntryPointSetting);
                _settings.Save();
                Close();
            }
        }

        private void ConfigureServers_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = _dirty ? DialogResult.OK : DialogResult.Cancel;
        }
    }
}