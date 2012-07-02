using System;
using System.Diagnostics;
using System.Windows.Forms;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Configuration
{
    public partial class ConfigureSosOnline : Form
    {
        private readonly SirenOfShameSettings _settings;

        public ConfigureSosOnline(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();
            
            InitializeSosOnlineSection();
            _settings.InitializeUserIAm(_userIAm);
        }

        private void InitializeSosOnlineSection()
        {
            _sosOnlineLogin.Text = _settings.SosOnlineUsername;
            _sosOnlinePassword.Text = _settings.GetSosOnlinePassword();
            // todo: set sos online status correctly
            bool hasEverConnected = _settings.SosOnlineHighWaterMark != null;
            _sosOnlineStatus.Text = hasEverConnected ? "Ready to sync" : "Have never synced";
        }

        private void SetUserIAm()
        {
            string myRawName = Settings.UserIamIsUnselected(_userIAm) ? null : ((PersonSetting)_userIAm.SelectedItem).RawName;
            _settings.MyRawName = myRawName;
        }

        private void SaveSosOnlineSettings()
        {
            _settings.SosOnlineUsername = _sosOnlineLogin.Text;
            _settings.SetSosOnlinePassword(_sosOnlinePassword.Text);
        }

        private void ResyncClick(object sender, EventArgs e)
        {
            SetUserIAm();
            SaveSosOnlineSettings();
            if (string.IsNullOrEmpty(_settings.MyRawName))
            {
                SosMessageBox.Show("Who Am I?", "Please select which user you are from the 'I Am' textbox so we know which records to export", "Fine");
                return;
            }
            var sosDb = new SosDb();
            var exportedBuilds = sosDb.ExportNewBuilds(_settings);
            if (exportedBuilds == null)
            {
                _sosOnlineStatus.Text = "No new builds to export";
                return;
            }
            var sosOnlineService = new SosOnlineService();
            string exportedAchievements = _settings.ExportNewAchievements();
            sosOnlineService.Synchronize(_settings, exportedBuilds, exportedAchievements, OnAddBuildsSuccess, OnSosOnlineFailure);
        }

        private void CreateAccountLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(SosOnlineService.SOS_URL + "/Account/Register");
        }

        private void ViewLeaderboardsLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(SosOnlineService.SOS_URL + "/Leaders");
        }

        private void OnAddBuildsSuccess(DateTime sosOnlineHighWaterMark)
        {
            _settings.SosOnlineHighWaterMark = sosOnlineHighWaterMark.Ticks;
            _settings.Save();
            _sosOnlineStatus.Text = "Successfully sync'd";
        }

        private void VerifyCredentialsClick(object sender, EventArgs e)
        {
            var sosOnlineService = new SosOnlineService();
            SaveSosOnlineSettings();
            sosOnlineService.VerifyCredentialsAsync(_settings, OnVerifyCredentialsSuccess, OnSosOnlineFailure);
            _sosOnlineStatus.Text = "Logging in ...";
        }

        private void OnSosOnlineFailure(string userFriendlyErrorMessage, ServerUnavailableException ex)
        {
            SosMessageBox.Show("Error connecting", userFriendlyErrorMessage, "Hmmmm");
            _sosOnlineStatus.Text = userFriendlyErrorMessage;
        }

        private void OnVerifyCredentialsSuccess()
        {
            _resync.Enabled = true;
            _sosOnlineStatus.Text = "Login success";
        }

        private void OkClick(object sender, EventArgs e)
        {
            SetUserIAm();
        }
    }
}
