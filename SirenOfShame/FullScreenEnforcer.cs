using System.Drawing;
using System.Windows.Forms;
using SirenOfShame.Lib.Exceptions;

namespace SirenOfShame
{
    public partial class FullScreenEnforcer : Form
    {
        public FullScreenEnforcer()
        {
            InitializeComponent();
        }

        public void UpdateText(string durationAsText, OvertimeStatus overtimeStatus)
        {
            _label.Text = durationAsText;
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

        private void LabelClick(object sender, System.EventArgs e)
        {
            Hide();
        }
    }
}
