using System;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Extruder.Services;

namespace SirenOfShame.Extruder.Helpers
{
    public static class ControlHelpers
    {
        private static readonly ILog _log = MyLogManager.GetLog(typeof (ControlHelpers));

        public static void Invoke(this Control ctrl, Action a)
        {
            bool invokeRequired = ctrl.InvokeRequired;
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
    }
}