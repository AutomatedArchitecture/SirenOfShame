using System.Drawing;
using System.Windows.Forms;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class Avatar : UserControl
    {
        public Avatar()
        {
            InitializeComponent();
        }

        private void AvatarPaint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, DisplayRectangle, Color.DarkGray, ButtonBorderStyle.Solid);
        }

        public void SetImage(int imageListId, ImageList avatarImageList)
        {
            label1.ImageList = avatarImageList;
            label1.ImageIndex = imageListId;
        }

        public void SetImage(PersonBase user, ImageList avatarImageList)
        {
            var avatarId = user.GetAvatarId(avatarImageList);
            SetImage(user.AvatarId, avatarImageList);
        }

        private void Label1Click(object sender, System.EventArgs e)
        {
            OnClick(e);
        }

        public int ImageIndex
        {
            get { return label1.ImageIndex; }
            set { label1.ImageIndex = value; }
        }
    }
}
