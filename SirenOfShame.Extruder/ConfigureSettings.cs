using System;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Extruder.Models;
using SirenOfShame.Extruder.Services;

namespace SirenOfShame.Extruder
{
    public partial class ConfigureSettings : FormBase
    {
        private readonly ILog _log = MyLogManager.GetLog(typeof (ConfigureSettings));
        private readonly ExtruderSettings _settings;
        private readonly TripleDesStringEncryptor _encryptor;
        private readonly SosOnlineService _sosOnlineService = new SosOnlineService();

        public ConfigureSettings()
        {
            _settings = ExtruderSettings.GetAppSettings();
            _encryptor = new TripleDesStringEncryptor();
            
            InitializeComponent();

            RetrieveSettings();
            _sosOnlineService.StatusChanged += status => Invoke(() =>
            {
                _connectionStatus.Text = status.NewState.ToString();
            });
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
            _connectionStatus.Text = "Verifying credentials";
            var result = await _sosOnlineService.ConnectExtruder(connectExtruderModel);
            if (result.Success)
            {
                SaveSettings();
                _connectionStatus.Text = "Connecting";
                await _sosOnlineService.StartRealtimeConnection(connectExtruderModel);
            }
            else
            {
                _connectionStatus.Text = "Failed To Connect";
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            WindowState = FormWindowState.Minimized;
            Invoke(Application.Exit);
        }

        private void ConfigureSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
            }
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowConfigureScreen();
        }

        private void ShowConfigureScreen()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
            Activate();
            Focus();
            BringToFront();
        }

        private void _notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowConfigureScreen();
        }
    }
}
