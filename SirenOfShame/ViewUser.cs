using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class ViewUser : UserControl
    {
        private SirenOfShameSettings _settings;
        public event CloseScreen OnClose;
        public event UserChangedAvatarId OnUserChangedAvatarId;
        public event UserDisplayNameChanged OnUserDisplayNameChanged;
        private PersonSetting _personSetting;
        private AvatarPicker _avatarPicker;

        private void InvokeOnOnUserChangedAvatarId(int newImageIndex)
        {
            UserChangedAvatarId handler = OnUserChangedAvatarId;
            if (handler != null) handler(this, new UserChangedAvatarIdArgs { NewImageIndex = newImageIndex, RawName = _personSetting.RawName });
        }

        private void InvokeOnUserDisplayNameChanged(UserDisplayNameChangedArgs args)
        {
            UserDisplayNameChanged handler = OnUserDisplayNameChanged;
            if (handler != null) handler(this, args);
        }

        public ViewUser()
        {
            InitializeComponent();

            _displayNameTextbox.LostFocus += DisplayNameTextboxOnLostFocus;

            foreach (var achievementLookup in AchievementSetting.AchievementLookups)
            {
                var label = new Label { Text = achievementLookup.Name };
                var cloneFrom = _unobtainedTemplate;
                label.Font = cloneFrom.Font;
                label.ForeColor = cloneFrom.ForeColor;
                label.BackColor = cloneFrom.BackColor;
                label.BorderStyle = cloneFrom.BorderStyle;
                label.AutoSize = cloneFrom.AutoSize;
                label.Margin = cloneFrom.Margin;
                label.Padding = cloneFrom.Padding;
                flowLayoutPanel1.Controls.Add(label);
                toolTip1.SetToolTip(label, achievementLookup.Description);
            }
        }

        private void DisplayNameTextboxOnLostFocus(object sender, EventArgs eventArgs)
        {
            SaveDisplayName();
        }

        public void Initilaize(SirenOfShameSettings settings)
        {
            _settings = settings;
        }

        private void InvokeOnClose()
        {
            var onClose = OnClose;
            if (onClose != null)
                onClose(this, new CloseScreenArgs());
        }

        private ImageList _avatarImageList;

        public void SetUser(PersonSetting personSetting, ImageList avatarImageList)
        {
            this.SuspendDrawing(() =>
            {
                _avatarImageList = avatarImageList;
                avatar1.SetImage(personSetting, avatarImageList);
                _personSetting = personSetting;
                SetUser(personSetting);
            });
        }

        private void SetUser(PersonSetting personSetting)
        {
            _userName.Text = personSetting.GetBothDisplayAndRawNames();
            _reputation.Text = personSetting.GetReputation().ToString(CultureInfo.InvariantCulture);
            _achievementCount.Text = personSetting.Achievements.Count.ToString(CultureInfo.InvariantCulture);
            _achievementsText.Text = personSetting.Achievements.Count == 1 ? "Achievement" : "Achievements";

            int achievementIndex = 0;
            foreach (var achievementLookup in AchievementSetting.AchievementLookups)
            {
                bool hasUserAchieved = personSetting.Achievements.Any(i => i.AchievementId == (int) achievementLookup.Id);
                var cloneFrom = hasUserAchieved ? _obtainedTemplate : _unobtainedTemplate;
                Control control = flowLayoutPanel1.Controls[achievementIndex];
                Debug.Assert(control != null);
                control.BackColor = cloneFrom.BackColor;
                control.ForeColor = cloneFrom.ForeColor;
                achievementIndex++;
            }
        }

        private void ChangeAvatarLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenAvatarPicker(_changeAvatar);
        }

        private void OpenAvatarPicker(Control changeAvatar)
        {
            _avatarPicker = new AvatarPicker(_avatarImageList, _personSetting);
            _avatarPicker.Deactivate += (sender, args) =>
            {
                _avatarPicker.Close();
                _avatarPicker.Dispose();
            };
            _avatarPicker.Show(this);
            _avatarPicker.Activate();

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

        private void UserNameClick(object sender, EventArgs e)
        {
            MakeUserNameEditable(true);
        }

        private void MakeUserNameEditable(bool editable)
        {
            _userName.Visible = !editable;
            _displayNameTextbox.Visible = editable;
            if (editable)
            {
                _displayNameTextbox.Text = _personSetting.DisplayName;
            }
        }

        private void DisplayNameTextboxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _displayNameTextbox.Text = _personSetting.DisplayName;
                MakeUserNameEditable(false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SaveDisplayName();
            }
        }

        private void SaveDisplayName()
        {
            string newDisplayName = _displayNameTextbox.Text;
            _personSetting.DisplayName = newDisplayName;
            _settings.Save();
            InvokeOnUserDisplayNameChanged(new UserDisplayNameChangedArgs
            {RawUserName = _personSetting.RawName, NewDisplayName = newDisplayName});
            MakeUserNameEditable(false);
            _userName.Text = _personSetting.GetBothDisplayAndRawNames();
        }

        private void BackClick(object sender, EventArgs e)
        {
            InvokeOnClose();
        }

        public void NewAchievements(PersonSetting person)
        {
            if (!Visible) return;
            var currentlyDisplayedUserJustGotNewAchievements = _personSetting.RawName == person.RawName;
            if (!currentlyDisplayedUserJustGotNewAchievements) return;
            SetUser(person);
        }
    }

    public delegate void UserChangedAvatarId(object sender, UserChangedAvatarIdArgs args);

    public class UserChangedAvatarIdArgs
    {
        public string RawName { get; set; }
        public int NewImageIndex { get; set; }
    }

    public delegate void CloseScreen(object sender, CloseScreenArgs args);

    public class CloseScreenArgs
    {
    }

    public delegate void UserDisplayNameChanged(object sender, UserDisplayNameChangedArgs args);

    public class UserDisplayNameChangedArgs
    {
        public string RawUserName { get; set; }
        public string NewDisplayName { get; set; }
    }
}
