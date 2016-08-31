using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SirenOfShame
{
    public partial class HelpAbout : Form
    {
        public HelpAbout()
        {
            InitializeComponent();
            _version.Text = string.Format("Version {0}", Application.ProductVersion);
        }

        private void OkClick(object sender, EventArgs e)
        {
            Close();
        }

        private void UrlLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {   
            ProcessStartInfo sInfo = new ProcessStartInfo("https://www.sirenofshame.com/");
            Process.Start(sInfo);
        }
    }
}
