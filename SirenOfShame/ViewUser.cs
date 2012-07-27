using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class ViewUser : UserControl
    {
        private SirenOfShameSettings _settings;
        public event CloseViewUser OnClose;
        private PersonSetting _personSetting;
        
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
            _personSetting = personSetting;
            _userName.Text = personSetting.GetBothDisplayAndRawNames();
            avatar1.SetPerson(personSetting);
            flowLayoutPanel1.Controls.Clear();

            foreach (var achievementLookup in AchievementSetting.AchievementLookups)
            {
                var label = new Label {Text = achievementLookup.Name};
                bool hasUserAchieved = personSetting.Achievements.Any(i => i.AchievementId == (int)achievementLookup.Id);
                var cloneFrom = hasUserAchieved ? _obtainedTemplate : _unobtainedTemplate;
                label.Font = cloneFrom.Font;
                label.ForeColor = cloneFrom.ForeColor;
                label.BackColor = cloneFrom.BackColor;
                label.BorderStyle = cloneFrom.BorderStyle;
                label.AutoSize = cloneFrom.AutoSize;
                label.Margin = cloneFrom.Margin;
                flowLayoutPanel1.Controls.Add(label);
                toolTip1.SetToolTip(label, achievementLookup.Description);
            }
        }

        private AvatarPicker avatarPicker;

        private void ChangeAvatarLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            avatarPicker = new AvatarPicker();
            avatarPicker.Show(this);

            Point locationOfLink = PointToScreen(_changeAvatar.Location);
            avatarPicker.Location = new Point(locationOfLink.X + _changeAvatar.Width, locationOfLink.Y + _changeAvatar.Height);
            avatarPicker.OnAvatarClicked += AvatarPickerOnOnAvatarClicked;
        }

        private void AvatarPickerOnOnAvatarClicked(object sender, AvatarClickedArgs args)
        {
            if (_personSetting == null) return;
            _personSetting.AvatarId = args.Index;
            _settings.Save();
            avatar1.ImageIndex = args.Index;
        }
    }

    public delegate void CloseViewUser(object sender, CloseViewUserArgs args);

    public class CloseViewUserArgs
    {
    }
}
