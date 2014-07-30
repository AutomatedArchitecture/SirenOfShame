using System;
using System.Windows.Forms;
using SirenOfShame.Extruder.Helpers;

namespace SirenOfShame.Extruder
{
    public class FormBase : Form
    {
        public void Invoke(Action a)
        {
            ControlHelpers.Invoke(this, a);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormBase
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "FormBase";
            this.ResumeLayout(false);

        }
    }
}