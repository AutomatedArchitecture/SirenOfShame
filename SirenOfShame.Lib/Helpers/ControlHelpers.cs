using System;
using System.Windows.Forms;
using log4net;

namespace SirenOfShame.Lib.Helpers
{
    public static class ControlHelpers
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(ControlHelpers));

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
