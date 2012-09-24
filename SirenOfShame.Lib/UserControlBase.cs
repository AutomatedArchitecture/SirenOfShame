using System;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame.Lib
{
    public class UserControlBase : UserControl
    {
        protected void Invoke(Action a)
        {
            ControlHelpers.Invoke(this, a);
        }
    }
}
