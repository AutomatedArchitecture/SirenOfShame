using System;
using System.Diagnostics;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class NewAchievement : Form
    {
        private readonly SirenOfShameSettings _settings;
        private readonly AchievementLookup _achievement;

        public static void ShowForm(SirenOfShameSettings settings, AchievementLookup achievement, PersonSetting person, IWin32Window owner)
        {
            var newAchievement = new NewAchievement(settings, achievement, person);
            newAchievement.ShowDialog(owner);
        }

        private NewAchievement(SirenOfShameSettings settings, AchievementLookup achievement, PersonSetting person)
        {
            _achievement = achievement;
            _settings = settings;

            InitializeComponent();
            
            _user.Text = person.DisplayName;
            _title.Text = achievement.Name + "!";
            _accomplishment.Text = achievement.Description;

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


        public NewAchievement()
        {
            InitializeComponent();
        }

        private void OkClick(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
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
                if (_userIAm.SelectedItem as string == "")
                {
                    MessageBox.Show("Please either select who you are or select a different notification option.");
                    e.Cancel = true;
                }
                else
                {
                    _settings.MyRawName = ((PersonSetting) _userIAm.SelectedItem).RawName;
                }

            }
            _settings.Save();
        }

        private static string UrlEncode(string s)
        {
            return s.Replace("%", "%25").Replace("+", "%2B").Replace(" ", "%20");
        }
        
        private void ShareOnTwitterClick(object sender, EventArgs e)
        {
            string tweetText = "I%20just%20got%20the%20" + UrlEncode(_achievement.Name) +  "%20achievement%20on%20%40sirenofshame.%20I%20" + UrlEncode(_achievement.Description.ToLower()) + ".";
            Process.Start("https://twitter.com/intent/tweet?text=" + tweetText);
        }

        private void AlwaysShowNewAchievementsCheckedChanged(object sender, EventArgs e)
        {
            ShowUserIAm(false);
        }

        private void ShowUserIAm(bool visible)
        {
            _iAmLabel.Visible = visible;
            _userIAm.Visible = visible;
        }

        private void OnlyShowMyAchievementsCheckedChanged(object sender, EventArgs e)
        {
            ShowUserIAm(true);
        }

        private void NeverShowAchievementsCheckedChanged(object sender, EventArgs e)
        {
            ShowUserIAm(false);
        }
    }
}
