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
        public event UserChangedAvatarId OnUserChangedAvatarId;
        private PersonSetting _personSetting;

        private void InvokeOnOnUserChangedAvatarId(int newImageIndex)
        {
            UserChangedAvatarId handler = OnUserChangedAvatarId;
            if (handler != null) handler(this, new UserChangedAvatarIdArgs { NewImageIndex = newImageIndex, RawName = _personSetting.RawName });
        }

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
            OpenAvatarPicker(_changeAvatar);
        }

        private void OpenAvatarPicker(Control changeAvatar)
        {
            avatarPicker = new AvatarPicker();
            avatarPicker.Show(this);

            Point locationOfLink = PointToScreen(changeAvatar.Location);
            avatarPicker.Location = new Point(locationOfLink.X + changeAvatar.Width, locationOfLink.Y + changeAvatar.Height);
            avatarPicker.OnAvatarClicked += AvatarPickerOnOnAvatarClicked;
        }

        private void AvatarPickerOnOnAvatarClicked(object sender, AvatarClickedArgs args)
        {
            if (_personSetting == null) return;
            int newImageIndex = args.Index;
            _personSetting.AvatarId = newImageIndex;
            _settings.Save();
            avatar1.ImageIndex = newImageIndex;
            InvokeOnOnUserChangedAvatarId(newImageIndex);
        }

        private void Avatar1Click(object sender, EventArgs e)
        {
            OpenAvatarPicker(avatar1);
        }
    }

    public delegate void UserChangedAvatarId(object sender, UserChangedAvatarIdArgs args);

    public class UserChangedAvatarIdArgs
    {
        public string RawName { get; set; }
        public int NewImageIndex { get; set; }
    }

    public delegate void CloseViewUser(object sender, CloseViewUserArgs args);

    public class CloseViewUserArgs
    {
    }
}
