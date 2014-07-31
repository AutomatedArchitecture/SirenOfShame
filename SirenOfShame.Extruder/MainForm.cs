using System;
using System.Collections.Generic;
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
    public partial class MainForm : FormBase
    {
        private const int TIMEOUT = 3000;
        private readonly ILog _log = MyLogManager.GetLog(typeof(MainForm));
        private readonly ExtruderSettings _settings;
        private readonly TripleDesStringEncryptor _encryptor;
        private readonly SosOnlineService _sosOnlineService = new SosOnlineService();
        private readonly ISirenOfShameDevice _sirenOfShameDevice = new SirenOfShameDevice();
        private bool _connectedToServer;
        private readonly SettingsPage _settingsPage = new SettingsPage();
        private readonly LeadersPage _leadersPage = new LeadersPage();

        public MainForm()
        {
            _settings = ExtruderSettings.GetAppSettings();
            _encryptor = new TripleDesStringEncryptor();

            InitializeComponent();

            DeviceConnected();
            RefreshConnectText(ConnectionState.Disconnected);

            InitializeSettingsPage();
            InitializePages();

            SubscribeEvents();
        }

        private void InitializePages()
        {
            _leadersPage.Dock = DockStyle.Fill;
        }

        private void InitializeSettingsPage()
        {
            _settingsPage.Dock = DockStyle.Fill;
            _settingsPage.Settings = _settings;
            _settingsPage.OnToggleConnection += SettingsPageOnOnToggleConnection;
        }

        private async Task SettingsPageOnOnToggleConnection(object sender, ToggleConnectionEventArgs args)
        {
            if (!_connectedToServer)
            {
                ConnectExtruderModel connectExtruderModel = new ConnectExtruderModel
                {
                    UserName = args.UserName,
                    Password = _encryptor.EncryptString(args.PlainTextPassword),
                    Name = args.Name,
                };
                bool success = await TryToConnect(connectExtruderModel);
                if (success)
                {
                    SaveSettings(connectExtruderModel);
                    SetPage(PageType.Builds);
                }
            }
            else
            {
                TryToDisconnect();
            }
        }

        private void SubscribeEvents()
        {
            _sosOnlineService.StatusChanged += OnStatusChanged;
            _sosOnlineService.PlaySiren += OnPlaySiren;
            _sosOnlineService.SetTrayIcon += RulesEngineSetTrayIcon;
            _sosOnlineService.TrayNotify += RulesEngineTrayNotify;
            _sirenOfShameDevice.Connected += SirenOfShameDeviceConnected;
            _sirenOfShameDevice.Disconnected += SirenOfShameDeviceDisconnected;
            _sirenOfShameDevice.TryConnect();
        }

        private static readonly Dictionary<SosToolTipIcon, ToolTipIcon> _toolTipIconMapping = new Dictionary<SosToolTipIcon, ToolTipIcon> 
        {
            { SosToolTipIcon.None, ToolTipIcon.None },
            { SosToolTipIcon.Info, ToolTipIcon.Info },
            { SosToolTipIcon.Warning, ToolTipIcon.Warning },
            { SosToolTipIcon.Error, ToolTipIcon.Error },
        };
        
        private void RulesEngineTrayNotify(object sender, TrayNotifyEventArgs args)
        {
            ToolTipIcon tipIcon = _toolTipIconMapping[args.TipIcon];
            Invoke(() => _notifyIcon.ShowBalloonTip(TIMEOUT, args.Title, args.TipText, tipIcon));
        }

        private void OnStatusChanged(StateChange status)
        {
            Invoke(() => RefreshConnectText(status.NewState));
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
                _settingsPage.DeviceConnected(connected);
                _sirenStatus.Text = connected ? "Connected" : "Disconnected";
            });
        }

        private void UpdateNetworkStatus(bool? isBusy, string statusText)
        {
            _connectionStatus.Text = statusText;
            _settingsPage.UpdateNetworkStatus(isBusy, statusText);
        }

        private void TryToDisconnect()
        {
            _log.Debug("Attempting to disconnect from server");
            UpdateNetworkStatus(true, "Disconnecting");
            _sosOnlineService.Disconnect();
        }

        private async Task<bool> TryToConnect(ConnectExtruderModel connectExtruderModel)
        {
            _log.Debug("Attempting to connect as " + connectExtruderModel.UserName);
            UpdateNetworkStatus(true, "Verifying credentials");
            var result = await _sosOnlineService.ConnectExtruder(connectExtruderModel);
            if (!result.Success) {
                UpdateNetworkStatus(false, "Failed To Connect");
                MessageBox.Show(result.ErrorMessage);
            }
            return result.Success;
        }

        private void RefreshConnectText(ConnectionState newState)
        {
            switch (newState)
            {
                case ConnectionState.Connected:
                    UpdateNetworkStatus(false, newState.ToString());
                    _connectedToServer = true;
                    break;
                case ConnectionState.Disconnected:
                    UpdateNetworkStatus(false, newState.ToString());
                    _connectedToServer = false;
                    break;
                default:
                    UpdateNetworkStatus(null, newState.ToString());
                    break;
            }
            _settingsPage.RefreshIsConnected(_connectedToServer);
        }


        private void SaveSettings(ConnectExtruderModel connectExtruderModel)
        {
            _settings.UserName = connectExtruderModel.UserName;
            _settings.EncryptedPassword = connectExtruderModel.Password;
            _settings.MyName = connectExtruderModel.Name;
            _settings.Save();
        }

        private ConnectExtruderModel GetConnectExtruderModel()
        {
            return new ConnectExtruderModel
            {
                UserName = _settings.UserName,
                Password = _settings.EncryptedPassword,
                Name = _settings.MyName,
            };
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
                var connectExtruderModel = GetConnectExtruderModel();
                await TryToConnect(connectExtruderModel);
                SetPage(null);
            }
            else
            {
                SetPage(PageType.Settings);
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

        private void PlaySiren(LedPattern ledPattern, TimeSpan ledDuration, AudioPattern audioPattern, TimeSpan audioDuration)
        {
            if (!_sirenOfShameDevice.IsConnected) return;

            _sirenOfShameDevice.PlayLightPattern(ledPattern, ledDuration);
            _sirenOfShameDevice.PlayAudioPattern(audioPattern, audioDuration);
        }

        private void _settingsButton_Click(object sender, EventArgs e)
        {
            SetPage(PageType.Settings);
        }

        private void SetPage(PageType? pageType)
        {
            if (pageType == null)
            {
                // todo: remember last page
                pageType = PageType.Builds;
            }
            _mainPanel.Controls.Clear();
            if (pageType == PageType.Settings)
            {
                _mainPanel.Controls.Add(_settingsPage);
            }
            else
            {
                _leadersPage.Navigate(_settings, pageType.Value);
                _mainPanel.Controls.Add(_leadersPage);
            }
        }

        private void _buildsButton_Click(object sender, EventArgs e)
        {
            SetPage(PageType.Builds);
        }

        private void _leadersButton_Click(object sender, EventArgs e)
        {
            SetPage(PageType.Leaders);
        }

        private void _newsButton_Click(object sender, EventArgs e)
        {
            SetPage(PageType.News);
        }

        private void _notifyIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowConfigureScreen();
            }
        }
    }

    public enum PageType
    {
        Settings = 0,
        Builds = 1,
        Leaders = 2,
        News = 3,
    }
}
