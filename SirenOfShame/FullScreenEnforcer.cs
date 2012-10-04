using System;
using System.Drawing;
using System.Windows.Forms;
using SirenOfShame.Lib.Exceptions;

namespace SirenOfShame
{
    public partial class FullScreenEnforcer : FullScreenFormBase
    {

        public FullScreenEnforcer()
        {
            InitializeComponent();
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
            ExitFullScreen();
        }

        private void LabelClick(object sender, EventArgs e)
        {
            ExitFullScreen();
        }
    }
}
