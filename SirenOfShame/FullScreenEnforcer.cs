using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Exceptions;
using log4net;

namespace SirenOfShame
{
    public partial class FullScreenEnforcer : Form
    {
        private static readonly ILog Log = MyLogManager.GetLogger(typeof(FullScreenEnforcer));
        private readonly bool _screenSaverActive;

        public FullScreenEnforcer()
        {
            InitializeComponent();
            try
            {
                var regVal = (Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "ScreenSaveActive", "1") ?? "1").ToString();
                if (regVal == "1") regVal = "true";
                if (regVal == "0") regVal = "false";
                _screenSaverActive = bool.Parse(regVal);
            }
            catch (Exception ex)
            {
                Log.Error("Could not get screen saver value", ex);
                _screenSaverActive = true;
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            ResetScreenSaverSettings();
            base.OnVisibleChanged(e);
        }

        private void ResetScreenSaverSettings()
        {
            try
            {
                if (Visible)
                {
                    Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "ScreenSaveActive", "0");
                }
                else
                {
                    Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "ScreenSaveActive",
                                      _screenSaverActive ? "1" : "0");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Could not set screen saver", ex);
            }
        }

        private string TimeSpanAsText(TimeSpan timespan, OvertimeStatus overtimeStatus)
        {
            if (overtimeStatus == OvertimeStatus.Normal)
            {
                return ((int)timespan.TotalMinutes).ToString();
            }
            return TimeboxEnforcer.DurationAsText(timespan);
        }

        public void UpdateText(TimeSpan timespan, OvertimeStatus overtimeStatus)
        {
            _label.Text = TimeSpanAsText(timespan, overtimeStatus);
            _label.ForeColor = GetForecolor(overtimeStatus);
        }

        private Color GetForecolor(OvertimeStatus overtimeStatus)
        {
            switch (overtimeStatus)
            {
                case OvertimeStatus.Normal:
                    return Color.White;
                case OvertimeStatus.Warning:
                    return Color.Yellow;
                case OvertimeStatus.Overtime:
                    return Color.Red;
                default:
                    throw new SosException("Unknown status: " + overtimeStatus);
            }
        }

        private void FullScreenEnforcerKeyDown(object sender, KeyEventArgs e)
        {
            Hide();
        }

        private void LabelClick(object sender, EventArgs e)
        {
            Hide();
        }

        private void FullScreenEnforcer_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetScreenSaverSettings();
        }
    }
}
