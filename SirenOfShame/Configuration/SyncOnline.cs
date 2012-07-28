using System;
using System.Diagnostics;
using System.Windows.Forms;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Configuration
{
    public partial class SyncOnline : UserControl
    {
        private readonly SirenOfShameSettings _settings;

        private bool _initializing = true;
        
        public SyncOnline(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();
            InitializeSosOnlineSection();
            InitializeProxySection();
            InitializeRadioButtons();
            _settings.InitializeUserIAm(_userIAm);
            _initializing = false;
        }

        private void InitializeProxySection()
        {
            bool proxyInfoExists = !string.IsNullOrEmpty(_settings.SosOnlineProxyUrl);
            _useProxy.Checked = proxyInfoExists;
            MakeProxySectionVisible(proxyInfoExists);
            _proxyUrl.Text = _settings.SosOnlineProxyUrl;
            _proxyUsername.Text = _settings.SosOnlineProxyUsername;
            _proxyPassword.Text = _settings.GetSosOnlineProxyPassword();
        }

        private void InitializeRadioButtons()
        {
            _syncAlways.Checked = _settings.SosOnlineAlwaysSync;
            _syncNever.Checked = !_settings.SosOnlineAlwaysSync;
            _syncAlways.Enabled = _settings.SosOnlineAlwaysSync;
        }

        private void InitializeSosOnlineSection()
        {
            _sosOnlineLogin.Text = _settings.SosOnlineUsername;
            _sosOnlinePassword.Text = _settings.GetSosOnlinePassword();
            // todo: set sos online status correctly
            bool hasEverConnected = _settings.SosOnlineHighWaterMark != null;
            _sosOnlineStatus.Text = hasEverConnected ? "Ready to sync" : "Have never synced";
        }

        private void SaveUserIAm()
        {
            string myRawName = Settings.UserIamIsUnselected(_userIAm) ? null : ((PersonSetting)_userIAm.SelectedItem).RawName;
            _settings.MyRawName = myRawName;
        }

        private void SaveSosOnlineSettings()
        {
            _settings.SosOnlineUsername = _sosOnlineLogin.Text;
            _settings.SetSosOnlinePassword(_sosOnlinePassword.Text);
            _settings.SosOnlineProxyUrl = _proxyUrl.Text;
            _settings.SosOnlineProxyUsername = _proxyUsername.Text;
            _settings.SetSosOnlineProxyPassword(_proxyPassword.Text);
        }

        private void OnAddBuildsSuccess(DateTime sosOnlineHighWaterMark)
        {
            _settings.SosOnlineHighWaterMark = sosOnlineHighWaterMark.Ticks;
            ManualSyncComplete("Successfully sync'd", authenticatedSuccessfully: true);
        }

        private void VerifyCredentialsClick(object sender, EventArgs e)
        {
            SaveUserIAm();
            SaveSosOnlineSettings();
            if (string.IsNullOrEmpty(_settings.MyRawName))
            {
                SosMessageBox.Show("Who Am I?", "Please select which user you are from the 'I Am' textbox so we know which records to export", "Fine");
                return;
            }

            var sosOnlineService = new SosOnlineService();
            SaveSosOnlineSettings();
            _loading.Visible = true;
            sosOnlineService.VerifyCredentialsAsync(_settings, OnVerifyCredentialsSuccess, OnSosOnlineFailure);
            _sosOnlineStatus.Text = "Logging in ...";
        }

        private void OnSosOnlineFailure(string userFriendlyErrorMessage, ServerUnavailableException ex)
        {
            _loading.Visible = false;
            SosMessageBox.Show("Error connecting", userFriendlyErrorMessage, "Hmmmm");
            _sosOnlineStatus.Text = userFriendlyErrorMessage;
        }

        private void OnVerifyCredentialsSuccess()
        {
            _sosOnlineStatus.Text = "Login success, performing sync";

            var sosDb = new SosDb();
            var exportedBuilds = sosDb.ExportNewBuilds(_settings);
            if (exportedBuilds == null)
            {
                ManualSyncComplete("No new builds to export", authenticatedSuccessfully: true);
                return;
            }
            string exportedAchievements = _settings.ExportNewAchievements();
            var sosOnlineService = new SosOnlineService();
            sosOnlineService.Synchronize(_settings, exportedBuilds, exportedAchievements, OnAddBuildsSuccess, OnSosOnlineFailure);
        }

        private void ManualSyncComplete(string status, bool authenticatedSuccessfully)
        {
            _sosOnlineStatus.Text = status;
            _loading.Visible = false;
            if (authenticatedSuccessfully)
            {
                _settings.SosOnlineAlwaysSync = true;
                _settings.Save();
                InitializeRadioButtons();
            }
        }

        private void CreateAccountLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(SosOnlineService.SOS_URL + "/Account/Register");
        }

        private void SyncNeverCheckedChanged(object sender, EventArgs e)
        {
            SyncRadioChanged();
        }

        private void SyncRadioChanged()
        {
            if (_initializing) return;
            _settings.SosOnlineAlwaysSync = _syncAlways.Checked;
            _settings.Save();
        }
        
        private void SyncAlwaysCheckedChanged(object sender, EventArgs e)
        {
            SyncRadioChanged();
        }

        private void _sosOnlineLogin_TextChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            DisableSyncAlways();
        }

        private void DisableSyncAlways()
        {
            _settings.SosOnlineAlwaysSync = false;
            InitializeRadioButtons();
        }

        private void _sosOnlinePassword_TextChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            DisableSyncAlways();
        }

        private void UseProxyCheckedChanged(object sender, EventArgs e)
        {
            if (!_useProxy.Checked)
            {
                _proxyUrl.Text = null;
                _settings.SosOnlineProxyUrl = null;
                _settings.Save();
            }
            MakeProxySectionVisible(_useProxy.Checked);
        }

        private void MakeProxySectionVisible(bool proxyInfoExists)
        {
            _proxyUrl.Visible = proxyInfoExists;
            _proxyUrlLabel.Visible = proxyInfoExists;
            _proxyUsername.Visible = proxyInfoExists;
            _proxyUsernameLabel.Visible = proxyInfoExists;
            _proxyPassword.Visible = proxyInfoExists;
            _proxyPasswordLabel.Visible = proxyInfoExists;
        }
    }
}
