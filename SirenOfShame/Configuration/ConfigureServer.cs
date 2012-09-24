using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
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

        public static bool Show(SirenOfShameSettings settings, CiEntryPointSetting ciEntryPointSetting)
        {
            ConfigureServer configureServer = new ConfigureServer(settings, ciEntryPointSetting);
            var anyChanges = configureServer.ShowDialog() != DialogResult.Cancel;
            return anyChanges;
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
            if (_adding)
            {
                _serverType.DataSource = ciEntryPoints;
            }
            else
            {
                SetServerType(ciEntryPointSetting.GetCiEntryPoint(settings));
            }

            _add.Text = _adding ? "Add" : "Update";
            _cancel.Visible = _adding;
        }

        public SirenOfShameSettings Settings { get; set; }

        private void ClearConfigurePanel()
        {
            _configurationContainer.Controls.Cast<Control>().ToList().ForEach(c => c.Dispose());
        }

        private void ServerTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            var newServerType = (ICiEntryPoint)_serverType.SelectedItem;
            SetServerType(newServerType);
        }

        private void SetServerType(ICiEntryPoint newServerType)
        {
            ClearConfigurePanel();
            var configureServerControl = newServerType.CreateConfigurationWindow(Settings, _ciEntryPointSetting);
            configureServerControl.BackColor = Color.Transparent;
            configureServerControl.ForeColor = Color.White;
            _configurationContainer.Controls.Add(configureServerControl);
            _ciEntryPointSetting.Name = newServerType.Name;
        }

        private void CloseClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
            Dispose();
        }

        private void AddClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
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
