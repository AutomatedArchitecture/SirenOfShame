using System.Drawing;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class Avatar : UserControl
    {
        public Avatar()
        {
            InitializeComponent();
        }

        public int AvatarCount
        {
            get { return imageList1.Images.Count; }
        }

        private void AvatarPaint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, DisplayRectangle, Color.DarkGray, ButtonBorderStyle.Solid);
        }

        public void SetPerson(PersonSetting user)
        {
            label1.ImageIndex = user.AvatarId;
        }
    }
}
