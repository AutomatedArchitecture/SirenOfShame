using System;
using System.Drawing;
using System.Globalization;
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
        public event UserDisplayNameChanged OnUserDisplayNameChanged;
        private PersonSetting _personSetting;
        private AvatarPicker _avatarPicker;

        private void InvokeOnOnUserChangedAvatarId(int newImageIndex)
        {
            UserChangedAvatarId handler = OnUserChangedAvatarId;
            if (handler != null) handler(this, new UserChangedAvatarIdArgs { NewImageIndex = newImageIndex, RawName = _personSetting.RawName });
        }

        public void InvokeOnUserDisplayNameChanged(UserDisplayNameChangedArgs args)
        {
            UserDisplayNameChanged handler = OnUserDisplayNameChanged;
            if (handler != null) handler(this, args);
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

        private ImageList _avatarImageList;

        public void SetUser(PersonSetting personSetting, ImageList avatarImageList)
        {
            _avatarImageList = avatarImageList;
            _personSetting = personSetting;
            _userName.Text = personSetting.GetBothDisplayAndRawNames();
            avatar1.SetImage(personSetting, avatarImageList);
            flowLayoutPanel1.Controls.Clear();
            _reputation.Text = personSetting.GetReputation().ToString(CultureInfo.InvariantCulture);
            _achievementCount.Text = personSetting.Achievements.Count.ToString(CultureInfo.InvariantCulture);
            _achievementsText.Text = personSetting.Achievements.Count == 1 ? "Achievement" : "Achievements";

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

        private void ChangeAvatarLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenAvatarPicker(_changeAvatar);
        }

        private void OpenAvatarPicker(Control changeAvatar)
        {
            _avatarPicker = new AvatarPicker(_avatarImageList);
            _avatarPicker.Show(this);

            Point locationOfLink = PointToScreen(changeAvatar.Location);
            _avatarPicker.Location = new Point(locationOfLink.X + changeAvatar.Width, locationOfLink.Y + changeAvatar.Height);
            _avatarPicker.OnAvatarClicked += AvatarPickerOnOnAvatarClicked;
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

    public delegate void UserDisplayNameChanged(object sender, UserDisplayNameChangedArgs args);

    public class UserDisplayNameChangedArgs
    {
        public string RawUserName { get; set; }
        public string NewDisplayName { get; set; }
    }
}
