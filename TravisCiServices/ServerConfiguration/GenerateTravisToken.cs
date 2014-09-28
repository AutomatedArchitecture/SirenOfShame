using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using log4net;
using Newtonsoft.Json;
using SirenOfShame;
using SirenOfShame.Lib;

namespace TravisCiServices.ServerConfiguration
{
    public partial class GenerateTravisToken : FormBase
    {
        private readonly string _url;
        private readonly ILog _log = MyLogManager.GetLogger(typeof (GenerateTravisToken));

        private string TravisApiAccessToken { get; set; }

        public static string ShowDialog(string url)
        {
            var generateTravisToken = new GenerateTravisToken(url);
            generateTravisToken.ShowDialog();
            return generateTravisToken.TravisApiAccessToken;
        }

        private GenerateTravisToken(string url) : this()
        {
            _url = url;
        }

        private GenerateTravisToken()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://github.com/settings/applications");
            Process.Start(sInfo);
        }

        private async void GenerateToken_Click(object sender, EventArgs e)
        {
            using (var webClient = new WebClient())
            {
                Uri uri;
                try
                {
                    uri = new Uri(_url + "auth/github");
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                    MessageBox.Show("Invalid url: " + _url);
                    return;
                }
                webClient.Headers.Add("Accept", "application/vnd.travis-ci.2+json");
                webClient.Headers.Add("Content-Type", "application/json");

                try
                {
                    _loading.Visible = true;
                    var data = "{\"github_token\":\"" + _githubToken.Text + "\"}";
                    var result = await webClient.UploadStringTaskAsync(uri, "POST", data);
                    _loading.Visible = false;
                    var deserializeObject = JsonConvert.DeserializeAnonymousType(result, new {access_token = ""});
                    TravisApiAccessToken = deserializeObject.access_token;
                    Close();
                }
                catch (WebException ex)
                {
                    MessageBox.Show("Unable to connect: " + ex.Message);
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
                finally
                {
                    _loading.Visible = false;
                }
            }
        }
    }
}
