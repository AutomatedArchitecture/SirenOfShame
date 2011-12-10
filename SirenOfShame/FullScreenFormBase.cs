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
                var isScreenSaverActive = (Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "ScreenSaveActive", "1") ?? "1").ToString();
                if (isScreenSaverActive == "1") isScreenSaverActive = "true";
                if (isScreenSaverActive == "0") isScreenSaverActive = "false";
                _wasScreenSaverPreviouslyActive = bool.Parse(isScreenSaverActive);
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
                if (Visible)
                {
                    Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "ScreenSaveActive", "0");
                }
                else
                {
                    Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "ScreenSaveActive", _wasScreenSaverPreviouslyActive ? "1" : "0");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Could not set screen saver", ex);
            }
        }
    }
}
