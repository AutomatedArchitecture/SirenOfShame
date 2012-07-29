using System.Drawing;
using System.Windows.Forms;
using SirenOfShame.Helpers;

namespace SirenOfShame
{
    public class Pill : Label
    {
        public Color PillColor { get; set; }
        
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            pevent.Graphics.FillRoundedRectangle(new SolidBrush(PillColor), ClientRectangle, 5);
        }
    }
}
