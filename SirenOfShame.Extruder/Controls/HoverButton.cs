using System;
using System.Drawing;
using System.Windows.Forms;

namespace SirenOfShame.Extruder.Controls
{
    public class HoverButton : PictureBox
    {
        protected override void OnMouseEnter(EventArgs e)
        {
            BackColor = Color.FromArgb(196, 65, 0);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = Color.FromArgb(25, 25, 25);
            base.OnMouseLeave(e);
        }
    }
}
