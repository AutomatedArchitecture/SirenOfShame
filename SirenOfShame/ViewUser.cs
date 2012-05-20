using System;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class ViewUser : UserControl
    {
        private SirenOfShameSettings _settings;
        public event CloseViewUser OnClose;
        
        public ViewUser()
        {
            InitializeComponent();
        }

        public void Initilaize(SirenOfShameSettings settings)
        {
            _settings = settings;
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            OnClose(this, new CloseViewUserArgs());
        }

        public void SetUser(PersonSetting personSetting)
        {
            _userName.Text = personSetting.DisplayName;
            flowLayoutPanel1.Controls.Clear();

            foreach (var achievementLookup in AchievementSetting.AchievementLookups)
            {
                var label = new Label();
                label.Text = achievementLookup.Name;
                bool hasUserAchieved = personSetting.Achievements.Any(i => i.AchievementId == (int)achievementLookup.Id);
                var cloneFrom = hasUserAchieved ? _obtainedTemplate : _unobtainedTemplate;
                label.Font = cloneFrom.Font;
                label.ForeColor = cloneFrom.ForeColor;
                label.BackColor = cloneFrom.BackColor;
                label.BorderStyle = cloneFrom.BorderStyle;
                label.AutoSize = cloneFrom.AutoSize;
                label.Margin = cloneFrom.Margin;
                flowLayoutPanel1.Controls.Add(label);
            }
        }
    }

    public delegate void CloseViewUser(object sender, CloseViewUserArgs args);

    public class CloseViewUserArgs
    {
    }
}
