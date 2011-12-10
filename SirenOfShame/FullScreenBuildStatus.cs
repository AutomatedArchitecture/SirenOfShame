using System.Windows.Forms;

namespace SirenOfShame
{
    public partial class FullScreenBuildStatus : FullScreenFormBase
    {
        public FullScreenBuildStatus()
        {
            InitializeComponent();
        }

        private void FullScreenBuildStatus_KeyDown(object sender, KeyEventArgs e)
        {
            Hide();
        }
    }
}
