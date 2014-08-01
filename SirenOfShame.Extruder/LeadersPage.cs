using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Extruder.Models;
using SirenOfShame.Extruder.Services;

namespace SirenOfShame.Extruder
{
    public partial class LeadersPage : UserControl
    {
        private readonly ILog _log = MyLogManager.GetLog(typeof (LeadersPage));

        public LeadersPage()
        {
            InitializeComponent();
        }

        private bool _isInitialized = false;
        private ExtruderSettings _settings;

        public void EnsureConnected(ExtruderSettings settings)
        {
            if (_isInitialized) return;
            _isInitialized = true;

            _settings = settings;
            string credentialsAsString = string.Format("username={0}&encryptedpassword={1}", WebUtility.UrlEncode(settings.UserName), WebUtility.UrlEncode(settings.EncryptedPassword));
            var asciiEncoding = new ASCIIEncoding();
            byte[] credentials = asciiEncoding.GetBytes(credentialsAsString);

            const string url = SosOnlineService.SOS_URL + "/MyCi/Mobile";
            _webBrowser.Navigate(url, null, credentials, "Content-Type: application/x-www-form-urlencoded");
            _webBrowser.Url = new Uri(url);
        }

        public void Navigate(ExtruderSettings settings, PageType page)
        {
            if (!_isInitialized)
            {
                EnsureConnected(settings);
                return;
            }

            var pageElement = page.ToString().ToLowerInvariant();

            var navigateButton = _webBrowser.Document.GetElementById(pageElement);
            if (navigateButton == null)
            {
                _log.Error("Unable to find element " + pageElement);
                return;
            }
            navigateButton.InvokeMember("click");
        }

        private void _refresh_Click(object sender, EventArgs e)
        {
            if (_settings != null)
            {
                _isInitialized = false;
                EnsureConnected(_settings);
            }
        }
    }
}
