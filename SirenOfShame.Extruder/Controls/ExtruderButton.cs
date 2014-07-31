using System.Drawing;
using System.Windows.Forms;

namespace SirenOfShame.Extruder.Controls
{
    class ExtruderButton : Button
    {
        public ExtruderButton()
        {
            ForeColor = Color.White;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderColor = Color.FromArgb(40, 38, 39);
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.FromArgb(186, 55, 0);
            FlatAppearance.MouseOverBackColor = Color.FromArgb(196, 65, 0);
            BackColor = Color.FromArgb(68, 68, 68);
        }
    }
}
