using System;
using System.Windows.Forms;

namespace SirenOfShame
{
    public partial class AvatarPicker : Form
    {
        public event AvatarClicked OnAvatarClicked;

        public void InvokeOnOnAvatarClicked(int index)
        {
            AvatarClicked handler = OnAvatarClicked;
            if (handler != null) handler(this, new AvatarClickedArgs { Index = index });
        }

        public AvatarPicker()
        {
            InitializeComponent();
            int avatarCount = new Avatar().AvatarCount;
            for (int i = 0; i < avatarCount; i++)
            {
                Avatar avatar = new Avatar {ImageIndex = i};
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
