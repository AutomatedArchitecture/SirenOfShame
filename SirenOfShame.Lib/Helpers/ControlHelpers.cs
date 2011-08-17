using System;
using System.Windows.Forms;

namespace SirenOfShame.Lib.Helpers
{
    public static class ControlHelpers
    {
        public static void Invoke(this Control ctrl, Action a)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(a);
            }
            else
            {
                a();
            }
        }
    }
}
