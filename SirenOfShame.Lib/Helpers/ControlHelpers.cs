using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            if (ctrl.IsDisposed || ctrl.Disposing)
            {
                return;
            }

            if (ctrl.IsHandleCreated)
            {
                var invokeRequired = ctrl.InvokeRequired;
                if (invokeRequired)
                {
                    try
                    {
                        ctrl.Invoke(a);
                    }
                    catch (ArgumentException ex)
                    {
                        _log.Error("Error on invoke. Ignoring it for now, but this could be an indication of a larger problem.", ex);
                    }
                }
                else
                {
                    a();
                }
            }
            else
            {
                a();
            }
        }

        [DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SETREDRAW = 0xB;

        public static void SuspendDrawing(this Control target, Action action)
        {
            target.SuspendDrawing();
            try
            {
                action();
            }
            finally
            {
                target.ResumeDrawing();
            }
        }
        
        private static void SuspendDrawing(this Control target)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 0, 0);
        }

        private static void ResumeDrawing(this Control target, bool redraw = true)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 1, 0);

            if (redraw)
            {
                target.Refresh();
            }
        }
    }
}
