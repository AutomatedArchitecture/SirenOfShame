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
        private readonly ExtruderSettings _settings;
        private readonly TripleDesStringEncryptor _encryptor;

        public Form1()
        {
            _settings = ExtruderSettings.GetAppSettings();
            _encryptor = new TripleDesStringEncryptor();
            
            InitializeComponent();

            RetrieveSettings();
        }

        private void RetrieveSettings()
        {
            _username.Text = _settings.UserName;
            _password.Text = _encryptor.DecryptString(_settings.EncryptedPassword);
            _myname.Text = _settings.MyName;
        }

        private async void Connect_Click(object sender, EventArgs e)
        {
            _log.Debug("Attempting to connect as " + _username.Text);
            var connectExtruderModel = new ConnectExtruderModel
            {
                UserName = _username.Text,
                Password = _encryptor.EncryptString(_password.Text),
                Name = _myname.Text,
            };
            var sosOnlineService = new SosOnlineService();
            var result = await sosOnlineService.ConnectExtruder(connectExtruderModel);
            if (result.Success)
            {
                SaveSettings();
                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }

        private void SaveSettings()
        {
            _settings.UserName = _username.Text;
            _settings.EncryptedPassword = _encryptor.EncryptString(_password.Text);
            _settings.MyName = _myname.Text;
            _settings.Save();
        }
    }
}
