using System;
using System.Windows.Forms;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class AvatarPicker : Form
    {
        private readonly ImageList _avatarImageList;
        private readonly PersonSetting _personSetting;
        public event AvatarClicked OnAvatarClicked;

        private void InvokeOnOnAvatarClicked(int index)
        {
            var handler = OnAvatarClicked;
            if (handler != null) handler(this, new AvatarClickedArgs { Index = index });
        }

        public AvatarPicker(ImageList avatarImageList, PersonSetting personSetting)
        {
            _avatarImageList = avatarImageList;
            _personSetting = personSetting;
            InitializeComponent();
            emailTextbox.Text = personSetting.Email;
            int avatarCount = SirenOfShameSettings.AVATAR_COUNT;
            for (int i = 0; i < avatarCount; i++)
            {
                Avatar avatar = new Avatar();
                avatar.SetImage(i, avatarImageList);
                avatar.Click += AvatarOnClick;
                flowLayoutPanel1.Controls.Add(avatar);
            }
        }

        private void AvatarOnClick(object sender, EventArgs eventArgs)
        {
            Avatar avatar = (Avatar) sender;
            SelectAvatarAndClose(avatar);
        }

        private void SelectAvatarAndClose(Avatar avatar)
        {
            InvokeOnOnAvatarClicked(avatar.ImageIndex);
            Close();
            Dispose();
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            var gravatarService = new GravatarService();
            var avatarId = gravatarService.DownloadGravatarFromEmailAndAddToImageList(emailTextbox.Text, _avatarImageList);
            _gravatar.SetImage(avatarId, _avatarImageList);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            _personSetting.Email = emailTextbox.Text;
            SelectAvatarAndClose(_gravatar);
        }
    }

    public delegate void AvatarClicked(object sender, AvatarClickedArgs args);

    public class AvatarClickedArgs
    {
        public int Index { get; set; }
    }
}
