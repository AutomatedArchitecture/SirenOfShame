using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using log4net;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class Settings : FormBase
    {
        private readonly SirenOfShameSettings _settings;
        private readonly ILog _log = MyLogManager.GetLogger(typeof(Settings));

        public Settings(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();

            InitializePollIntervalSection();
            InitializeUpdateLocationSection();
            InitializeReputationAndAchievementSection();
            InitializeMiscSection();

            _viewLog.Enabled = Program.Form.CanViewLogs;
        }

        private void InitializeReputationAndAchievementSection()
        {
            InitializeAchievementAlertPreferences();
            _settings.InitializeUserIAm(_userIAm);
        }

        private void InitializePollIntervalSection()
        {
            _pollInterval.Value = _settings.PollInterval;
            RefreshDurationText();
        }

        private void InitializeMiscSection()
        {
            _alwaysOnTop.Checked = _settings.AlwaysOnTop;
            _startInFullscreen.Checked = _settings.StartInFullScreen;
            _showLeaders.Checked = _settings.ShowLeaders;
            _showNewsfeed.Checked = _settings.ShowNewsfeed;
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

            SetShowAchievements();
            _settings.SaveUserIAm(_userIAm);
            SetMiscSection();

            _settings.Save();
            Close();
        }

        private void SetMiscSection()
        {
            _settings.AlwaysOnTop = _alwaysOnTop.Checked;
            _settings.StartInFullScreen = _startInFullscreen.Checked;
            _settings.ShowNewsfeed = _showNewsfeed.Checked;
            _settings.ShowLeaders = _showLeaders.Checked;
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

        private void ResetReputationClick(object sender, EventArgs e)
        {
            var resetReputation = new ResetReputation(_settings);
            resetReputation.ShowDialog();
        }

        private async void ImportFromAd_Click(object sender, EventArgs e)
        {
            try
            {
                _errorMessage.Text = "Importing ...";
                _errorMessage.Visible = true;

                await Task.Factory.StartNew(ImportFromAd);
            }
            catch (Exception ex)
            {
                _log.Error("Error importing from active directory", ex);
                _errorMessage.Text = ex.Message;
            }
        }

        private void ImportFromAd()
        {
            var activeDirectoryService = new ActiveDirectoryService();
            var avatarsFolder = SirenOfShameSettings.GetAvatarsFolder();
            foreach (var personSetting in _settings.People)
            {
                ImportPersonFromAdContinueOnError(personSetting, activeDirectoryService, avatarsFolder);
            }
            _settings.Save();
            _errorMessage.Visible = false;
        }

        private void ImportPersonFromAdContinueOnError(PersonSetting personSetting, ActiveDirectoryService activeDirectoryService, string avatarsFolder)
        {
            try
            {
                ImportPersonFromAd(personSetting, activeDirectoryService, avatarsFolder);
            }
            catch (COMException ex)
            {
                if (ex.Message == "Unknown error (0x80005000)")
                {
                    throw;
                }
                if (ex.Message == "The server is not operational.")
                {
                    throw;
                }
                _log.Warn("Failed to import user " + personSetting.RawName + ", continuing import", ex);
            }
            catch (Exception ex)
            {
                _log.Warn("Failed to import user " + personSetting.RawName + ", continuing import", ex);
            }
        }

        private void ImportPersonFromAd(PersonSetting personSetting, ActiveDirectoryService activeDirectoryService, string avatarsFolder)
        {
            _log.Debug("Attempting to import image for " + personSetting.RawName);
            var picture = activeDirectoryService.GetUserPicture(personSetting.RawName, _activeDirectoryDomain.Text);
            if (picture == null) return;
            var newFileName = Guid.NewGuid() + ".png";
            var combine = Path.Combine(avatarsFolder, newFileName);
            picture.Save(combine);
            personSetting.AvatarImageName = newFileName;
            personSetting.AvatarImageUploaded = false;
        }
    }
}
