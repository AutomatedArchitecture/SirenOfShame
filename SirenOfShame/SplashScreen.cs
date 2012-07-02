using System.Drawing;
using System.Windows.Forms;

namespace SirenOfShame
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;
            label1.Text = "V " + Application.ProductVersion;
        }
    }
}
