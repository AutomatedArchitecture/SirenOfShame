using System;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame.Lib
{
    public class UserControlBase : UserControl
    {
        public void Invoke(Action a)
        {
            ControlHelpers.Invoke(this, a);
        }
    }
}
