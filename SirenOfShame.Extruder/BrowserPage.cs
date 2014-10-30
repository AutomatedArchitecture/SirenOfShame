using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Extruder.Models;
using SirenOfShame.Extruder.Services;

namespace SirenOfShame.Extruder
{
    public partial class BrowserPage : UserControl
    {
        private readonly ILog _log = MyLogManager.GetLog(typeof (BrowserPage));

        public BrowserPage()
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
            string credentialsAsString = string.Format("username={0}&encryptedpassword={1}&extruderName={2}", 
                WebUtility.UrlEncode(settings.UserName), 
                WebUtility.UrlEncode(settings.EncryptedPassword),
                WebUtility.UrlEncode(settings.MyName));
            var asciiEncoding = new ASCIIEncoding();
            byte[] credentials = asciiEncoding.GetBytes(credentialsAsString);

            string url = SosOnlineService.SOS_URL + "/Mobile/App";
            _webBrowser.Navigate(url, null, credentials, "Content-Type: application/x-www-form-urlencoded");
            _webBrowser.Url = new Uri(url);
        }

        public void Refresh()
        {
            if (_settings != null)
            {
                _isInitialized = false;
                EnsureConnected(_settings);
            }
        }
    }
}
