using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
using SirenOfShame.Extruder.Models;
using SirenOfShame.Extruder.Services;

namespace SirenOfShame.Extruder
{
    public partial class LeadersPage : UserControl
    {
        public LeadersPage()
        {
            InitializeComponent();
        }

        private bool _isInitialized = false;

        public void EnsureConnected(ExtruderSettings settings)
        {
            if (_isInitialized) return;
            _isInitialized = true;

            string credentialsAsString = string.Format("username={0}&encryptedpassword={1}", WebUtility.UrlEncode(settings.UserName), WebUtility.UrlEncode(settings.EncryptedPassword));
            var asciiEncoding = new ASCIIEncoding();
            byte[] credentials = asciiEncoding.GetBytes(credentialsAsString);

            const string url = SosOnlineService.SOS_URL + "/Extruder/Leaders";
            _webBrowser.Navigate(url, null, credentials, "Content-Type: application/x-www-form-urlencoded");
            _webBrowser.Url = new Uri(url);
        }
    }
}
