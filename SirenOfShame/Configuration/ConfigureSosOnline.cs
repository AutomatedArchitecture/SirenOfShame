using System;
using System.Diagnostics;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class ConfigureSosOnline : Form
    {
        private readonly SirenOfShameSettings _settings;

        public ConfigureSosOnline(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();
            _alwaysOffline.Checked = _settings.SosOnlineAlwaysOffline;
            LoadChildControl();
        }

        private void ViewLeaderboardsLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(SosOnlineService.SOS_URL + "/Leaders");
        }

        private void AlwaysOfflineCheckedChanged(object sender, EventArgs e)
        {
            LoadChildControl();
            _settings.SosOnlineAlwaysOffline = _alwaysOffline.Checked;
            _settings.Save();
        }

        private void LoadChildControl()
        {
            panel1.ClearAndDispose();
            var childControl = GetChildControl();
            childControl.Dock = DockStyle.Fill;
            panel1.Controls.Add(childControl);
        }

        private UserControl GetChildControl()
        {
            return _alwaysOffline.Checked ? (UserControl)new SyncOffline(_settings) : new SyncOnline(_settings);
        }
    }
}
