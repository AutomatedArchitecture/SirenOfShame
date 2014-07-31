using System;
using System.Windows.Forms;
using SirenOfShame.Extruder.Services;

namespace SirenOfShame.Extruder
{
    public partial class LeadersPage : UserControl
    {
        public LeadersPage()
        {
            InitializeComponent();
        }

        public void EnsureConnected()
        {
            if (_webBrowser.Url != null)
            {
                const string url = SosOnlineService.SOS_URL + "/Extruder/Leaders";
                _webBrowser.Url = new Uri(url);
            }
        }
    }
}
