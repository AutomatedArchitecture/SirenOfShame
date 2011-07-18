using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class ConfigureServer : Form
    {
        [ImportMany(typeof(ICiEntryPoint))]
        public List<ICiEntryPoint> CIEntryPoints { get; set; }

        public static void Show(SirenOfShameSettings settings)
        {
            ConfigureServer configureServer = new ConfigureServer { Settings = settings };
            IocContainer.Instance.Compose(configureServer);
            configureServer.Settings = settings;

            configureServer._serverType.DisplayMember = "Name";
            ICiEntryPoint[] ciEntryPoints = configureServer.CIEntryPoints.ToArray();
            configureServer._serverType.DataSource = ciEntryPoints;

            if (string.IsNullOrEmpty(settings.ServerType))
            {
                settings.ServerType = ciEntryPoints.First().Name;
                settings.Save();
            }
            configureServer._serverType.SelectedIndex = ciEntryPoints.Select((cep, i) => new {cep, i}).First(i => i.cep.Name == settings.ServerType).i;

            configureServer._initializing = false;

            configureServer.ShowDialog();
        }

        public ConfigureServer()
        {
            InitializeComponent();
        }

        private bool _initializing = true;
        public SirenOfShameSettings Settings { get; set; }

        private void ClearConfigurePanel()
        {
            _configurationContainer.Controls.Cast<Control>().ToList().ForEach(c => c.Dispose());
        }

        private void ServerTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            var newServerType = (ICiEntryPoint)_serverType.SelectedItem;

            ClearConfigurePanel();
            var configureServerControl = newServerType.CreateConfigurationWindow(Settings);
            _configurationContainer.Controls.Add(configureServerControl);

            if (_initializing) return;

            Settings.ServerType = newServerType.Name;
            Settings.Save();
        }

        private void CloseClick(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }
    }
}
