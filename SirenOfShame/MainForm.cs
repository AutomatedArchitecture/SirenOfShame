using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Services;
using log4net;
using SirenOfShame.Configuration;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.SirenConfiguration;
using Timer = System.Windows.Forms.Timer;

namespace SirenOfShame
{
    public partial class MainForm : FormBase
    {
        private const int TIMEOUT = 3000;
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(MainForm));
        SirenOfShameSettings _settings;
        private RulesEngine _rulesEngine;
        private string _logFilename;
        private bool _canViewLogs;
        readonly Timer _showAlertAnimation = new Timer();
        readonly SosOnlineService _sosOnlineService = new SosOnlineService();
        private readonly SoundService _soundService = new SoundService();
        private bool _isFullscreen = false;

        [Import(typeof(ISirenOfShameDevice))]
        public ISirenOfShameDevice SirenOfShameDevice { private get; set; }

        public MainForm()
        {
            _settings = GetAppSettings();
            _log.Info("MainForm Open");
            IocContainer.Instance.Compose(this);
            InitializeComponent();
            InitializeNotifyIcon();

            InitializeToolbar();
            InitializeViewBuilds();
            InitializeViewUser();
            InitializeNewsFeed();
            InitializeUserList();
            InitializeSirenOfShameDevice();
            
            SetAutomaticUpdaterSettings();

            _showAlertAnimation.Interval = 1;
            _showAlertAnimation.Tick += ShowAlertAnimationTick;

            InitializeLogging();
            ShowInMainWindow(MainWindowEnum.ViewBuilds);
        }

        private void InitializeWindow(bool startup)
        {
            TopMost = _settings.AlwaysOnTop;
            if (startup && _settings.StartInFullScreen)
            {
                SetFullScreenMode(true);
            }
            if (startup)
            {
                var lastFontSize = _settings.FontSize;
                // it seems to be necessary to set a baseline font size
                SetFontSize(SirenOfShameSettings.DEFAULT_FONT_SIZE);
                SetFontSize(lastFontSize);
            }
        }

        protected virtual SirenOfShameSettings GetAppSettings()
        {
            return SirenOfShameSettings.GetAppSettings();
        }

        protected virtual void InitializeNotifyIcon()
        {
            NotityIconVisible = true;
        }
        
        private void InitializeToolbar()
        {
            ShowAllUsers = false;
        }

        private void InitializeLogging()
        {
            try
            {
                _logFilename = MyLogManager.GetLogFilename();
                _viewLog.Enabled = true;
                _canViewLogs = true;
            } catch (Exception)
            {
                _viewLog.Enabled = false;
                _canViewLogs = false;
            }
        }

        private void InitializeUserList()
        {
            _userList.OnUserSelected += UsersListOnOnUserSelected;
            _userList.Initialize(_settings, _avatarImageList);
        }

        private void InitializeViewUser()
        {
            viewUser1.OnClose += ViewUserOnClose;
            viewUser1.OnUserChangedAvatarId += ViewUser1OnOnUserChangedAvatarId;
            viewUser1.OnUserDisplayNameChanged += UsersListOnOnUserDisplayNameChanged;
            viewUser1.Initilaize(_settings);
        }

        private void InitializeViewBuilds()
        {
            _viewBuilds.OnGettingStartedClick += ViewBuildsOnOnGettingStartedClick;
            _viewBuilds.SelectedBuildChanged += ViewBuildsOnSelectedBuildChanged;
            _viewBuilds.Initialize(_settings);
        }

        private void ViewBuildsOnSelectedBuildChanged(object sender, SelectedBuildChangedArgs args)
        {
            _newsFeed1.AddBuildFilter(_settings, args.BuildDefinitionId, _avatarImageList);
        }

        private void InitializeNewsFeed()
        {
            _newsFeed1.OnUserClicked += NewsFeedOnUserClicked;
            _newsFeed1.OnSendMessageToSosOnline += NewsFeedOnSendMessageToSosOnline;
            _newsFeed1.ClearFilter(_settings, _avatarImageList); // this will read in 
        }

        private void NewsFeedOnSendMessageToSosOnline(object sender, SendMessageToSosOnlineArgs args)
        {
            _sosOnlineService.SendMessage(_settings, args.Message);
        }

        private void ViewBuildsOnOnGettingStartedClick(object sender, GettingStartedOpenDialogArgs args)
        {
            if (args.GettingStartedClickType == GettingStartedClickTypeEnum.ConfigureServer)
            {
                OpenConfigureServerDialog();
            }
            if (args.GettingStartedClickType == GettingStartedClickTypeEnum.ConfigureSosOnline)
            {
                OpenConfigureSosOnlineDialog();
            }
        }

        private void InitializeSirenOfShameDevice()
        {
            if (SirenOfShameDevice == null)
            {
                _log.Error("SirenOfShameDevice was null in MainForm. This probably means that MEF failed to perform the dependency injection.");
                return;
            }
            SirenOfShameDevice.Connected += SirenofShameDeviceConnected;
            SirenOfShameDevice.Disconnected += SirenofShameDeviceDisconnected;
            if (SirenOfShameDevice.IsConnected)
            {
                SirenofShameDeviceConnected(this, new EventArgs());
            } else
            {
                SirenofShameDeviceDisconnected(this, new EventArgs());
            }
        }

        private void ViewUser1OnOnUserChangedAvatarId(object sender, UserChangedAvatarIdArgs args)
        {
            _newsFeed1.ChangeUserAvatarId(args.RawName, args.NewImageIndex);
            _userList.ChangeUserAvatarId(args.RawName, args.NewImageIndex);
        }

        private void NewsFeedOnUserClicked(object sender, UserClickedArgs args)
        {
            ShowViewUserPage(args.RawUserName);
        }

        private void UsersListOnOnUserDisplayNameChanged(object sender, UserDisplayNameChangedArgs args)
        {
            _lastRefreshStatusEventArgs.RefreshDisplayNames(_settings);
            _viewBuilds.RefreshBuildStatuses(_lastRefreshStatusEventArgs);
            _newsFeed1.RefreshDisplayNames(args);
            _userList.RefreshDisplayNames(args);
        }

        private void UsersListOnOnUserSelected(object sender, UserSelectedArgs args)
        {
            var rawName = args.RawName;
            ShowViewUserPage(rawName);
        }

        protected virtual void ShowInMainWindow(MainWindowEnum mainWindow)
        {
            this.SuspendDrawing(() =>
            {
                _viewBuilds.Visible = mainWindow == MainWindowEnum.ViewBuilds;
                viewUser1.Visible = mainWindow == MainWindowEnum.ViewUser;
                if (mainWindow == MainWindowEnum.ViewBuilds)
                {
                    _viewBuilds.RefreshBuildStatuses();
                }
            });
        }
        
        private void ShowViewUserPage(string rawName)
        {
            var aUserIsSelected = rawName != null;
            if (!aUserIsSelected) ShowInMainWindow(MainWindowEnum.ViewBuilds);
            if (!aUserIsSelected) return;
            var selectedPerson = _settings.People.FirstOrDefault(i => i.RawName == rawName);
            if (selectedPerson == null)
            {
                _log.Error("Unable to find user " + rawName);
                return;
            }
            ShowViewUserPage(selectedPerson);
        }

        private void ShowViewUserPage(PersonSetting selectedPerson)
        {
            if (selectedPerson == null) return;
            ShowInMainWindow(MainWindowEnum.ViewUser);
            viewUser1.SetUser(selectedPerson, _avatarImageList);
            _newsFeed1.AddUserFilter(_settings, selectedPerson, _avatarImageList);
        }

        private void SetAutomaticUpdaterSettings()
        {
            if (_settings == null) return;
            bool performUpdates = _settings.UpdateLocation != UpdateLocation.Never;
            var updatePath = GetUpdatePath();
            string server = updatePath + "wyserver.zip";
            _automaticUpdater.wyUpdateCommandline = " \"-server=" + server + "\" \"-updatepath=" + updatePath + "\"";
            _automaticUpdater.Enabled = performUpdates;
            _automaticUpdater.Visible = performUpdates;
        }

        private string GetUpdatePath()
        {
            if (_settings.UpdateLocation == UpdateLocation.Other)
            {
                return _settings.UpdateLocationOther;
            }
            if (_settings.UpdateLocation == UpdateLocation.Auto)
            {
                return "http://blueink.biz/SoS/updates/";
            }
            return "";
        }

        protected override void WndProc(ref Message m)
        {
            if (SirenOfShameDevice != null)
            {
                SirenOfShameDevice.WndProc(ref m);
            }
            base.WndProc(ref m);
        }

        private void RulesEngineModalDialog(object sender, ModalDialogEventArgs args)
        {
            if (InFullscreenMode) return;
            BuildFailedMessageBox.ShowOnce("Siren of Shame", args.DialogText, args.OkText);
        }

        private void RulesEngineStatsChanged(object sender, StatsChangedEventArgs args)
        {
            Invoke(RefreshStats);
        }

        private void RefreshStats()
        {
            try
            {
                RefreshUserStats();
                _viewBuilds.RefreshStats();
            }
            catch (Exception ex)
            {
                ExceptionMessageBox.Show(this, "Drat", "This tricky, hard to reproduce bug just occurred while refreshing user stats. Would you mind please sending us the details?", ex);
                _log.Error("Error while trying to refresh stats", ex);
            }
        }

        /// <summary>
        /// For entering into full screen mode
        /// </summary>
        private RefreshStatusEventArgs _lastRefreshStatusEventArgs = null;
        
        private void RulesEngineRefreshRefreshStatus(object sender, RefreshStatusEventArgs args)
        {
            Invoke(() =>
            {
                _lastRefreshStatusEventArgs = args;
                _viewBuilds.RefreshBuildStatuses(args);
            });
        }

        private bool InFullscreenMode
        {
            get { return _isFullscreen; }
        }

        private void SirenofShameDeviceConnected(object sender, EventArgs e)
        {
            bool firstTimeSirenHasEverBeenConnected = !_settings.SirenEverConnected;
            if (firstTimeSirenHasEverBeenConnected)
            {
                _settings.SirenEverConnected = true;
                _settings.InitializeRulesForConnectedSiren();
                _settings.Save();
            }

            EnableSirenMenuItem(true);
        }

        private void EnableSirenMenuItem(bool isSirenConnected)
        {
            Invoke(() =>
            {
                _testSiren.Enabled = isSirenConnected;
            });
        }

        private void SirenofShameDeviceDisconnected(object sender, EventArgs args)
        {
            EnableSirenMenuItem(false);
        }

        private async void MainFormLoad(object sender, EventArgs e)
        {
            ShowRibbon(false);
            _panelAlertHeight = _panelAlert.Height;
            _log.Debug("Form1 loaded");
            if (_settings == null)
            {
                _settings = new SirenOfShameSettings();
            }

            TryUpgrade();

            if (_settings.ShowUpgradeWindowAtNextOpportunity)
            {
                FindOldAchievements.TryFindOldAchievements(_settings);
                _settings.ShowUpgradeWindowAtNextOpportunity = false;
            }

            StartWatchingBuild();
            _sosOnlineService.OnNewSosOnlineNotification += SosOnlineServiceOnNewSosOnlineNotification;
            _sosOnlineService.OnSosOnlineStatusChange += SosOnlineOnStatusChange;
            var realtimeConnectionResult = await _sosOnlineService.StartRealtimeConnection(_settings);

            if (realtimeConnectionResult.Success && realtimeConnectionResult.ChatEnabled)
            {
                _newsFeed1.EnableChat();
            }
            
            RefreshStats();
            SetMuteButton();
            InitializeWindow(startup: true);
        }

        private void SosOnlineOnStatusChange(object sender, SosOnlineStatusChangeArgs args)
        {
            Invoke(() =>
            {
                _sosOnlineStatus.Text = string.Format("Sos Online: " + args.TextStatus);
                if (args.Exception != null) _sosOnlineError.Tag = args.Exception;
                _sosOnlineError.Visible = args.Exception != null;
            });
        }

        private void SosOnlineServiceOnNewSosOnlineNotification(object sender, NewSosOnlineNotificationArgs args)
        {
            // this may result in a web request to retrieve the person's image, so keep it on some other thread
            SosOnlinePerson sosOnlinePerson = _sosOnlineService.CreateSosOnlinePersonFromSosOnlineNotification(args, _avatarImageList);
            Invoke(() =>
            {
                NewNewsItemEventArgs newNewsItemEventArgs = new NewNewsItemEventArgs
                {
                    EventDate = DateTime.Now,
                    Person = sosOnlinePerson,
                    Title = args.Message,
                    AvatarImageList = _avatarImageList,
                    NewsItemType = NewsItemTypeEnum.SosOnlineComment
                };
                _newsFeed1.AddNewsItem(newNewsItemEventArgs);
            });
        }

        private void TryUpgrade()
        {
            _settings.TryUpgrade();
        }

        private RulesEngine RulesEngine
        {
            get { return _rulesEngine ?? (_rulesEngine = GetRulesEngine()); }
        }

        private void StartWatchingBuild()
        {
            ControlHelpers.SuspendLayout(_viewBuilds, () => RulesEngine.Start(initialStart: true));
        }

        private void StopWatchingBuild()
        {
            RulesEngine.Stop();
        }

        private RulesEngine GetRulesEngine()
        {
            var rulesEngine = new RulesEngine(_settings);
            rulesEngine.PlayWindowsAudio += RulesEnginePlayWindowsAudio;
            rulesEngine.UpdateStatusBar += RulesEngineUpdateStatusBar;
            rulesEngine.RefreshStatus += RulesEngineRefreshRefreshStatus;
            rulesEngine.TrayNotify += RulesEngineTrayNotify;
            rulesEngine.ModalDialog += RulesEngineModalDialog;
            rulesEngine.SetAudio += RulesEngineSetAudio;
            rulesEngine.SetLights += RulesEngineSetLights;
            rulesEngine.SetTrayIcon += RulesEngineSetTrayIcon;
            rulesEngine.StatsChanged += RulesEngineStatsChanged;
            rulesEngine.NewAlert += RulesEngineNewAlert;
            rulesEngine.NewAchievement += RulesEngineNewAchievement;
            rulesEngine.NewNewsItem += RulesEngineNewNewsItem;
            rulesEngine.NewUser += RulesEngineNewUser;
            return rulesEngine;
        }

        private void RulesEngineNewUser(object sender, NewUserEventArgs args)
        {
            Invoke(() => _userList.NewUser(_avatarImageList, args.RawName));
        }

        private void RulesEngineNewNewsItem(object sender, NewNewsItemEventArgs args)
        {
            Invoke(() =>
            {
                args.AvatarImageList = _avatarImageList;
                _newsFeed1.AddNewsItem(args);
            });
        }

        private void RulesEngineNewAchievement(object sender, NewAchievementEventArgs args)
        {
            foreach (var achievement in args.Achievements)
            {
                _log.Debug(args.Person + " achieved " + achievement.Name);
            }
            Invoke(() =>
            {
                viewUser1.NewAchievements(args.Person);
                if (_settings.AchievementAlertPreference == AchievementAlertPreferenceEnum.Never) return;
                if (_settings.AchievementAlertPreference == AchievementAlertPreferenceEnum.OnlyForMe && !_settings.IsMeOrDefault(args.Person, true)) return;
                foreach (var achievement in args.Achievements)
                {
                    NewAchievement.ShowForm(_settings, achievement, args.Person, this, modal: false);
                }
                ShowViewUserPage(args.Person);
            });
        }

        private void RulesEnginePlayWindowsAudio(object sender, PlayWindowsAudioEventArgs args)
        {
            // note: not doing an invoke because so far this code doesn't require the UI thread
            _soundService.Play(args.Location);
        }

        private bool _showAlert;
        private DateTime _alertDate;
        private string _alertUrl;
        
        private void RulesEngineNewAlert(object sender, NewAlertEventArgs args)
        {
            Invoke(() =>
            {
                _showAlert = true;
                _alertUrl = args.Url;
                _panelAlert.Visible = true;
                _panelAlert.Height = 1;
                _labelAlert.Text = args.Message;
                _details.Location = new Point(_labelAlert.Width + 7, _labelAlert.Location.Y);
                _showAlertAnimation.Start();
                _alertDate = args.AlertDate;
            });
        }

        private void RulesEngineSetTrayIcon(object sender, SetTrayIconEventArgs args)
        {
            if (args.TrayIcon == TrayIcon.Red)
                notifyIcon.Icon = Properties.Resources.SirenOfShameTrayRed;
            else if (args.TrayIcon == TrayIcon.Green)
                notifyIcon.Icon = Properties.Resources.SirenOfShameTrayGreen;
            else
                notifyIcon.Icon = Properties.Resources.SirenOfShameTrayTriangle;
        }

        private void RulesEngineSetLights(object sender, SetLightsEventArgs args)
        {
            if (SirenOfShameDevice.IsConnected)
            {
                SirenOfShameDevice.PlayLightPattern(args.LedPattern, args.TimeSpan);
            }
        }

        private void RulesEngineSetAudio(object sender, SetAudioEventArgs args)
        {
            if (SirenOfShameDevice.IsConnected)
            {
                SirenOfShameDevice.PlayAudioPattern(args.AudioPattern, args.TimeSpan);
            }
        }

        private void RulesEngineTrayNotify(object sender, TrayNotifyEventArgs args)
        {
            if (InFullscreenMode) return;
            Invoke(() => notifyIcon.ShowBalloonTip(TIMEOUT, args.Title, args.TipText, args.TipIcon));
        }

        private void RulesEngineUpdateStatusBar(object sender, UpdateStatusBarEventArgs args)
        {
            Invoke(() =>
            {
                _lastStatusUpdate.Text = args.StatusText;
                var thereWasAnException = args.Exception != null;
                _toolStripSplitErrorButton.Visible = thereWasAnException;
                _toolStripSplitErrorButton.Tag = args.Exception;
            });
        }

        private void MainFormMove(object sender, EventArgs e)
        {
            //If we are minimizing the form then hide it so it doesn't show up on the task bar
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            var isWyUpdateClosingUs = CheckIfWyUpdateIsClosingUs();
            _log.Debug("ClosingForInstall: " + _automaticUpdater.ClosingForInstall);
            if (!_automaticUpdater.ClosingForInstall && !isWyUpdateClosingUs && WindowState != FormWindowState.Minimized)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
            }
            else
            {
                RulesEngine.Stop();
            }
        }

        // this is a hack to determine if we are being called by wyUpdate
        private bool CheckIfWyUpdateIsClosingUs()
        {
            try
            {
                if (new StackTrace().ToString().Contains("auBackend_CloseAppNow"))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Could not determine if wyUpdate is closing us", ex);
            }
            return false;
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
            Focus();
        }

        private void NotifyIconDoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }

            Activate();
            Focus();
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Hide();
            WindowState = FormWindowState.Minimized;
            Invoke(Application.Exit);
        }

        private void ConfigureRulesClick(object sender, EventArgs e)
        {
            var configureRules = new ConfigureRules(_settings);
            configureRules.ShowDialog(this);
        }

        private void SosOnlineClick(object sender, EventArgs e)
        {
            OpenConfigureSosOnlineDialog();
        }

        private void OpenConfigureSosOnlineDialog()
        {
            bool didNotPreviouslySyncBuildStatuses = _settings.SosOnlineWhatToSync != WhatToSyncEnum.BuildStatuses;

            var configureSosOnline = new ConfigureSosOnline(_settings);
            configureSosOnline.SyncAllBuildStatuses += ConfigureSosOnlineSyncAllBuildStatuses;
            configureSosOnline.ShowDialog(this);

            bool isNowSyncingBuildStatuses = _settings.SosOnlineWhatToSync == WhatToSyncEnum.BuildStatuses;
            if (didNotPreviouslySyncBuildStatuses && isNowSyncingBuildStatuses)
            {
                _rulesEngine.SyncAllBuildStatuses();
            }

            _viewBuilds.ReinitializeGettingStarted();
        }

        private void ConfigureSosOnlineSyncAllBuildStatuses(object sender, SyncAllBuildStatusesArgs args)
        {
            _rulesEngine.SyncAllBuildStatuses();
        }

        private void ConfigureServersClick(object sender, EventArgs e)
        {
            OpenConfigureServerDialog();
        }

        private void OpenConfigureServerDialog()
        {
            bool anyChanges = ConfigureServers.Show(_settings);
            if (anyChanges)
            {
                _settings.ClearDuplicateNameCache();
                StopWatchingBuild();
                _rulesEngine = null; // reset the rules engine in case it changed (e.g. from TFS to Team City)
                StartWatchingBuild();
            }
            Activate();
            Focus();
            _viewBuilds.ReinitializeGettingStarted();
        }

        private void TestSirenClick(object sender, EventArgs e)
        {
            var testSiren = new TestSiren();
            testSiren.ShowDialog(this);
        }

        private void ConfigureSirenClick(object sender, EventArgs e)
        {
            var configureSounds = new ConfigureSounds(_settings);
            configureSounds.ShowDialog(this);
        }

        protected enum MainWindowEnum
        {
            ViewBuilds = 0,
            ViewUser = 1,
        }

        private void RefreshUserStats()
        {
            _userList.RefreshUserStats();
        }

        private void OpenSettingsClick(object sender, EventArgs e)
        {
            StopWatchingBuild();
            Settings settings = new Settings(_settings);
            settings.ShowDialog(this);
            RefreshStats(); // just in case they clicked reset reputation
            SetAutomaticUpdaterSettings(); // just in case they changed the updater settings
            InitializeWindow(startup: false); // just in case they changed "always on top"
            StartWatchingBuild();
        }

        private void TimeboxEnforcerClick(object sender, EventArgs e)
        {
            TimeboxEnforcer timeboxEnforcer = new TimeboxEnforcer();
            timeboxEnforcer.Show(this);
        }

        private void HelpClick(object sender, EventArgs e)
        {
            HelpAbout helpAbout = new HelpAbout();
            helpAbout.ShowDialog(this);
        }

        private void ConfigurationMoreClick(object sender, EventArgs e)
        {
            Point pt = _configurationMore.Location;
            pt.X += _configurationMore.Width;
            pt.Y += _configurationMore.Height;
            _configurationMenu.Show(this, pt);
        }

        private void SirenMoreClick(object sender, EventArgs e)
        {
            Point pt = _sirenMore.Location;
            pt.X += _sirenMore.Width;
            pt.Y += _sirenMore.Height;
            _sirenMenu.Show(this, pt);
        }

        private void CheckForUpdatesClick(object sender, EventArgs e)
        {
            CheckForUpdates();
        }

        public void CheckForUpdates()
        {
            SetAutomaticUpdaterSettings();
            _automaticUpdater.ForceCheckForUpdate(true);
        }

        private void ViewLogClick(object sender, EventArgs e)
        {
            ViewLogs();
        }

        public void ViewLogs()
        {
            try
            {
                Process.Start(_logFilename);
            } catch (Win32Exception)
            {
                var directoryName = Path.GetDirectoryName(_logFilename);
                if (directoryName == null)
                {
                    _log.Error(_logFilename + " didn't contain a directory");
                    return;
                }
                SosMessageBox.Show("Can't Open Log", "Sorry there is no app associated with .log files (you really are a developer, right?), anyway we'll just open the location of the log files for you.  The logs follow the pattern 'SirenOfShame*.log'", "Do It");
                Process.Start(directoryName);
            }
        }

        public bool CanViewLogs
        {
            get { return _canViewLogs; }
        }

        public int AvatarCount
        {
            get { return _avatarImageList.Images.Count; }
        }

        private void RefreshClick(object sender, EventArgs e)
        {
            RulesEngine.RefreshAll();
        }

        private void SirenUpgradeFirmwareClick(object sender, EventArgs e)
        {
            SirenFirmwareUpgrade upgrade = new SirenFirmwareUpgrade();
            upgrade.ShowDialog(this);
        }

        private void FullscreenClick(object sender, EventArgs e)
        {
            ShowFullscreen();
        }

        private void SetFullScreenMode(bool makeFullScreen)
        {
            _isFullscreen = makeFullScreen;
            ShowRibbon(false);
            TopMost = _isFullscreen;
            MaximizeBox = !_isFullscreen; // this is necessary to make full screen mode cover the taskbar for some reason ?
            WindowState = _isFullscreen ? FormWindowState.Maximized : FormWindowState.Normal;
            FormBorderStyle = _isFullscreen ? FormBorderStyle.None : FormBorderStyle.Sizable;
        }

        private void ShowFullscreen()
        {
            SetFullScreenMode(!_isFullscreen);
        }

        private void MuteClick(object sender, EventArgs e)
        {
            _settings.Mute = !_settings.Mute;
            _settings.Save();
            SetMuteButton();
        }

        private void SetMuteButton()
        {
            _mute.ImageKey = _settings.Mute ? "loudspeaker_forbidden.bmp" : "loudspeaker.bmp";
            _mute.Text = _settings.Mute ? "Unmute" : "Mute";
        }

        private void CloseAlertClick(object sender, EventArgs e)
        {
            _showAlert = false;
            _showAlertAnimation.Start();
            _settings.AlertClosed = _alertDate;
            _settings.Save();
        }

        int _panelAlertHeight;

        private void ShowAlertAnimationTick(object sender, EventArgs e)
        {
            bool hideAlert = !_showAlert;
            if (hideAlert && _panelAlert.Height > 0)
            {
                _panelAlert.Height -= 2;
            } 
            else
            {
                if (_showAlert && _panelAlert.Height < _panelAlertHeight)
                {
                    _panelAlert.Height += 1;
                }
                else
                {
                    _panelAlert.Visible = _showAlert;
                    if (_showAlert)
                    {
                        _panelAlert.Height = _panelAlertHeight;
                    }
                    _showAlertAnimation.Stop();
                }
            }
        }

        private void DetailsLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_alertUrl.StartsWith("http://"))
            {
                ProcessStartInfo sInfo = new ProcessStartInfo(_alertUrl);
                Process.Start(sInfo);
            }
        }

        private void ViewUserOnClose(object sender, CloseScreenArgs args)
        {
            ShowInMainWindow(MainWindowEnum.ViewBuilds);
            _newsFeed1.ClearFilter(_settings, _avatarImageList);
        }

        private void ToolStripSplitErrorButtonClick(object sender, EventArgs e)
        {
            var exception = (Exception) _toolStripSplitErrorButton.Tag;
            ExceptionMessageBox.Show(this, "Connection Error", exception.Message, exception);
        }

        private void ShowRibbonClick(object sender, EventArgs e)
        {
            ShowRibbon(!_ribbonPanel.Visible);
        }

        private void ShowRibbon(bool show)
        {
            _showRibbon.Image = show ? Properties.Resources.navigate_up : Properties.Resources.navigate_down2;
            _ribbonPanel.SuspendDrawing(() => _ribbonPanel.Visible = show);
        }

        private void MainFormResizeBegin(object sender, EventArgs e)
        {
            SuspendLayout();
        }

        private void MainFormResizeEnd(object sender, EventArgs e)
        {
            ResumeLayout();
        }

        private void SosOnlineErrorClick(object sender, EventArgs e)
        {
            var exception = (Exception)_sosOnlineError.Tag;
            ExceptionMessageBox.Show(this, "Sos Online Error", exception.Message, exception);
        }

        protected bool NotityIconVisible
        {
            set { notifyIcon.Visible = value; }
        }

        private bool _showAllUsers;

        private bool ShowAllUsers
        {
            get { return _showAllUsers; }
            set
            {
                _viewAllUsers.ImageKey = value ? "users4_checkbox_checked.bmp" : "users4_checkbox_unchecked.bmp";
                _showAllUsers = value;
                _userList.SetShowAllUsers(value);
            }
        }
        
        private void ViewAllUsersClick(object sender, EventArgs e)
        {
            ShowAllUsers = !ShowAllUsers;
        }

        private void UserMappingsClick(object sender, EventArgs e)
        {
            var userMappings = new UserMappings(_settings);
            userMappings.ShowDialog(this);
            _userList.RefreshUserPanelVisibility();
        }

        readonly Dictionary<Keys, int> _numericKeyMappings = new Dictionary<Keys, int>
            {
                { Keys.D1, 0 },
                { Keys.D2, 1 },
                { Keys.D3, 2 },
                { Keys.D4, 3 },
                { Keys.D5, 4 },
                { Keys.D6, 5 },
                { Keys.D7, 6 },
                { Keys.D8, 7 },
                { Keys.D9, 8 },
                { Keys.D0, 0 }
            };

        private void ZoomOut()
        {
            SetFontSize(Font.Size * .9f);
        }

        private void ZoomIn()
        {
            SetFontSize(Font.Size * 1.1f);
        }

        private void ResetZoom()
        {
            SetFontSize(SirenOfShameSettings.DEFAULT_FONT_SIZE);
        }

        private void SetFontSize(float fontSize)
        {
            this.SuspendDrawing(() =>
            {
                var oldHeight = Height;
                var oldWidth = Width;
                Font = new Font(Font.FontFamily, fontSize);
                Height = oldHeight;
                Width = oldWidth;
            });
            _settings.FontSize = fontSize;
            _settings.Save();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            int numberPressed;
            var wasNumberPressed = _numericKeyMappings.TryGetValue(e.KeyCode, out numberPressed);
            
            if (e.Control && e.KeyCode == Keys.Oemplus)
            {
                ZoomIn();
                return;
            }
            if (e.Control && e.KeyCode == Keys.OemMinus)
            {
                ZoomOut();
                return;
            }
            if (e.Control && wasNumberPressed && numberPressed == 0)
            {
                ResetZoom();
                return;
            }

            if (e.KeyCode == Keys.Escape && InFullscreenMode)
            {
                SetFullScreenMode(false);
            }
            if (e.KeyCode == Keys.Back)
            {
                ShowInMainWindow(MainWindowEnum.ViewBuilds);
                _viewBuilds.InitializeForBuild();
            }
            if (e.Alt && e.KeyCode == Keys.F)
            {
                ShowFullscreen();
            }
            if (wasNumberPressed)
            {
                if (e.Alt)
                {
                    var personToShow = _settings.People.ElementAtOrDefault(numberPressed);
                    if (personToShow == null) return;
                    ShowViewUserPage(personToShow);
                }
                else
                {
                    ShowInMainWindow(MainWindowEnum.ViewBuilds);
                    _viewBuilds.InitializeForBuild(numberPressed);
                }
            }
        }
    }
}