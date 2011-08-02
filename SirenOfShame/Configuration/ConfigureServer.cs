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

        private CiEntryPointSetting _ciEntryPointSetting;

        public static void Show(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting)
        {
            ConfigureServer configureServer = new ConfigureServer { Settings = settings };
            IocContainer.Instance.Compose(configureServer);
            configureServer.Settings = settings;

            configureServer._serverType.DisplayMember = "Name";
            ICiEntryPoint[] ciEntryPoints = configureServer.CIEntryPoints.ToArray();
            configureServer._serverType.DataSource = ciEntryPoints;

            bool adding = ciEntryPointSetting == null;
            configureServer._ciServerPanel.Visible = adding;
            if (adding)
            {
                var newCiEntryPointSetting = new CiEntryPointSetting();
                configureServer._ciEntryPointSetting = newCiEntryPointSetting;
                settings.CiEntryPointSettings.Add(newCiEntryPointSetting);
                settings.Save();
            } else
            {
                configureServer._ciEntryPointSetting = ciEntryPointSetting;
            }

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
            var configureServerControl = newServerType.CreateConfigurationWindow(Settings, _ciEntryPointSetting);
            _configurationContainer.Controls.Add(configureServerControl);

            if (_initializing) return;
        }

        private void CloseClick(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }
    }
}
