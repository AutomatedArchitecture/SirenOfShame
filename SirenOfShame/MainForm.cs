using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using SirenOfShame.Lib.Services;
using SirenOfShame.Resources2;
using log4net;
using SirenOfShame.Configuration;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.SirenConfiguration;

namespace SirenOfShame
{
    public partial class MainForm : FormBase
    {
        private const int TIMEOUT = 3000;
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(MainForm));
        SirenOfShameSettings _settings = SirenOfShameSettings.GetAppSettings();
        private RulesEngine _rulesEngine;
        private readonly string _logFilename;
        private readonly bool _canViewLogs;
        readonly SosDb _sosDb = new SosDb();
        readonly Timer _showAlertAnimation = new Timer();

        [Import(typeof(ISirenOfShameDevice))]
        public ISirenOfShameDevice SirenOfShameDevice { get; set; }

        public MainForm()
        {
            _log.Info("MainForm Open");
            IocContainer.Instance.Compose(this);
            InitializeComponent();

            _showAlertAnimation.Interval = 1;
            _showAlertAnimation.Tick += ShowAlertAnimationTick;
            viewUser1.OnClose += ViewUserOnClose;
            viewUser1.OnUserChangedAvatarId += ViewUser1OnOnUserChangedAvatarId;
            _buildStats.OnClose += BuildStatsOnClose;
            viewUser1.Initilaize(_settings);
            SirenOfShameDevice.Connected += SirenofShameDeviceConnected;
            SirenOfShameDevice.Disconnected += SirenofShameDeviceDisconnected;
            _userList.OnUserSelected += UsersListOnOnUserSelected;
            _userList.OnUserDisplayNameChanged += UsersListOnOnUserDisplayNameChanged;
            _userList.Settings = _settings;
            _newsFeed1.OnUserClicked += NewsFeedOnOnUserClicked;
            
            if (SirenOfShameDevice.IsConnected)
            {
                SirenofShameDeviceConnected(this, new EventArgs());
            }
            else
            {
                SirenofShameDeviceDisconnected(this, new EventArgs());
            }

            SetAutomaticUpdaterSettings();

            try
            {
                _logFilename = MyLogManager.GetLogFilename();
                _viewLog.Enabled = true;
                _canViewLogs = true;
            }
            catch (Exception)
            {
                _viewLog.Enabled = false;
                _canViewLogs = false;
            }
        }

        private void ViewUser1OnOnUserChangedAvatarId(object sender, UserChangedAvatarIdArgs args)
        {
            _newsFeed1.ChangeUserAvatarId(args.RawName, args.NewImageIndex);
        }

        private void NewsFeedOnOnUserClicked(object sender, UserClickedArgs args)
        {
            ShowViewUserPage(args.RawUserName);
        }

        private void UsersListOnOnUserDisplayNameChanged(object sender, UserDisplayNameChangedArgs args)
        {
            _lastRefreshStatusEventArgs.RefreshDisplayNames(_settings);
            _buildDefinitions.RefreshListViewWithBuildStatus(_lastRefreshStatusEventArgs);
        }

        private void UsersListOnOnUserSelected(object sender, UserSelectedArgs args)
        {
            var rawName = args.RawName;
            ShowViewUserPage(rawName);
        }

        private void ShowViewUserPage(string rawName)
        {
            var aUserIsSelected = rawName != null;
            _buildDefinitions.Visible = !aUserIsSelected;
            viewUser1.Visible = aUserIsSelected;
            if (aUserIsSelected)
            {
                var selectedPerson = _settings.People.First(i => i.RawName == rawName);
                viewUser1.SetUser(selectedPerson);
            }
        }

        private void BuildStatsOnClose(object sender, CloseBuildStatsArgs args)
        {
            _buildDefinitions.SelectedItems.Clear();
        }

        private void SetAutomaticUpdaterSettings()
        {
            if (_settings == null) return;
            string updatePath = "http://blueink.biz/SoS/updates/";
            if (_settings.UpdateLocation == UpdateLocation.Other)
            {
                updatePath = _settings.UpdateLocationOther;
            }
            string server = updatePath + "wyserver.zip";
            _automaticUpdater.wyUpdateCommandline = " \"-server=" + server + "\" \"-updatepath=" + updatePath + "\"";
            _automaticUpdater.Enabled = _settings.UpdateLocation != UpdateLocation.Never;
            _automaticUpdater.Visible = _settings.UpdateLocation != UpdateLocation.Never;
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

        public static ListViewItem AsListViewItem(BuildStatusListViewItem buildStatusListViewItem)
        {
            var listViewItem = new ListViewItem(buildStatusListViewItem.Name)
                                   {
                                       ImageIndex = buildStatusListViewItem.ImageIndex
                                   };

            AddSubItem(listViewItem, "ID", buildStatusListViewItem.BuildId);
            AddSubItem(listViewItem, "StartTime", buildStatusListViewItem.StartTime);
            AddSubItem(listViewItem, "Duration", buildStatusListViewItem.Duration);
            AddSubItem(listViewItem, "RequestedBy", buildStatusListViewItem.RequestedByDisplayName);
            AddSubItem(listViewItem, "Comment", buildStatusListViewItem.Comment);
            listViewItem.Tag = buildStatusListViewItem.Id;
            return listViewItem;
        }

        private static void AddSubItem(ListViewItem lvi, string name, string value)
        {
            var subItem = new ListViewItem.ListViewSubItem(lvi, value)
            {
                Name = name
            };
            lvi.SubItems.Add(subItem);
        }

        private void RulesEngineStatsChanged(object sender, StatsChangedEventArgs args)
        {
            Invoke(() => RefreshStats(args.ChangedBuildStatuses));
        }

        private void RefreshStats(IList<BuildStatus> changedBuildStatuses)
        {
            BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();
            RefreshStats(buildDefinitionSetting, changedBuildStatuses);
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
                _buildDefinitions.RefreshListViewWithBuildStatus(args);
                if (InFullscreenMode)
                {
                    _fullScreenBuildStatus.RefreshListViewWithBuildStatus(args, _settings);
                }
            });
        }

        private bool InFullscreenMode
        {
            get { return _fullScreenBuildStatus != null; }
        }

        private void SirenofShameDeviceConnected(object sender, EventArgs e)
        {
            bool firstTimeSirenHasEverBeenConnected = !_settings.SirenEverConnected;
            if (firstTimeSirenHasEverBeenConnected)
            {
                _settings.SirenEverConnected = true;
                _settings.ResetSirenSettings();
                _settings.Save();
            }

            EnableSirenMenuItem(true);
        }

        private void EnableSirenMenuItem(bool enable)
        {
            Invoke(() =>
            {
                _testSiren.Enabled = enable;
                if (enable)
                {
                    _configureSiren.Enabled = SirenOfShameDevice.HardwareType == HardwareType.Pro;
                }
                else
                {
                    _configureSiren.Enabled = false;
                }
            });
        }

        private void SirenofShameDeviceDisconnected(object sender, EventArgs args)
        {
            EnableSirenMenuItem(false);
        }

        SosOnlineService _sosOnlineService = new SosOnlineService();

        private void MainFormLoad(object sender, EventArgs e)
        {
            _panelAlertHeight = _panelAlert.Height;
            _log.Debug("Form1 loaded");
            if (_settings == null)
            {
                _settings = new SirenOfShameSettings();
            }

            TryUpgrade();

            SetRightMenu(RightMenu.NewsFeed);


            if (_settings.TryToFindOldAchievementsAtNextOpportunity)
            {
                FindOldAchievements.TryFindOldAchievements(_settings);
                _settings.TryToFindOldAchievementsAtNextOpportunity = false;
            }

            StartWatchingBuild();
            _sosOnlineService.OnNewSosOnlineNotification += SosOnlineServiceOnOnNewSosOnlineNotification;
            _sosOnlineService.StartRealtimeConnection(_settings);
            
            RefreshStats(null);
            SetMuteButton();
            _buildStats.InitializeBuildHistoryChart();
            _buildDefinitions.SetSortColumn(_settings);
        }

        private void SosOnlineServiceOnOnNewSosOnlineNotification(object sender, NewSosOnlineNotificationArgs args)
        {
            Invoke(() =>
            {
                NewNewsItemEventArgs newNewsItemEventArgs = new NewNewsItemEventArgs
                {
                    EventDate = DateTime.Now,
                    Person = args.GetSosOnlinePerson(),
                    Title = "\"" + args.Message + "\""
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
            RulesEngine.Start();
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
            return rulesEngine;
        }

        private void RulesEngineNewNewsItem(object sender, NewNewsItemEventArgs args)
        {
            Invoke(() => _newsFeed1.AddNewsItem(args));
        }

        private void RulesEngineNewAchievement(object sender, NewAchievementEventArgs args)
        {
            foreach (var achievement in args.Achievements)
            {
                _log.Debug(args.Person + " achieved " + achievement.Name);
            }
            if (_settings.AchievementAlertPreference == AchievementAlertPreferenceEnum.Never) return;
            if (_settings.AchievementAlertPreference == AchievementAlertPreferenceEnum.OnlyForMe && !_settings.IsMeOrDefault(args.Person, true)) return;
            Invoke(() =>
            {
                foreach (var achievement in args.Achievements)
                {
                    NewAchievement.ShowForm(_settings, achievement, args.Person, this, modal: false);
                }
                _userList.SelectUser(args.Person);
            });
        }

        private void RulesEnginePlayWindowsAudio(object sender, PlayWindowsAudioEventArgs args)
        {
            // note: not doing an invoke because so far this code doesn't require the UI thread
            System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            Stream s = a.GetManifestResourceStream(args.Location);
            SoundPlayer player = new SoundPlayer(s);
            player.Play();
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
            else
            {
                // ToDo: Show() on MainFormMove was causing an infinite loop on startup. What was it for? Permanently remove if this was necessary LR.
                Show(); 
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
            configureRules.ShowDialog();
        }

        private void SosOnlineClick(object sender, EventArgs e)
        {
            var configureSosOnline = new ConfigureSosOnline(_settings);
            configureSosOnline.ShowDialog();
        }

        private void ConfigureServersClick(object sender, EventArgs e)
        {
            bool anyChanges = ConfigureServers.Show(_settings);
            if (anyChanges)
            {
                StopWatchingBuild();
                _rulesEngine = null; // reset the rules engine in case it changed (e.g. from TFS to Team City)
                StartWatchingBuild();
            }
            Activate();
            Focus();
        }

        private void TestSirenClick(object sender, EventArgs e)
        {
            var testSiren = new TestSiren();
            testSiren.ShowDialog();
        }

        private void ConfigureSirenClick(object sender, EventArgs e)
        {
            var configureSiren = new ConfigureSirenDialog(_settings, SirenOfShameDevice);
            configureSiren.ShowDialog(this);
        }

        private void BuildDefinitionsDoubleClick(object sender, EventArgs e)
        {
            var listViewItem = _buildDefinitions.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (listViewItem == null) return;
            string buildId = (string)listViewItem.Tag;
            BuildStatusListViewItem buildStatusListViewItem = _lastRefreshStatusEventArgs.BuildStatusListViewItems.First(i => i.Id == buildId);
            var url = buildStatusListViewItem.Url;
            if (!string.IsNullOrWhiteSpace(url) && url.StartsWith("http"))
            {
                Process.Start(url);
            }
        }

        private void BuildDefinitionsMouseUp(object sender, MouseEventArgs e)
        {
            BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();

            if (e.Button == MouseButtons.Right)
            {
                _buildMenu.Show(_buildDefinitions, e.X, e.Y);
                _affectsTrayIcon.Checked = buildDefinitionSetting == null || buildDefinitionSetting.AffectsTrayIcon;
            }
        }

        private enum RightMenu
        {
            Users = 0,
            BuildStats = 1,
            NewsFeed = 2
        }

        private void RefreshStats(BuildDefinitionSetting buildDefinitionSetting, IList<BuildStatus> changedBuildStatuses)
        {
            bool buildDefinitionSelected = buildDefinitionSetting != null;
            RightMenu rightMenu = buildDefinitionSelected ? RightMenu.BuildStats : RightMenu.Users;
            //SetRightMenu(rightMenu);
            _panelRight.Visible = _settings.People.Any() && (buildDefinitionSelected || !_settings.HideReputation);
            if (_panelRight.Visible)
            {
                if (!buildDefinitionSelected && !_settings.HideReputation)
                {
                    RefreshUserStats(changedBuildStatuses);
                } else
                {
                    RefreshProjectStats(buildDefinitionSetting);
                }
            }
        }
        
        const string NEWS_SELECTED_PNG = "NewsSelected.png";
        const string PERSON_SELECTED_PNG = "PersonSelected.png";

        private void SetRightMenu(RightMenu rightMenu)
        {
            _buildStats.Visible = rightMenu == RightMenu.BuildStats;
            _userList.Visible = rightMenu == RightMenu.Users;
            _newsFeed1.Visible = rightMenu == RightMenu.NewsFeed;
            ResetRightMenuButtons();
        }

        private void ResetRightMenuButtons()
        {
            _usersButton.ImageKey = _userList.Visible ? PERSON_SELECTED_PNG : "PersonDeselected.png";
            _newsButton.ImageKey = _newsFeed1.Visible ? NEWS_SELECTED_PNG : "NewsDeselected.png";
        }

        private void RefreshProjectStats(BuildDefinitionSetting buildDefinitionSetting)
        {
            var definitions = _sosDb.ReadAll(buildDefinitionSetting);

            _buildStats.GraphBuildHistory(definitions);

            var count = definitions.Count;
            var failed = definitions.Count(s => s.BuildStatusEnum == BuildStatusEnum.Broken);
            double percentFailed = count == 0 ? 0 : ((double) failed)/count;
            _buildStats.SetStats(count, failed, percentFailed);
        }

        private void RefreshUserStats(IList<BuildStatus> changedBuildStatuses)
        {
            _userList.RefreshUserStats(changedBuildStatuses);
        }

        private BuildDefinitionSetting GetActiveBuildDefinitionSetting()
        {
            var listViewItem = _buildDefinitions.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (listViewItem == null) return null;

            string buildId = (string)listViewItem.Tag;

            var buildDefinitionSetting = _settings.CiEntryPointSettings.SelectMany(i => i.BuildDefinitionSettings).FirstOrDefault(bds => bds.Id == buildId);
            if (buildDefinitionSetting == null)
            {
                _log.Error("Could not find a build definition settings for id " + buildId);
                return null;
            }
            return buildDefinitionSetting;
        }

        private void AffectsTrayIconClick(object sender, EventArgs e)
        {
            BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();
            if (buildDefinitionSetting == null) return;

            buildDefinitionSetting.AffectsTrayIcon = !buildDefinitionSetting.AffectsTrayIcon;
            _settings.Save();
        }

        private static void AddToolStripItems(ToolStripItemCollection items, IEnumerable<ToolStripMenuItem> toolStripItems)
        {
            foreach (ToolStripMenuItem toolStripItem in toolStripItems)
            {
                items.Add(toolStripItem);
            }
        }
        
        private void BuildMenuOpening(object sender, CancelEventArgs e)
        {
            BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();

            IEnumerable<ToolStripMenuItem> toolStripItems = Rule.TriggerTypes.Select(i => WhenMenu(i, buildDefinitionSetting, person: null));
            _when.DropDownItems.Clear();
            AddToolStripItems(_when.DropDownItems, toolStripItems);

            _buildMenu.Items.Clear();
            if (buildDefinitionSetting != null)
            {
                _buildMenu.Items.Add(_affectsTrayIcon);
                _buildMenu.Items.Add(_stopWatching);
            }
            _buildMenu.Items.Add(_when);
            if (buildDefinitionSetting != null)
            {
                _buildMenu.Items.Add(_toolStripSeparator1);
                AddToolStripItems(_buildMenu.Items, buildDefinitionSetting.People.Select(p => PersonMenu(p, buildDefinitionSetting)).ToArray());
            }
        }

        private ToolStripMenuItem PersonMenu(string person, BuildDefinitionSetting buildDefinitionSetting)
        {
            var displayName = _settings.TryGetDisplayName(person);
            ToolStripMenuItem menu = new ToolStripMenuItem(displayName);
            var toolStripItems = Rule.TriggerTypes.Select(i => WhenMenu(i, buildDefinitionSetting, person));
            AddToolStripItems(menu.DropDownItems, toolStripItems);
            return menu;
        }

        private ToolStripMenuItem WhenMenu(KeyValuePair<TriggerType, string> triggerTypeDescription, BuildDefinitionSetting buildDefinitionSetting, string person)
        {
            var triggerType = triggerTypeDescription.Key;
            string buildDefinitionId = buildDefinitionSetting == null ? null : buildDefinitionSetting.Id;
            var rule = _settings.FindRule(triggerType, buildDefinitionId, person);

            var menuItem = new ToolStripMenuItem(triggerTypeDescription.Value);

            var trayAlertMenu = new ToolStripMenuItem("Display a quick tray alert")
            {
                Checked = rule != null && rule.AlertType == AlertType.TrayAlert,
                Tag = new RuleDropDownItemTag { AlertType = AlertType.TrayAlert, BuildDefinitionId = buildDefinitionId, TriggerPerson = person, TriggerType = triggerType }
            };
            trayAlertMenu.Click += RuleDropDownItemClick;
            var modalDialogMenu = new ToolStripMenuItem("Open a modal dialog")
            {
                Checked = rule != null && rule.AlertType == AlertType.ModalDialog,
                Tag = new RuleDropDownItemTag { AlertType = AlertType.ModalDialog, BuildDefinitionId = buildDefinitionId, TriggerPerson = person, TriggerType = triggerType }
            };
            modalDialogMenu.Click += RuleDropDownItemClick;

            menuItem.DropDownItems.Add(trayAlertMenu);
            menuItem.DropDownItems.Add(modalDialogMenu);

            bool isConnected = SirenOfShameDevice.IsConnected;

            var playWindowsAudioMenu = new ToolStripMenuItem("Play the following audio in Windows");
            var playAudioMenu = new ToolStripMenuItem("Play the following audio on the device") { Enabled = isConnected };
            var playLightsMenu = new ToolStripMenuItem("Turn on the following light pattern") { Enabled = isConnected };

            if (isConnected)
            {
                AddToolStripItems(playAudioMenu.DropDownItems, SirenOfShameDevice.AudioPatterns.Select(ap => AudioPatternMenu(ap, rule, buildDefinitionId, triggerType, person)).ToArray());
                playAudioMenu.Checked = playAudioMenu.DropDownItems.Cast<ToolStripMenuItem>().Any(d => d.Checked);

                AddToolStripItems(playLightsMenu.DropDownItems, SirenOfShameDevice.LedPatterns.Select(ap => LedPatternMenu(ap, rule, buildDefinitionId, triggerType, person)).ToArray());
                playLightsMenu.Checked = playLightsMenu.DropDownItems.Cast<ToolStripMenuItem>().Any(d => d.Checked);
            }

            AddToolStripItems(playWindowsAudioMenu.DropDownItems, ResourceManager.InternalAudioFiles.Select(af => WindowsAudioPatternMenu(af, rule, buildDefinitionId, triggerType, person)).ToArray());
            playWindowsAudioMenu.Checked = playWindowsAudioMenu.DropDownItems.Cast<ToolStripMenuItem>().Any(d => d.Checked);

            menuItem.DropDownItems.Add(playWindowsAudioMenu);
            menuItem.DropDownItems.Add(playAudioMenu);
            menuItem.DropDownItems.Add(playLightsMenu);

            menuItem.Checked = menuItem.DropDownItems.Cast<ToolStripMenuItem>().Any(d => d.Checked);

            return menuItem;
        }

        private readonly IEnumerable<KeyValuePair<int?, string>> _durations = new List<KeyValuePair<int?, string>>
        {
            new KeyValuePair<int?, string>(1, "1 Second"),
            new KeyValuePair<int?, string>(5, "5 Seconds"),
            new KeyValuePair<int?, string>(10, "10 Seconds"),
            new KeyValuePair<int?, string>(30, "30 Seconds"),
            new KeyValuePair<int?, string>(60, "60 Seconds"),
            new KeyValuePair<int?, string>(null, "Until the build Passes"),
        };

        private ToolStripMenuItem WindowsAudioPatternMenu(AudioFile af, Rule rule, string buildDefinitionId, TriggerType triggerType, string person)
        {
            bool patternIsMatch = false;
            if (rule != null && !string.IsNullOrEmpty(rule.WindowsAudioLocation))
                patternIsMatch = af.Location == rule.WindowsAudioLocation;
            var menu = new ToolStripMenuItem(af.DisplayName)
            {
                Checked = patternIsMatch,
                Tag = new RuleDropDownItemTag
                {
                    AlertType = null,
                    BuildDefinitionId = buildDefinitionId,
                    TriggerPerson = person,
                    TriggerType = triggerType,
                    LedPattern = null,
                    WindowsAudioLocation = af.Location,
                    AudioPattern = null,
                    Duration = null
                }
            };
            menu.Click += RuleDropDownItemClick;
            return menu;
        }

        private ToolStripMenuItem AudioPatternMenu(AudioPattern ap, Rule rule, string buildDefinitionId, TriggerType triggerType, string person)
        {
            bool patternIsMatch = false;
            if (rule != null && !rule.InheritAudioSettings && rule.AudioPattern != null)
                patternIsMatch = rule.AudioPattern.Equals(ap);
            var menu = new ToolStripMenuItem(ap.Name)
            {
                Checked = patternIsMatch
            };
            AddToolStripItems(menu.DropDownItems, GetDurations(rule, null, ap, patternIsMatch, buildDefinitionId, triggerType, person));
            return menu;
        }

        private ToolStripMenuItem LedPatternMenu(LedPattern lp, Rule rule, string buildDefinitionId, TriggerType triggerType, string person)
        {
            bool patternIsMatch = false;
            if (rule != null && !rule.InheritLedSettings && rule.LedPattern != null)
                patternIsMatch = rule.LedPattern.Equals(lp);
            var menu = new ToolStripMenuItem(lp.Name)
            {
                Checked = patternIsMatch
            };
            AddToolStripItems(menu.DropDownItems, GetDurations(rule, lp, null, patternIsMatch, buildDefinitionId, triggerType, person));
            return menu;
        }

        private IEnumerable<ToolStripMenuItem> GetDurations(Rule rule, LedPattern ledPattern, AudioPattern audioPattern, bool patternIsMatch, string buildDefinitionId, TriggerType triggerType, string person)
        {
            int? duration = null;
            if (rule != null)
                duration = ledPattern == null ? rule.AudioDuration : rule.LightsDuration;

            var durations = _durations.Select(d => new ToolStripMenuItem(d.Value)
            {
                Checked = patternIsMatch && duration == d.Key,
                Tag = new RuleDropDownItemTag
                {
                    AlertType = null,
                    BuildDefinitionId = buildDefinitionId,
                    TriggerPerson = person,
                    TriggerType = triggerType,
                    LedPattern = ledPattern,
                    AudioPattern = audioPattern,
                    Duration = d.Key
                }
            }).ToArray();
            foreach (var toolStripMenuItem in durations)
            {
                toolStripMenuItem.Click += RuleDropDownItemClick;
            }
            return durations;
        }

        private void RuleDropDownItemClick(object sender, EventArgs e)
        {
            var toolStripSender = (ToolStripItem)sender;
            RuleDropDownItemTag tag = (RuleDropDownItemTag)toolStripSender.Tag;

            if (tag == null)
            {
                _log.Error("User clicked '" + toolStripSender.Text + "' but it had no tag");
                return;
            }

            var rule = _settings.Rules.FirstOrDefault(r =>
                r.TriggerType == tag.TriggerType &&
                r.BuildDefinitionId == tag.BuildDefinitionId &&
                r.TriggerPerson == tag.TriggerPerson
                );

            // find/add
            if (rule == null)
            {
                // base new rules on any generic high level rules
                var baseRule = _settings.Rules.FirstOrDefault(r =>
                                                          r.TriggerType == tag.TriggerType &&
                                                          r.BuildDefinitionId == null && r.TriggerPerson == null);

                rule = new Rule
                {
                    TriggerType = tag.TriggerType,
                    BuildDefinitionId = tag.BuildDefinitionId,
                    TriggerPerson = tag.TriggerPerson,
                };

                if (baseRule != null)
                {
                    rule.InheritAudioSettings = baseRule.InheritAudioSettings;
                    rule.InheritLedSettings = baseRule.InheritLedSettings;
                    rule.AlertType = baseRule.AlertType;
                    rule.LedPattern = baseRule.LedPattern;
                    rule.LightsDuration = baseRule.LightsDuration;
                    rule.WindowsAudioLocation = baseRule.WindowsAudioLocation;
                    rule.AudioPattern = baseRule.AudioPattern;
                    rule.AudioDuration = baseRule.AudioDuration;
                }

                _settings.Rules.Add(rule);
            }

            if (tag.AlertType != null)
            {
                bool uncheckedAlertType = rule.AlertType == tag.AlertType.Value;
                rule.AlertType = uncheckedAlertType ? AlertType.NoAlert : tag.AlertType.Value;
            }

            if (!string.IsNullOrEmpty(tag.WindowsAudioLocation))
            {
                bool uncheckWindowsAudio = tag.WindowsAudioLocation == rule.WindowsAudioLocation;
                rule.WindowsAudioLocation = uncheckWindowsAudio ? null : tag.WindowsAudioLocation;
            }
            
            if (tag.AudioPattern != null)
            {
                bool uncheckAudioPattern = tag.AudioPattern.Equals(rule.AudioPattern) && tag.Duration == rule.AudioDuration;
                rule.AudioPattern = uncheckAudioPattern ? null : tag.AudioPattern;
                rule.AudioDuration = tag.Duration;
                rule.InheritAudioSettings = rule.AudioPattern == null;
            }

            if (tag.LedPattern != null)
            {
                bool uncheckLedPattern = tag.LedPattern.Equals(rule.LedPattern) && tag.Duration == rule.LightsDuration;
                rule.LedPattern = uncheckLedPattern ? null : tag.LedPattern;
                rule.LightsDuration = tag.Duration;
                rule.InheritLedSettings = rule.LedPattern == null;
            }

            _settings.Save();
        }

        private void StopWatchingClick(object sender, EventArgs e)
        {
            BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();
            if (buildDefinitionSetting == null) return;
            buildDefinitionSetting.Active = false;
            _settings.Save();
            var listViewItem = _buildDefinitions.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (listViewItem != null) listViewItem.Remove();
        }

        private void OpenSettingsClick(object sender, EventArgs e)
        {
            Settings settings = new Settings(_settings);
            settings.ShowDialog();
            RefreshStats(null); // just in case they clicked reset reputation
            SetAutomaticUpdaterSettings(); // just in case they changed the updater settings
        }

        private void TimeboxEnforcerClick(object sender, EventArgs e)
        {
            TimeboxEnforcer timeboxEnforcer = new TimeboxEnforcer();
            timeboxEnforcer.Show();
        }

        private void HelpClick(object sender, EventArgs e)
        {
            HelpAbout helpAbout = new HelpAbout();
            helpAbout.ShowDialog();
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

        private void RefreshClick(object sender, EventArgs e)
        {
            RulesEngine.RefreshAll();
        }

        private void SirenUpgradeFirmwareClick(object sender, EventArgs e)
        {
            SirenFirmwareUpgrade upgrade = new SirenFirmwareUpgrade();
            upgrade.ShowDialog(this);
        }

        private void BuildDefinitionsSelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshStats(null);
        }

        FullScreenBuildStatus _fullScreenBuildStatus = null;

        private void FullscreenClick(object sender, EventArgs e)
        {
            if (_fullScreenBuildStatus == null)
            {
                _fullScreenBuildStatus = new FullScreenBuildStatus();
                _fullScreenBuildStatus.FormClosed += FullScreenBuildStatusFormClosed;
            }
            _fullScreenBuildStatus.Show();
            if (_lastRefreshStatusEventArgs != null)
                _fullScreenBuildStatus.RefreshListViewWithBuildStatus(_lastRefreshStatusEventArgs, _settings);
        }

        private void FullScreenBuildStatusFormClosed(object sender, FormClosedEventArgs e)
        {
            _fullScreenBuildStatus = null;
        }

        private void MuteClick(object sender, EventArgs e)
        {
            _settings.Mute = !_settings.Mute;
            _settings.Save();
            SetMuteButton();
        }

        private void SetMuteButton()
        {
            _mute.ImageIndex = _settings.Mute ? 5 : 6;
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

        private void BuildDefinitionsColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (_settings.SortColumn == e.Column)
            {
                _settings.SortDescending = !_settings.SortDescending;
            } 
            else
            {
                _settings.SortDescending = false;
            }
            _settings.SortColumn = e.Column;
            _settings.Save();
            _buildDefinitions.SetSortColumn(_settings);
        }

        private void ViewUserOnClose(object sender, CloseViewUserArgs args)
        {
            _userList.DeselectAllUsers();
        }

        private void ToolStripSplitErrorButtonClick(object sender, EventArgs e)
        {
            var exception = (Exception) _toolStripSplitErrorButton.Tag;
            ExceptionMessageBox.Show(this, "Connection Error", exception.Message, exception);
        }

        private void NewsButtonClick(object sender, EventArgs e)
        {
            SetRightMenu(RightMenu.NewsFeed);
        }

        private void UsersButtonClick(object sender, EventArgs e)
        {
            SetRightMenu(RightMenu.Users);
        }

        private void RightPanelButtonsResize(object sender, EventArgs e)
        {
            var halfWidth = _rightPanelButtons.Width/2;
            _newsButton.Width = halfWidth;
            _usersButton.Width = halfWidth;
            _usersButton.Left = halfWidth;
        }

        private void NewsButtonMouseEnter(object sender, EventArgs e)
        {
            _newsButton.ImageKey = NEWS_SELECTED_PNG;
        }

        private void UsersButtonMouseEnter(object sender, EventArgs e)
        {
            _usersButton.ImageKey = PERSON_SELECTED_PNG;
        }

        private void NewsButtonMouseLeave(object sender, EventArgs e)
        {
            ResetRightMenuButtons();
        }

        private void UsersButtonMouseLeave(object sender, EventArgs e)
        {
            ResetRightMenuButtons();
        }
    }
}