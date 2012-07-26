using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SirenOfShame
{
    public partial class Avatar : UserControl
    {
        public Avatar()
        {
            InitializeComponent();
        }

        private void Avatar_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, DisplayRectangle, Color.DarkGray, ButtonBorderStyle.Solid);
        }
    }
}
