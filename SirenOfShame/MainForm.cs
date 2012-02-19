using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using SirenOfShame.Resources2;
using ZedGraph;
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
        private static readonly ILog Log = MyLogManager.GetLogger(typeof(MainForm));
        SirenOfShameSettings _settings = SirenOfShameSettings.GetAppSettings();
        private RulesEngine _rulesEngine;
        private readonly string _logFilename;
        private readonly bool _canViewLogs;
        SosDb _sosDb = new SosDb();
        
        [Import(typeof(ISirenOfShameDevice))]
        public ISirenOfShameDevice SirenOfShameDevice { get; set; }

        public MainForm()
        {
            Log.Info("MainForm Open");
            IocContainer.Instance.Compose(this);
            InitializeComponent();

            fastAnimation.Interval = 1;
            fastAnimation.Tick += FastAnimationTick;

            SirenOfShameDevice.Connected += SirenofShameDeviceConnected;
            SirenOfShameDevice.Disconnected += SirenofShameDeviceDisconnected;
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

        private void SetAutomaticUpdaterSettings()
        {
            string updatePath;
            if (_settings.UpdateLocation == UpdateLocation.Other)
            {
                updatePath = _settings.UpdateLocationOther;
            }
            else
            {
                updatePath = "http://blueink.biz/SoS/updates/";
            }
            string server = updatePath + "wyserver.zip";
            _automaticUpdater.wyUpdateCommandline = " \"-server=" + server + "\" \"-updatepath=" + updatePath + "\"";
        }

        protected override void WndProc(ref Message m)
        {
            SirenOfShameDevice.WndProc(ref m);
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
            Invoke(RefreshStats);
        }

        private void RefreshStats()
        {
            BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();
            RefreshStats(buildDefinitionSetting);
        }

        /// <summary>
        /// For entering into full screen mode
        /// </summary>
        private RefreshStatusEventArgs lastRefreshStatusEventArgs = null;
        
        private void RulesEngineRefreshRefreshStatus(object sender, RefreshStatusEventArgs args)
        {
            Invoke(() =>
            {
                lastRefreshStatusEventArgs = args;
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

        private void Form1Load(object sender, EventArgs e)
        {
            _panelAlertHeight = _panelAlert.Height;
            Log.Debug("Form1 loaded");
            if (_settings == null)
            {
                _settings = new SirenOfShameSettings();
            }
            StartWatchingBuild();
            RefreshStats();
            SetMuteButton();
            InitializeBuildHistoryChart();
        }

        private void InitializeBuildHistoryChart()
        {
            GraphPane myPane = _buildHistoryZedGraph.GraphPane;
            myPane.Margin.All = 0;
            myPane.Legend.IsVisible = false;
            myPane.Title.IsVisible = false;
            myPane.XAxis.IsVisible = false;

            myPane.YAxis.IsVisible = true;
            myPane.YAxis.MinorTic.IsOpposite = false;
            myPane.YAxis.IsAxisSegmentVisible = true;
            myPane.YAxis.MinorTic.Color = Color.White;

            myPane.YAxis.MajorTic.IsCrossOutside = false;
            myPane.YAxis.MajorTic.IsCrossInside = false;
            myPane.YAxis.MajorTic.IsInside = false;
            myPane.YAxis.MajorTic.IsOutside = false;

            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.IsSkipFirstLabel = true;
            myPane.YAxis.Scale.IsSkipLastLabel = true;
            myPane.YAxis.MajorTic.IsOpposite = false;
            myPane.YAxis.Title.IsVisible = false;
            myPane.XAxis.Type = AxisType.Text;
            myPane.IsFontsScaled = false;
            myPane.YAxis.Scale.FontSpec.Size = 10;

            myPane.Chart.Border.IsVisible = false;
            myPane.Border.IsVisible = false;

            _buildHistoryZedGraph.IsEnableZoom = false;
            myPane.BarSettings.ClusterScaleWidth = 60;
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
            return rulesEngine;
        }

        private void RulesEnginePlayWindowsAudio(object sender, PlayWindowsAudioEventArgs args)
        {
            // note: not doing an invoke because so far this code doesn't require the UI thread
            System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream s = a.GetManifestResourceStream(args.Location);
            SoundPlayer player = new SoundPlayer(s);
            player.Play();
        }

        private bool _showAlert;
        private DateTime _alertDate;
        private string _alertUrl;
        
        private void RulesEngineNewAlert(object sender, NewAlertArgs args)
        {
            Invoke(() =>
            {
                _showAlert = true;
                _alertUrl = args.Url;
                _panelAlert.Visible = true;
                _panelAlert.Height = 1;
                _labelAlert.Text = args.Message;
                _details.Location = new Point(_labelAlert.Width + 7, _labelAlert.Location.Y);
                fastAnimation.Start();
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
            Invoke(() => _lastStatusUpdate.Text = args.StatusText);
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
            Log.Debug("ClosingForInstall: " + _automaticUpdater.ClosingForInstall);
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
                Log.Error("Could not determine if wyUpdate is closing us", ex);
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

        private void OpenConfigureRulesDialog()
        {
            var configureRules = new ConfigureRules(_settings);
            configureRules.ShowDialog();
        }

        private void ConfigureRulesClick(object sender, EventArgs e)
        {
            OpenConfigureRulesDialog();
        }

        private void ConfigureServersClick(object sender, EventArgs e)
        {
            StopWatchingBuild();
            ConfigureServers.Show(_settings);
            _rulesEngine = null; // reset the rules engine in case it changed (e.g. from TFS to Team City)
            StartWatchingBuild();
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
            OpenConfigureRulesDialog();
        }

        private void BuildDefinitionsMouseUp(object sender, MouseEventArgs e)
        {
            BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();

            if (e.Button == MouseButtons.Right)
            {
                _buildMenu.Show(_buildDefinitions, e.X, e.Y);
                _affectsTrayIcon.Checked = buildDefinitionSetting == null ? true : buildDefinitionSetting.AffectsTrayIcon;
            }
        }

        private void RefreshStats(BuildDefinitionSetting buildDefinitionSetting)
        {
            bool buildDefinitionSelected = buildDefinitionSetting != null;
            _panelBuildStats.Visible = buildDefinitionSelected;
            _userStats.Visible = !buildDefinitionSelected;
            _panelRight.Visible = _settings.People.Any() && (buildDefinitionSelected || !_settings.HideReputation);
            if (_panelRight.Visible)
            {
                if (!buildDefinitionSelected && !_settings.HideReputation)
                {
                    RefreshUserStats();
                } else
                {
                    RefreshProjectStats(buildDefinitionSetting);
                }
            }
        }

        private void RefreshProjectStats(BuildDefinitionSetting buildDefinitionSetting)
        {
            var definitions = _sosDb.ReadAll(buildDefinitionSetting);

            GraphBuildHistory(definitions);

            var count = definitions.Count;
            var failed = definitions.Where(s => s.BuildStatusEnum == BuildStatusEnum.Broken).Count();
            double percentFailed = count == 0 ? 0 : ((double) failed)/count;
            SetStats(count, failed, percentFailed);
        }

        private void GraphBuildHistory(List<BuildStatus> buildStatuses)
        {
            GraphPane myPane = _buildHistoryZedGraph.GraphPane;
            myPane.CurveList.Clear();

            IEnumerable<BuildStatus> lastFiveBuildStatuses = buildStatuses.Skip(buildStatuses.Count - 8);
            foreach (BuildStatus buildStatus in lastFiveBuildStatuses)
            {
                if (buildStatus.FinishedTime == null || buildStatus.StartedTime == null) continue;
                var duration = buildStatus.FinishedTime.Value - buildStatus.StartedTime.Value;
                Fill fill = buildStatus.BuildStatusEnum == BuildStatusEnum.Broken ? failFill: successFill;
                var bar = myPane.AddBar(null, null, new [] { duration.TotalMinutes }, Color.White);
                bar.Bar.Fill = fill;
                bar.Bar.Border.Color = Color.White;
            }

            _buildHistoryZedGraph.AxisChange();
            _buildHistoryZedGraph.Invalidate();
        }

        Fill failFill = new Fill(Color.FromArgb(192, 80, 77));
        Fill successFill = new Fill(Color.FromArgb(79, 129, 189));

        private void RefreshUserStats()
        {
            _users.Items.Clear();
            var filteredPeople = _showAllPeople ? _settings.People : _settings.VisiblePeople;
            var personSettings = filteredPeople
                .Select(i => new {i.RawName, i.DisplayName, Reputation = i.GetReputation()})
                .OrderByDescending(i => i.Reputation);
            foreach (var person in personSettings)
            {
                ListViewItem lvi = new ListViewItem(person.DisplayName);
                AddSubItem(lvi, "Reputation", person.Reputation.ToString());
                lvi.Tag = person.RawName;
                _users.Items.Add(lvi);
            }
        }

        private void ClearStats()
        {
            SetStats(0, 0, 0);
        }

        private void SetStats(int count, int failed, double percentFailed)
        {
            _buildCount.Text = count.ToString();
            _failedBuilds.Text = failed.ToString();
            _percentFailed.Text = percentFailed.ToString("p");
        }

        private PersonSetting GetActivePerson()
        {
            var listViewItem = _users.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (listViewItem == null) return null;

            var rawName = (string)listViewItem.Tag;
            return _settings.People.FirstOrDefault(u => u.RawName == rawName);
        }
        
        private BuildDefinitionSetting GetActiveBuildDefinitionSetting()
        {
            var listViewItem = _buildDefinitions.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (listViewItem == null) return null;

            string buildId = (string)listViewItem.Tag;

            var buildDefinitionSetting = _settings.CiEntryPointSettings.SelectMany(i => i.BuildDefinitionSettings).FirstOrDefault(bds => bds.Id == buildId);
            if (buildDefinitionSetting == null)
            {
                Log.Error("Could not find a build definition settings for id " + buildId);
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

        private void BuildMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();

            var toolStripItems = Rule.TriggerTypes.Select(i => WhenMenu(i, buildDefinitionSetting, person: null));
            _when.DropDownItems.Clear();
            _when.DropDownItems.AddRange(toolStripItems.ToArray());

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
                _buildMenu.Items.AddRange(buildDefinitionSetting.People.Select(p => PersonMenu(p, buildDefinitionSetting)).ToArray());
            }
        }

        private ToolStripMenuItem PersonMenu(string person, BuildDefinitionSetting buildDefinitionSetting)
        {
            var displayName = _settings.TryGetDisplayName(person);
            ToolStripMenuItem menu = new ToolStripMenuItem(displayName);
            var toolStripItems = Rule.TriggerTypes.Select(i => WhenMenu(i, buildDefinitionSetting, person));
            menu.DropDownItems.AddRange(toolStripItems.ToArray());
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
                Checked = rule == null ? false : rule.AlertType == AlertType.TrayAlert,
                Tag = new RuleDropDownItemTag { AlertType = AlertType.TrayAlert, BuildDefinitionId = buildDefinitionId, TriggerPerson = person, TriggerType = triggerType }
            };
            trayAlertMenu.Click += RuleDropDownItemClick;
            var modalDialogMenu = new ToolStripMenuItem("Open a modal dialog")
            {
                Checked = rule == null ? false : rule.AlertType == AlertType.ModalDialog,
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
                playAudioMenu.DropDownItems.AddRange(SirenOfShameDevice.AudioPatterns.Select(ap => AudioPatternMenu(ap, rule, buildDefinitionId, triggerType, person)).ToArray());
                playAudioMenu.Checked = playAudioMenu.DropDownItems.Cast<ToolStripMenuItem>().Any(d => d.Checked);

                playLightsMenu.DropDownItems.AddRange(SirenOfShameDevice.LedPatterns.Select(ap => LedPatternMenu(ap, rule, buildDefinitionId, triggerType, person)).ToArray());
                playLightsMenu.Checked = playLightsMenu.DropDownItems.Cast<ToolStripMenuItem>().Any(d => d.Checked);
            }

            playWindowsAudioMenu.DropDownItems.AddRange(ResourceManager.InternalAudioFiles.Select(af => WindowsAudioPatternMenu(af, rule, buildDefinitionId, triggerType, person)).ToArray());
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
            menu.DropDownItems.AddRange(GetDurations(rule, null, ap, patternIsMatch, buildDefinitionId, triggerType, person));
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
            menu.DropDownItems.AddRange(GetDurations(rule, lp, null, patternIsMatch, buildDefinitionId, triggerType, person));
            return menu;
        }

        private ToolStripMenuItem[] GetDurations(Rule rule, LedPattern ledPattern, AudioPattern audioPattern, bool patternIsMatch, string buildDefinitionId, TriggerType triggerType, string person)
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
                Log.Error("User clicked '" + toolStripSender.Text + "' but it had no tag");
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
            RefreshStats(); // just in case they clicked reset reputation
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

        private void _configurationMore_Click(object sender, EventArgs e)
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

        private void _checkForUpdates_Click(object sender, EventArgs e)
        {
            CheckForUpdates();
        }

        public void CheckForUpdates()
        {
            SetAutomaticUpdaterSettings();
            _automaticUpdater.ForceCheckForUpdate(true);
        }

        private void _viewLog_Click(object sender, EventArgs e)
        {
            ViewLogs();
        }

        public void ViewLogs()
        {
            Process.Start(_logFilename);
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

        private void _buildDefinitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshStats();
        }

        private void UsersMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            ListViewItem lvi = GetSelectedUser();
            var anyUserSelected = lvi != null;
            _editUserName.Visible = anyUserSelected;
            _hideUser.Visible = anyUserSelected;
            if (anyUserSelected)
            {
                var person = GetActivePerson();
                _hideUser.Text = person.Hidden ? "Show" : "Hide";
            }
            _userMenu.Show(_users, e.X, e.Y);
        }

        private ListViewItem GetSelectedUser()
        {
            return _users.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
        }

        private void UsersAfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            var activePerson = GetActivePerson();
            if (activePerson == null) return;
            activePerson.DisplayName = e.Label;
            _settings.Save();

            lastRefreshStatusEventArgs.RefreshDisplayNames(_settings);
            _buildDefinitions.RefreshListViewWithBuildStatus(lastRefreshStatusEventArgs);
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
            if (lastRefreshStatusEventArgs != null)
                _fullScreenBuildStatus.RefreshListViewWithBuildStatus(lastRefreshStatusEventArgs, _settings);
        }

        private void FullScreenBuildStatusFormClosed(object sender, FormClosedEventArgs e)
        {
            _fullScreenBuildStatus = null;
        }

        private void EditUserNameClick(object sender, EventArgs e)
        {
            ListViewItem lvi = GetSelectedUser();
            if (lvi != null)
                lvi.BeginEdit();
        }

        private void HideUserClick(object sender, EventArgs e)
        {
            var activePerson = GetActivePerson();
            if (activePerson == null) return;
            activePerson.Hidden = !activePerson.Hidden;
            _settings.Save();
            RefreshUserStats();
        }

        private bool _showAllPeople = false;
        
        private void ShowAllUsersCheckedChanged(object sender, EventArgs e)
        {
            _showAllPeople = _showAllUsers.Checked;
            RefreshUserStats();
        }

        private void _mute_Click(object sender, EventArgs e)
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

        Timer fastAnimation = new Timer();
        
        private void _closeAlert_Click(object sender, EventArgs e)
        {
            _showAlert = false;
            fastAnimation.Start();
            _settings.AlertClosed = _alertDate;
            _settings.Save();
        }

        int _panelAlertHeight;

        private void FastAnimationTick(object sender, EventArgs e)
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
                    fastAnimation.Stop();
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
            } else
            {
                _settings.SortDescending = false;
            }
            _settings.SortColumn = e.Column;
            _settings.Save();
            SortOrder sortOrder = _settings.SortDescending ? SortOrder.Descending : SortOrder.Ascending;
            _buildDefinitions.SetSortColumn(e.Column, sortOrder);
        }
    }
}