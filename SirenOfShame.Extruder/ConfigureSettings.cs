using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Microsoft.AspNet.SignalR.Client;
using SirenOfShame.Extruder.Models;
using SirenOfShame.Extruder.Services;
using SirenOfShame.Lib.Device;

namespace SirenOfShame.Extruder
{
    public partial class ConfigureSettings : FormBase
    {
        private readonly ILog _log = MyLogManager.GetLog(typeof (ConfigureSettings));
        private readonly ExtruderSettings _settings;
        private readonly TripleDesStringEncryptor _encryptor;
        private readonly SosOnlineService _sosOnlineService = new SosOnlineService();
        private readonly ISirenOfShameDevice _sirenOfShameDevice = new SirenOfShameDevice();
        private bool _connectedToServer;

        public ConfigureSettings()
        {
            _settings = ExtruderSettings.GetAppSettings();
            _encryptor = new TripleDesStringEncryptor();

            InitializeComponent();

            DeviceConnected();
            RetrieveSettings();
            RefreshConnectText(ConnectionState.Disconnected);
 
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _sosOnlineService.StatusChanged += OnStatusChanged;
            _sosOnlineService.PlaySiren += OnPlaySiren;
            _sosOnlineService.SetTrayIcon += RulesEngineSetTrayIcon;
            _sirenOfShameDevice.Connected += SirenOfShameDeviceConnected;
            _sirenOfShameDevice.Disconnected += SirenOfShameDeviceDisconnected;
            _sirenOfShameDevice.TryConnect();
        }

        private void OnStatusChanged(StateChange status)
        {
            Invoke(() => RefreshConnectText(status.NewState));
        }
        
        private void RefreshConnectText(ConnectionState newState)
        {
            switch (newState)
            {
                case ConnectionState.Connected:
                    UpdateNetworkStatus(false, newState.ToString());
                    _connectedToServer = true;
                    _connectButton.Text = "Disconnect";
                    break;
                case ConnectionState.Disconnected:
                    UpdateNetworkStatus(false, newState.ToString());
                    _connectedToServer = false;
                    _connectButton.Text = "Connect";
                    break;
                default:
                    UpdateNetworkStatus(null, newState.ToString());
                    break;
            }
        }

        void SirenOfShameDeviceConnected(object sender, EventArgs e)
        {
            DeviceConnected();
        }

        void SirenOfShameDeviceDisconnected(object sender, EventArgs e)
        {
            DeviceConnected();
        }

        private void DeviceConnected()
        {
            Invoke(() =>
            {
                bool connected = _sirenOfShameDevice.IsConnected;
                _testSiren.Enabled = connected;
                _sirenStatus.Text = connected ? "Connected" : "Disconnected";
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
            if (!_connectedToServer)
            {
                await TryToConnect();
            }
            else
            {
                TryToDisconnect();
            }
        }

        private void UpdateNetworkStatus(bool? isBusy, string statusText)
        {
            _connectionStatus.Text = statusText;
            if (isBusy.HasValue)
            {
                _connectButton.Enabled = !isBusy.Value;
            }
        }

        private void TryToDisconnect()
        {
            _log.Debug("Attempting to disconnect from server");
            UpdateNetworkStatus(true, "Disconnecting");
            _sosOnlineService.Disconnect();
        }

        private async Task TryToConnect()
        {
            _log.Debug("Attempting to connect as " + _username.Text);
            var connectExtruderModel = GetConnectExtruderModel();
            UpdateNetworkStatus(true, "Verifying credentials");
            var result = await _sosOnlineService.ConnectExtruder(connectExtruderModel);
            if (result.Success)
            {
                SaveSettings();
            }
            else
            {
                UpdateNetworkStatus(false, "Failed To Connect");
                MessageBox.Show(result.ErrorMessage);
            }
        }

        private ConnectExtruderModel GetConnectExtruderModel()
        {
            var connectExtruderModel = new ConnectExtruderModel
            {
                UserName = _username.Text,
                Password = _encryptor.EncryptString(_password.Text),
                Name = _myname.Text,
            };
            return connectExtruderModel;
        }

        protected override void WndProc(ref Message m)
        {
            if (_sirenOfShameDevice != null)
            {
                try
                {
                    _sirenOfShameDevice.WndProc(ref m);
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }
            base.WndProc(ref m);
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
            OnAppExit();
            Invoke(Application.Exit);
        }

        private void ConfigureSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                e.Cancel = true;
                SetWindowState(FormWindowState.Minimized);
            }
            else
            {
                OnAppExit();
            }
        }

        private void OnAppExit()
        {
            TryToDisconnect();
        }

        private void SetWindowState(FormWindowState windowState)
        {
            WindowState = windowState;
            ShowInTaskbar = windowState != FormWindowState.Minimized;
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
                SetWindowState(FormWindowState.Normal);
            }
            Activate();
            Focus();
            BringToFront();
        }

        private void _notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowConfigureScreen();
        }

        private void RulesEngineSetTrayIcon(object sender, SetTrayIconEventArgs args)
        {
            if (args.TrayIcon == TrayIcon.Red)
                _notifyIcon.Icon = Properties.Resources.SirenOfShameTrayRed;
            else if (args.TrayIcon == TrayIcon.Green)
                _notifyIcon.Icon = Properties.Resources.SirenOfShameTrayGreen;
            else
                _notifyIcon.Icon = Properties.Resources.SirenOfShameTrayTriangle;
        }

        private async void ConfigureSettings_Load(object sender, EventArgs e)
        {
            var userHasEverSuccessfullyConnected = !string.IsNullOrEmpty(_settings.UserName);
            var windowState = userHasEverSuccessfullyConnected ? FormWindowState.Minimized : FormWindowState.Normal;
            SetWindowState(windowState);
            if (userHasEverSuccessfullyConnected)
            {
                await TryToConnect();
            }
        }

        // no need to marshall this to the UI thread b/c we don't do any UI work
        private void OnPlaySiren(int? ledPatternIndex, TimeSpan ledDuration, int? audioPatternIndex, TimeSpan audioDuration)
        {
            if (!_sirenOfShameDevice.IsConnected)
            {
                _log.Warn("Retrieved request to play siren, but siren wasn't connected");
                MessageBox.Show("Retrieved request to play siren, but siren wasn't connected");
                return;
            }

            LedPattern ledPattern = ledPatternIndex == null ? null : _sirenOfShameDevice.LedPatterns.ElementAtOrDefault(ledPatternIndex.Value);
            AudioPattern audioPattern = audioPatternIndex == null ? null : _sirenOfShameDevice.AudioPatterns.ElementAtOrDefault(audioPatternIndex.Value);
            PlaySiren(ledPattern, ledDuration, audioPattern, audioDuration);
        }

        private void TestSiren_Click(object sender, EventArgs e)
        {
            if (!_sirenOfShameDevice.IsConnected) return;
            
            PlaySiren(_sirenOfShameDevice.LedPatterns.First(), new TimeSpan(0, 0, 0, 10), null, new TimeSpan(0));
        }

        private void PlaySiren(LedPattern ledPattern, TimeSpan ledDuration, AudioPattern audioPattern, TimeSpan audioDuration)
        {
            if (!_sirenOfShameDevice.IsConnected) return;

            _sirenOfShameDevice.PlayLightPattern(ledPattern, ledDuration);
            _sirenOfShameDevice.PlayAudioPattern(audioPattern, audioDuration);
        }
    }
}
