using System;
using System.Net;
using System.Windows.Forms;
using SirenOfShame.Extruder.Models;

namespace SirenOfShame.Extruder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Connect_Click(object sender, EventArgs e)
        {
            var webClient = new WebClient();
            var connectExtruderModel = new ConnectExtruderModel
            {
                UserName = _username.Text,
                Password = _password.Text,
                Name = _myname.Text,
            };
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(connectExtruderModel);
            const string url = "http://localhost:3115/ApiV1/ConnectExtruder";
            var result = await webClient.UploadStringTaskAsync(url, "POST", data);
        }
    }
}
