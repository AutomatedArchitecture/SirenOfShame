using System;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame
{
    public class FormBase : Form
    {
        public void Invoke(Action a)
        {
            ControlHelpers.Invoke(this, a);
        }

    }
}
