using System.Drawing;
using System.Windows.Forms;

namespace SirenOfShame
{
    public class Separator : Control
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.FromArgb(88,88,88)), 0, 0, 0, Height);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(17,17,17)), 1, 0, 1, Height);
        }
    }
}
