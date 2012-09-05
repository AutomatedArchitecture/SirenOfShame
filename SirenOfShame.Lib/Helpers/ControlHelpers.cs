using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using log4net;

namespace SirenOfShame.Lib.Helpers
{
    public static class ControlHelpers
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(ControlHelpers));
        
        public static void SuspendLayout(Control control, Action action)
        {
            control.SuspendLayout();
            try
            {
                action();
            } 
            finally
            {
                control.ResumeLayout();
            }
        }

        public static void ClearAndDispose(this Control control)
        {
            if (control == null) return;
            control.SuspendLayout();
            try
            {
                List<Control> ctrls = control.Controls.Cast<Control>().ToList();
                control.Controls.Clear();
                foreach (Control c in ctrls)
                    c.Dispose();
            } finally
            {
                control.ResumeLayout();
            }
        }

        public static void Invoke(this Control ctrl, Action a)
        {
            if (ctrl.InvokeRequired)
            {
                try
                {
                    ctrl.Invoke(a);
                } catch (ArgumentException ex)
                {
                    _log.Error("Error on invoke. Ignoring it for now, but this could be an indication of a larger problem.", ex);
                }
            }
            else
            {
                a();
            }
        }
    }
}
