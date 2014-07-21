using System;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Extruder.Models;
using SirenOfShame.Extruder.Services;

namespace SirenOfShame.Extruder
{
    public partial class Form1 : Form
    {
        private readonly ILog _log = MyLogManager.GetLog(typeof (Form1));

        public Form1()
        {
            InitializeComponent();
        }

        private async void Connect_Click(object sender, EventArgs e)
        {
            _log.Debug("Attempting to connect as " + _username.Text);
            var encryptor = new TripleDesStringEncryptor();
            var connectExtruderModel = new ConnectExtruderModel
            {
                UserName = _username.Text,
                Password = encryptor.EncryptString(_password.Text),
                Name = _myname.Text,
            };
            var sosOnlineService = new SosOnlineService();
            var result = await sosOnlineService.ConnectExtruder(connectExtruderModel);
            if (result.Success)
            {
                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }
    }
}
