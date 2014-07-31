using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SirenOfShame.Extruder.Models;
using SirenOfShame.Extruder.Services;

namespace SirenOfShame.Extruder
{
    public partial class SettingsPage : UserControl
    {
        public ExtruderSettings Settings;
        private readonly TripleDesStringEncryptor _encryptor;

        public SettingsPage()
        {
            InitializeComponent();
            
            _encryptor = new TripleDesStringEncryptor();
        }

        public event ToggleConnectionEvent OnToggleConnection;
        public event TestSirenEvent OnTestSiren;

        protected virtual void InvokeOnTestSiren()
        {
            var handler = OnTestSiren;
            if (handler != null) handler(this, new TestSirenEventArgs());
        }

        private void RetrieveSettings()
        {
            if (Settings == null) return;

            _username.Text = Settings.UserName;
            _password.Text = _encryptor.DecryptString(Settings.EncryptedPassword);
            _myname.Text = Settings.MyName;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            RetrieveSettings();
        }

        private async void Connect_Click(object sender, EventArgs e)
        {
            if (OnToggleConnection == null) return;

            var toggleConnectionEventArgs = new ToggleConnectionEventArgs
            {
                UserName = _username.Text,
                PlainTextPassword = _password.Text,
                Name = _myname.Text,
            };
            await OnToggleConnection(this, toggleConnectionEventArgs);
        }

        public void RefreshIsConnected(bool connectedToServer)
        {
            _connectButton.Text = connectedToServer ? "Disconnect" : "Connect";
        }

        public void DeviceConnected(bool connected)
        {
            _testSiren.Enabled = connected;

        }

        public void UpdateNetworkStatus(bool? isBusy, string statusText)
        {
            if (isBusy.HasValue)
            {
                _connectButton.Enabled = !isBusy.Value;
            }
        }

        private void TestSiren_Click(object sender, EventArgs e)
        {
            InvokeOnTestSiren();
        }
    }

    public delegate void TestSirenEvent(object sender, TestSirenEventArgs args);

    public class TestSirenEventArgs
    {
    }

    public delegate Task ToggleConnectionEvent(object sender, ToggleConnectionEventArgs args);

    public class ToggleConnectionEventArgs
    {
        public string UserName { get; set; }
        public string PlainTextPassword { get; set; }
        public string Name { get; set; }
    }
}
