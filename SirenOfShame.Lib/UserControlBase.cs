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

        public void AddMouseUpToAllControls(MouseEventHandler onMouseUp)
        {
            AddMouseUpToControlAndAllChildren(this, onMouseUp);
        }

        public void AddMouseEnterToAllControls(EventHandler onMouseEnter)
        {
            AddMouseEnterToControlAndAllChildren(this, onMouseEnter);
        }

        private static void AddMouseUpToControlAndAllChildren(Control control, MouseEventHandler onMouseUp)
        {
            control.MouseUp += onMouseUp;
            foreach (Control c in control.Controls)
            {
                AddMouseUpToControlAndAllChildren(c, onMouseUp);
            }
        }

        private static void AddMouseEnterToControlAndAllChildren(Control control, EventHandler onMouseEnter)
        {
            control.MouseEnter += onMouseEnter;
            foreach (Control c in control.Controls)
            {
                AddMouseEnterToControlAndAllChildren(c, onMouseEnter);
            }
        }
    }
}
