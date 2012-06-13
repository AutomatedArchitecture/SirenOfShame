using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Configuration
{
    public partial class Settings : FormBase
    {
        private readonly SirenOfShameSettings _settings;

        public Settings(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();

            InitializePollIntervalSection();
            InitializeUpdateLocationSection();
            InitializeReputationAndAchievementSection();
            InitializeSosOnlineSection();

            _viewLog.Enabled = Program.Form.CanViewLogs;
        }

        private void InitializeSosOnlineSection()
        {
            _sosOnlineLogin.Text = _settings.SosOnlineUsername;
            _sosOnlinePassword.Text = _settings.GetSosOnlinePassword();
            // todo: set sos online status correctly
            _sosOnlineStatus.Text = "Have never synced";
        }

        private void InitializeReputationAndAchievementSection()
        {
            _hideReputation.Checked = _settings.HideReputation;
            InitializeAchievementAlertPreferences();
            InitializeUserIAm();
        }

        private void InitializePollIntervalSection()
        {
            _pollInterval.Value = _settings.PollInterval;
            RefreshDurationText();
        }

        private void InitializeUpdateLocationSection()
        {
            _updateLocationAuto.Checked = _settings.UpdateLocation == UpdateLocation.Auto;
            _updateLocationOther.Checked = _settings.UpdateLocation == UpdateLocation.Other;
            _updateLocationNever.Checked = _settings.UpdateLocation == UpdateLocation.Never;
            _updateLocationOtherLocation.Text = _settings.UpdateLocation == UpdateLocation.Other
                                                    ? _settings.UpdateLocationOther
                                                    : "";
        }

        private void InitializeUserIAm()
        {
            _userIAm.Items.Add("");
            foreach (var personInProject in _settings.People)
            {
                _userIAm.Items.Add(personInProject);
            }
            if (!string.IsNullOrEmpty(_settings.MyRawName))
            {
                foreach (var item in _userIAm.Items)
                {
                    var personSetting = item as PersonSetting;
                    if (personSetting != null && personSetting.RawName == _settings.MyRawName)
                    {
                        _userIAm.SelectedItem = item;
                    }
                }
            }
        }

        private void InitializeAchievementAlertPreferences()
        {
            if (_settings.AchievementAlertPreference == AchievementAlertPreferenceEnum.Always)
            {
                _alwaysShowNewAchievements.Checked = true;
            }
            if (_settings.AchievementAlertPreference == AchievementAlertPreferenceEnum.Never)
            {
                _neverShowAchievements.Checked = true;
            }
            if (_settings.AchievementAlertPreference == AchievementAlertPreferenceEnum.OnlyForMe)
            {
                _onlyShowMyAchievements.Checked = true;
            }
        }

        private UpdateLocation GetUpdateLocation()
        {
            if (_updateLocationAuto.Checked)
            {
                return UpdateLocation.Auto;
            }
            if (_updateLocationOther.Checked)
            {
                return UpdateLocation.Other;
            }
            if (_updateLocationNever.Checked)
            {
                return UpdateLocation.Never;
            }
            throw new Exception("One of the update locations needs to be checked");
        }

        private void OkClick(object sender, EventArgs e)
        {
            _settings.PollInterval = _pollInterval.Value;
            UpdateLocation updateLocation = GetUpdateLocation();
            _settings.UpdateLocationOther = null;
            _settings.UpdateLocationOther = _updateLocationOtherLocation.Text;
            _settings.UpdateLocation = updateLocation;

            _settings.HideReputation = _hideReputation.Checked;

            SetShowAchievements();
            SetUserIAm();

            _settings.Save();
            Close();
        }

        private void SetUserIAm()
        {
            string myRawName = _userIAm.SelectedItem as string == "" ? null : ((PersonSetting) _userIAm.SelectedItem).RawName;
            _settings.MyRawName = myRawName;
        }

        private void SetShowAchievements()
        {
            if (_alwaysShowNewAchievements.Checked)
            {
                _settings.AchievementAlertPreference = AchievementAlertPreferenceEnum.Always;
            }
            if (_neverShowAchievements.Checked)
            {
                _settings.AchievementAlertPreference = AchievementAlertPreferenceEnum.Never;
            }
            if (_onlyShowMyAchievements.Checked)
            {
                _settings.AchievementAlertPreference = AchievementAlertPreferenceEnum.OnlyForMe;
            }
        }

        private void CancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void PollIntervalValueChanged(object sender, EventArgs e)
        {
            RefreshDurationText();
        }

        private void RefreshDurationText()
        {
            string snideComment = ".";
            if (_pollInterval.Value <= 5)
                snideComment = ". (yes, I hate my sys admin that much)";
            if (_pollInterval.Value <= 1)
                snideComment = ". (yes, I really hate my co-workers that much, but s'alright, will blame the authors of this app)";
            _duration.Text = string.Format("{0} seconds{1}", _pollInterval.Value, snideComment);
        }

        private void ViewLogClick(object sender, EventArgs e)
        {
            Program.Form.ViewLogs();
        }

        private void UpdateLocationOtherCheckedChanged(object sender, EventArgs e)
        {
            _updateLocationOtherLocation.Enabled = _updateLocationOther.Checked;
        }

        private void CheckForUpdatesClick(object sender, EventArgs e)
        {
            Program.Form.CheckForUpdates();
        }

        private void RecalculateClick(object sender, EventArgs e)
        {
            FindOldAchievements.TryFindOldAchievements(_settings);
        }

        private void CreateAccountLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(SOS_URL + "/Account/Register");
        }

        private void ViewLeaderboardsLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // todo: put in correct URL
            Process.Start(SOS_URL + "/Leaders");
        }

        private void ResyncClick(object sender, EventArgs e)
        {
            var sosDb = new SosDb();
            var exportedBuilds = sosDb.ExportNewBuilds(_settings);
            // todo: Push exported builds to server
        }

        private const string SOS_URL = "http://localhost:3115";
        const string AUTHENTICATION_SUCCESS = "success";
        
        private void VerifyCredentialsClick(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.UploadValuesCompleted += (s, uploadEventArgs) =>
            {
                // todo: error handeling when authenticating + abstract this into a service
                byte[] result = uploadEventArgs.Result;
                string resultAsStr = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
                if (resultAsStr == AUTHENTICATION_SUCCESS)
                {
                    _resync.Enabled = true;
                    _settings.SosOnlineUsername = _sosOnlineLogin.Text;
                    _settings.SetSosOnlinePassword(_sosOnlinePassword.Text);
                    _sosOnlineStatus.Text = "Login success";
                }
                else
                {
                    SosMessageBox.Show("Error connecting", resultAsStr, "Hmmmm");
                    _sosOnlineStatus.Text = resultAsStr;
                }
            };

            NameValueCollection data = new NameValueCollection();
            data["UserName"] = _sosOnlineLogin.Text;
            data["Password"] = _sosOnlinePassword.Text;
            webClient.UploadValuesAsync(new Uri(SOS_URL + "/api/VerifyCredentials"), "POST", data);
            _sosOnlineStatus.Text = "Logging in ...";
        }
    }
}
