using System.Windows.Forms;

namespace SirenOfShame
{
    public partial class FullScreenEnforcer : Form
    {
        public FullScreenEnforcer()
        {
            InitializeComponent();
        }

        public void UpdateText(string durationAsText)
        {
            _label.Text = durationAsText;
        }

        private void FullScreenEnforcer_KeyDown(object sender, KeyEventArgs e)
        {
            Hide();
        }

        private void _label_Click(object sender, System.EventArgs e)
        {
            Hide();
        }
    }
}
