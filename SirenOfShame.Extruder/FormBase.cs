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
    }
}