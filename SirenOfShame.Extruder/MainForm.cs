using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using SirenOfShame.Extruder.Models;
using SirenOfShame.Extruder.Services;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Watcher;
using SetTrayIconEventArgs = SirenOfShame.Extruder.Models.SetTrayIconEventArgs;
using TrayIcon = SirenOfShame.Extruder.Models.TrayIcon;
using TrayNotifyEventArgs = SirenOfShame.Extruder.Services.TrayNotifyEventArgs;

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
        private readonly BrowserPage _browserPage = new BrowserPage();
        private PageType? _currentPage;

        public MainForm()
        {
            _settings = ExtruderSettings.GetAppSettings();
            _encryptor = new TripleDesStringEncryptor();

            InitializeComponent();

            DeviceConnectedOrDisconnected();
            RefreshConnectText(ConnectionState.Disconnected);

            InitializeSettingsPage();
            InitializePages();

            SubscribeEvents();
        }

        private void InitializePages()
        {
            _browserPage.Dock = DockStyle.Fill;
        }

        private void InitializeSettingsPage()
        {
            _settingsPage.Dock = DockStyle.Fill;
            _settingsPage.Settings = _settings;
            _settingsPage.OnToggleConnection += SettingsPageOnOnToggleConnection;
            _settingsPage.OnCloseSettings += SettingsPageOnOnCloseSettings;
            _settingsPage.OnTestSiren += SettingsPageOnTestSiren;
        }

        private void SettingsPageOnTestSiren(object sender, TestSirenEventArgs args)
        {
            OnSetLights(1, new TimeSpan(0, 0, 10));
            OnSetAudio(1, new TimeSpan(0, 0, 10));
        }

        private void SettingsPageOnOnCloseSettings(object sender, CloseSettingsEventArgs args)
        {
            if (_connectedToServer)
            {
                SetPage(PageType.BrowserPage);
            }
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
                _settings.InitializeConnectExtruderModel(connectExtruderModel);
                bool success = await TryToConnect(connectExtruderModel);
                if (success)
                {
                    SaveSettings(connectExtruderModel);
                    SetPage(PageType.BrowserPage);
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
            _sosOnlineService.SetAudio += OnSetAudio;
            _sosOnlineService.SetLights += OnSetLights;
            _sosOnlineService.ModalDialog += OnModalDialog;
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
            DeviceConnectedOrDisconnected();
        }

        void SirenOfShameDeviceDisconnected(object sender, EventArgs e)
        {
            DeviceConnectedOrDisconnected();
        }

        private void DeviceConnectedOrDisconnected()
        {
            Invoke(() =>
            {
                bool connected = _sirenOfShameDevice.IsConnected;
                if (connected)
                {
                    var firstTimeConnectingDevice = _settings.LedPatterns == null;
                    _settings.LedPatterns = JsonConvert.SerializeObject(_sirenOfShameDevice.LedPatterns);
                    _settings.AudioPatterns = JsonConvert.SerializeObject(_sirenOfShameDevice.AudioPatterns);
                    _settings.Save();
                    if (firstTimeConnectingDevice)
                    {
                        SilentlyReconnectToServer();
                    }
                }
                _settingsPage.DeviceConnected(connected);
                _sirenStatus.Text = connected ? "Connected" : "Disconnected";
            });
        }

        /// <summary>
        /// async void is kinda evil, but this will happen on a background thread and that's ok b/c the method name starts with Silently :)
        /// </summary>
        private async void SilentlyReconnectToServer()
        {
            if (!_connectedToServer) return;
            
            TryToDisconnect();
            var extruderModel = GetConnectExtruderModel();
            await TryToConnect(extruderModel);
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
            var result = new ConnectExtruderModel
            {
                UserName = _settings.UserName,
                Password = _settings.EncryptedPassword,
                Name = _settings.MyName,
            };

            _settings.InitializeConnectExtruderModel(result);
            return result;
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
                SetPage(PageType.BrowserPage);
            }
            else
            {
                SetPage(PageType.Settings);
            }
        }

        private void OnModalDialog(ModalDialogEventArgs obj)
        {
            Invoke(() => MessageBox.Show(this, obj.DialogText, obj.OkText));
        }

        // no need to marshall this to the UI thread b/c we don't do any UI work
        private void OnSetLights(int? ledPatternIndex, TimeSpan? ledDuration)
        {
            if (!_sirenOfShameDevice.IsConnected)
            {
                _log.Warn("Retrieved request to play siren, but siren wasn't connected");
                return;
            }

            LedPattern ledPattern = ledPatternIndex == null ? null : _sirenOfShameDevice.LedPatterns.FirstOrDefault(i => i.Id == ledPatternIndex);
            _sirenOfShameDevice.PlayLightPattern(ledPattern, ledDuration);
        }

        // no need to marshall this to the UI thread b/c we don't do any UI work
        private void OnSetAudio(int? audioPatternIndex, TimeSpan? audioDuration)
        {
            if (!_sirenOfShameDevice.IsConnected)
            {
                _log.Warn("Retrieved request to play siren, but siren wasn't connected");
                return;
            }

            AudioPattern audioPattern = audioPatternIndex == null ? null : _sirenOfShameDevice.AudioPatterns.FirstOrDefault(i => i.Id == audioPatternIndex);
            _sirenOfShameDevice.PlayAudioPattern(audioPattern, audioDuration);
        }

        private void SetPage(PageType newPage)
        {
            var oldPage = _currentPage;
            _currentPage = newPage;

            _refresh.Visible = newPage == PageType.BrowserPage;

            if (oldPage == newPage) return;

            _mainPanel.Controls.Clear();
            if (newPage == PageType.Settings)
            {
                _mainPanel.Controls.Add(_settingsPage);
            }
            else
            {
                _browserPage.EnsureConnected(_settings);
                _mainPanel.Controls.Add(_browserPage);
            }
        }

        private void NotifyIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowConfigureScreen();
            }
        }

        private void SettingsButton_ButtonClick(object sender, EventArgs e)
        {
            SetPage(PageType.Settings);
        }

        private void Refresh_ButtonClick(object sender, EventArgs e)
        {
            if (_browserPage == null) return;
            _browserPage.Refresh();
        }
    }

    public enum PageType
    {
        Settings = 0,
        BrowserPage = 1,
    }
}
