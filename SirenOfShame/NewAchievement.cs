using System;
using System.Diagnostics;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class NewAchievement : Form
    {
        private SirenOfShameSettings _settings;
        private readonly AchievementLookup _achievement;
        private PersonSetting _person;

        public static void ShowForm(SirenOfShameSettings settings, AchievementLookup achievement, PersonSetting person)
        {
            var newAchievement = new NewAchievement(settings, achievement, person);
            newAchievement.ShowDialog();
        }

        private NewAchievement(SirenOfShameSettings settings, AchievementLookup achievement, PersonSetting person)
        {
            _person = person;
            _achievement = achievement;
            _settings = settings;

            InitializeComponent();
            
            _user.Text = person.DisplayName;
            _title.Text = achievement.Name + "!!";
            _accomplishment.Text = achievement.Description;
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
            
        }

        private static string UrlEncode(string s)
        {
            return s.Replace(" ", "%20");
        }
        
        private void ShareOnTwitterClick(object sender, EventArgs e)
        {
            string tweetText = "I%20just%20got%20the%20" + UrlEncode(_achievement.Name) +  "%20achievement%20on%20%23sirenofshame.%20I%20" + UrlEncode(_achievement.Description.ToLower()) + ".";
            Process.Start("https://twitter.com/intent/tweet?text=" + tweetText);
        }
    }
}
