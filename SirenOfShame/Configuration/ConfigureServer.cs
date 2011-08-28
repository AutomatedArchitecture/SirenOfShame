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
    public partial class ConfigureServer : FormBase
    {
        [ImportMany(typeof(ICiEntryPoint))]
        public List<ICiEntryPoint> CIEntryPoints { get; set; }

        private readonly CiEntryPointSetting _ciEntryPointSetting;

        public static void Show(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting)
        {
            ConfigureServer configureServer = new ConfigureServer(settings, ciEntryPointSetting);
            configureServer.ShowDialog();
        }

        private bool _adding;
        
        public ConfigureServer(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting)
        {
            _adding = ciEntryPointSetting == null;
            if (_adding)
            {
                ciEntryPointSetting = new CiEntryPointSetting();
            }
            
            Settings = settings;
            _ciEntryPointSetting = ciEntryPointSetting;

            IocContainer.Instance.Compose(this);

            InitializeComponent();
            
            _ciServerPanel.Visible = _adding;
            ICiEntryPoint[] ciEntryPoints = CIEntryPoints.ToArray();
            _serverType.DataSource = ciEntryPoints;

            _add.Text = _adding ? "Add" : "Update";
            _cancel.Visible = _adding;

            _initializing = false;
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
            _ciEntryPointSetting.Name = newServerType.Name;

            if (_initializing) return;
        }

        private void CloseClick(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void AddClick(object sender, EventArgs e)
        {
            if (_ciEntryPointSetting.BuildDefinitionSettings.Count < 1)
            {
                SosMessageBox.Show(
                    "Select More Stuff", 
                    "Please select at least one build definition", 
                    "Sorry, I'm a Manager, This Stuff Is Complicated");
                return;
            }
            if (_adding)
            {
                 Settings.CiEntryPointSettings.Add(_ciEntryPointSetting);
            }
            Settings.Save();
            Close();
            Dispose();
        }
    }
}
