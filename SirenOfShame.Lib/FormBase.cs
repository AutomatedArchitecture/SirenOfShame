using System;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame
{
    public class FormBase : Form
    {
        protected FormBase()
        {
            ShowInTaskbar = false;
        }

        public void Invoke(Action a)
        {
            ControlHelpers.Invoke(this, a);
        }

    }
}
