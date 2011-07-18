using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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

        [Import(typeof(ISirenOfShameDevice))]
        public ISirenOfShameDevice SirenOfShameDevice { get; set; }

        public MainForm()
        {
            Log.Info("MainForm Open");
            IocContainer.Instance.Compose(this);
            InitializeComponent();

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
                if (!updatePath.EndsWith("/") || !updatePath.EndsWith("\\"))
                {
                    _settings.UpdateLocationOther += "/";
                }
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
            Invoke(() => SosMessageBox.Show("Siren of Shame", args.DialogText, "Ok"));
        }

        public static ListViewItem AsListViewItem(BuildStatusListViewItem buildStatusListViewItem)
        {
            var listViewItem = new ListViewItem(buildStatusListViewItem.Name)
                                   {
                                       ImageIndex = buildStatusListViewItem.ImageIndex
                                   };

            AddSubItem(listViewItem, "StartTime", buildStatusListViewItem.StartTime);
            AddSubItem(listViewItem, "Duration", buildStatusListViewItem.Duration);
            AddSubItem(listViewItem, "RequestedBy", buildStatusListViewItem.RequestedBy);
            AddSubItem(listViewItem, "Comment", buildStatusListViewItem.Comment);
            listViewItem.Tag = buildStatusListViewItem.Id;
            return listViewItem;
        }

        private static void UpdateSubItem(ListViewItem lvi, string name, string value)
        {
            var subItem = lvi.SubItems.Cast<ListViewItem.ListViewSubItem>().FirstOrDefault(i => i.Name == name);
            if (subItem == null) throw new Exception("Unable to find list view sub item" + name);
            // ReSharper disable RedundantCheckBeforeAssignment
            if (value != subItem.Text)
                // ReSharper restore RedundantCheckBeforeAssignment
                subItem.Text = value;
        }

        private static void AddSubItem(ListViewItem lvi, string name, string value)
        {
            var subItem = new ListViewItem.ListViewSubItem(lvi, value)
            {
                Name = name
            };
            lvi.SubItems.Add(subItem);
        }

        public void UpdateListItem(ListViewItem listViewItem, BuildStatusListViewItem buildStatus)
        {
            listViewItem.ImageIndex = buildStatus.ImageIndex;
            UpdateSubItem(listViewItem, "StartTime", buildStatus.StartTime);
            UpdateSubItem(listViewItem, "Duration", buildStatus.Duration);
            UpdateSubItem(listViewItem, "RequestedBy", buildStatus.RequestedBy);
            UpdateSubItem(listViewItem, "Comment", buildStatus.Comment);
        }

        private void RulesEngineRefreshRefreshStatus(object sender, RefreshStatusEventArgs args)
        {
            Invoke(() =>
            {
                var buildStatusListViewItems = args.BuildStatusListViewItems;
                if (listView1.Items.Count != 0 && listView1.Items.Count != buildStatusListViewItems.Count())
                {
                    listView1.Items.Clear();
                }
                if (listView1.Items.Count == 0)
                {
                    var listViewItems = buildStatusListViewItems.Select(AsListViewItem).ToArray();
                    listView1.Items.AddRange(listViewItems);
                }
                else
                {
                    var listViewItemsJoinedStatus = from listViewItem in listView1.Items.Cast<ListViewItem>()
                                                    join buildStatus in buildStatusListViewItems on listViewItem.Text equals buildStatus.Name
                                                    select new { listViewItem, buildStatus };
                    listViewItemsJoinedStatus.ToList().ForEach(i => UpdateListItem(i.listViewItem, i.buildStatus));
                }
            });
        }

        private void SirenofShameDeviceConnected(object sender, EventArgs e)
        {
            bool firstTimeSirenHasEverBeenConnected = !_settings.SirenEverConnected;
            if (firstTimeSirenHasEverBeenConnected)
            {
                _settings.SirenEverConnected = true;

                TrySetDefaultRule(TriggerType.BuildTriggered, 1, false);
                TrySetDefaultRule(TriggerType.InitialFailedBuild, 10, true);
                TrySetDefaultRule(TriggerType.SubsequentFailedBuild, 10, true);
                TrySetDefaultRule(TriggerType.SuccessfulBuild, 1, false);

                _settings.Save();
            }

            Invoke(() =>
            {
                _testSiren.Enabled = true;
                _timeboxEnforcer.Enabled = true;
            });
        }

        private void TrySetDefaultRule(TriggerType triggerType, int audioDuration, bool setLed)
        {
            Rule rule = _settings.Rules.FirstOrDefault(r => r.TriggerType == triggerType && r.BuildDefinitionId == null && r.TriggerPerson == null);
            if (rule != null)
            {
                rule.InheritAudioSettings = false;
                rule.AudioPattern = SirenOfShameDevice.AudioPatterns.First();
                rule.AudioDuration = audioDuration;
                rule.InheritLedSettings = !setLed;
                rule.LedPattern = setLed ? SirenOfShameDevice.LedPatterns.First() : null;
                rule.LightsDuration = null;
            }
        }

        private void SirenofShameDeviceDisconnected(object sender, EventArgs args)
        {
            Invoke(() =>
            {
                _testSiren.Enabled = false;
                _timeboxEnforcer.Enabled = false;
            });
        }

        private void Form1Load(object sender, EventArgs e)
        {
            Log.Debug("Form1 loaded");
            if (_settings == null)
            {
                _settings = new SirenOfShameSettings();
            }
            StartWatchingBuild();
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
            rulesEngine.UpdateStatusBar += RulesEngineUpdateStatusBar;
            rulesEngine.RefreshStatus += RulesEngineRefreshRefreshStatus;
            rulesEngine.TrayNotify += RulesEngineTrayNotify;
            rulesEngine.ModalDialog += RulesEngineModalDialog;
            rulesEngine.SetAudio += RulesEngineSetAudio;
            rulesEngine.SetLights += RulesEngineSetLights;
            rulesEngine.SetTrayIcon += RulesEngineSetTrayIcon;
            return rulesEngine;
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
                SirenOfShameDevice.SetLight(args.LedPattern, args.TimeSpan);
            }
        }

        private void RulesEngineSetAudio(object sender, SetAudioEventArgs args)
        {
            if (SirenOfShameDevice.IsConnected)
            {
                SirenOfShameDevice.SetAudio(args.AudioPattern, args.TimeSpan);
            }
        }

        private void RulesEngineTrayNotify(object sender, TrayNotifyEventArgs args)
        {
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
                notifyIcon.ShowBalloonTip(TIMEOUT, "Siren of Shame",
                        "The App has be moved to the tray.",
                        ToolTipIcon.Info);
            }
            else
            {

                Show();
            }
        }

        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                notifyIcon.ShowBalloonTip(TIMEOUT, "Siren of Shame",
                     "Don't you hate it when apps override the close button?" +
                     (Char)(13) + "... yea, we just did that." +
                     (Char)(13) + "But s'ok you can right click really exit.",
                     ToolTipIcon.Info);
            }
            else
            {
                RulesEngine.Stop();
            }
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
            ConfigureServer.Show(_settings);
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
            var configureSiren = new ConfigureSirenDialog(_settings);
            configureSiren.ShowDialog(this);
        }

        private void ListView1DoubleClick(object sender, EventArgs e)
        {
            OpenConfigureRulesDialog();
        }

        private void ListView1MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();

                _buildMenu.Show(listView1, e.X, e.Y);
                _affectsTrayIcon.Checked = buildDefinitionSetting == null ? true : buildDefinitionSetting.AffectsTrayIcon;
            }
        }

        private BuildDefinitionSetting GetActiveBuildDefinitionSetting()
        {
            var listViewItem = listView1.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (listViewItem == null) return null;

            string buildId = (string)listViewItem.Tag;

            var buildDefinitionSetting = _settings.BuildDefinitionSettings.FirstOrDefault(bds => bds.Id == buildId);
            if (buildDefinitionSetting == null)
            {
                Log.Error("Could not find a build definition settings for id " + buildId);
                return buildDefinitionSetting;
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
            ToolStripMenuItem menu = new ToolStripMenuItem(person);
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

            var playAudioMenu = new ToolStripMenuItem("Play the following audio") { Enabled = isConnected };
            var playLightsMenu = new ToolStripMenuItem("Turn on the following light pattern") { Enabled = isConnected };

            if (isConnected)
            {
                playAudioMenu.DropDownItems.AddRange(SirenOfShameDevice.AudioPatterns.Select(ap => AudioPatternMenu(ap, rule, buildDefinitionId, triggerType, person)).ToArray());
                playAudioMenu.Checked = playAudioMenu.DropDownItems.Cast<ToolStripMenuItem>().Any(d => d.Checked);

                playLightsMenu.DropDownItems.AddRange(SirenOfShameDevice.LedPatterns.Select(ap => LedPatternMenu(ap, rule, buildDefinitionId, triggerType, person)).ToArray());
                playLightsMenu.Checked = playLightsMenu.DropDownItems.Cast<ToolStripMenuItem>().Any(d => d.Checked);
            }
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
            var listViewItem = listView1.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            listViewItem.Remove();
        }

        private void OpenSettingsClick(object sender, EventArgs e)
        {
            Settings settings = new Settings(_settings);
            settings.ShowDialog();
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
    }
}