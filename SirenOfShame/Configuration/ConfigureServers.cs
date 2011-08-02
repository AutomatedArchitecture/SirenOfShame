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
        }

        public static void Show(SirenOfShameSettings settings)
        {
            ConfigureServers configureServers = new ConfigureServers(settings);
            configureServers.ShowDialog();
        }

        private void AddClick(object sender, System.EventArgs e)
        {
            ConfigureServer.Show(_settings, null);
        }

        private void _close_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
