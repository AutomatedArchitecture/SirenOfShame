using System;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class AvatarPicker : Form
    {
        public event AvatarClicked OnAvatarClicked;

        private void InvokeOnOnAvatarClicked(int index)
        {
            var handler = OnAvatarClicked;
            if (handler != null) handler(this, new AvatarClickedArgs { Index = index });
        }

        public AvatarPicker(ImageList avatarImageList)
        {
            InitializeComponent();
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
            InvokeOnOnAvatarClicked(avatar.ImageIndex);
            Close();
            Dispose();
        }
    }

    public delegate void AvatarClicked(object sender, AvatarClickedArgs args);

    public class AvatarClickedArgs
    {
        public int Index { get; set; }
    }
}
