using System;
using System.Windows.Forms;
using Microsoft.Win32;
using SirenOfShame.Lib;
using log4net;

namespace SirenOfShame
{
    public class FullScreenFormBase : FormBase
    {
        private static readonly ILog Log = MyLogManager.GetLogger(typeof(FullScreenFormBase));

        private readonly bool _wasScreenSaverPreviouslyActive;

        private void FullScreenEnforcer_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetScreenSaverSettings();
        }

        public FullScreenFormBase()
        {
            FormClosing += FullScreenEnforcer_FormClosing;

            try
            {
                _wasScreenSaverPreviouslyActive = (Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "ScreenSaveActive", "1") ?? "1").Equals("1");
            }
            catch (Exception ex)
            {
                Log.Error("Could not get screen saver value", ex);
                _wasScreenSaverPreviouslyActive = true;
            }
        }

        protected void ResetScreenSaverSettings()
        {
            try
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "ScreenSaveActive", Visible || _wasScreenSaverPreviouslyActive ? "1" : "0");
            }
            catch (Exception ex)
            {
                Log.Error("Could not set screen saver", ex);
            }
        }

        protected void ExitFullScreen()
        {
            Hide();
            Close();
        }
    }
}
