using System;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.ServerConfiguration
{
    public class ConfigureServerBase : UserControl
    {
        protected SirenOfShameSettings Settings { get; set; }

        /// <summary>
        /// This is only for designer support, do not use this constructor.
        /// </summary>
        public ConfigureServerBase()
        {
        }

        public ConfigureServerBase(SirenOfShameSettings settings)
        {
            // http://www.codeproject.com/Tips/61966/Control-Resizing-on-a-UserControl-in-WinForms.aspx?msg=3389586
            Dock = DockStyle.Fill;

            Settings = settings;
        }

        protected void Invoke(Action a)
        {
            if (InvokeRequired)
            {
                Invoke((Delegate)a);
            }
            else
            {
                a();
            }
        }
    }
}
