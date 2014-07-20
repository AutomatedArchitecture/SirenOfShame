using System;
using System.Windows.Forms;
using SirenOfShame.Extruder.Models;
using SirenOfShame.Extruder.Services;

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
            var connectExtruderModel = new ConnectExtruderModel
            {
                UserName = _username.Text,
                Password = _password.Text,
                Name = _myname.Text,
            };
            var sosOnlineService = new SosOnlineService();
            await sosOnlineService.ConnectExtruder(connectExtruderModel);
        }
    }
}
