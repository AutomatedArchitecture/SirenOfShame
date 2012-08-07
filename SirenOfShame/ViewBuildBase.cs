using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Resources2;
using log4net;

namespace SirenOfShame
{
    public class ViewBuildBase : UserControl
    {
        protected readonly SirenOfShameSettings Settings;
        protected string Url;
        protected DateTime? LocalStartTime;
        public string BuildName { get; private set; }
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(ViewBuildBase));

        [Import(typeof(ISirenOfShameDevice))]
        public ISirenOfShameDevice SirenOfShameDevice { get; set; }

        // required for Visual Studio designer support :(
        public ViewBuildBase() { }

        public ViewBuildBase(SirenOfShameSettings settings)
        {
            Settings = settings;
            IocContainer.Instance.Compose(this);
        }

        protected virtual void InitializeLabels(BuildStatusDto buildStatusDto)
        {
            BuildName = buildStatusDto.Name;
            Url = buildStatusDto.Url;
        }

        protected readonly IEnumerable<KeyValuePair<int?, string>> Durations = new List<KeyValuePair<int?, string>>
        {
            new KeyValuePair<int?, string>(1, "1 Second"),
            new KeyValuePair<int?, string>(5, "5 Seconds"),
            new KeyValuePair<int?, string>(10, "10 Seconds"),
            new KeyValuePair<int?, string>(30, "30 Seconds"),
            new KeyValuePair<int?, string>(60, "60 Seconds"),
            new KeyValuePair<int?, string>(null, "Until the build Passes"),
        };

        private readonly Dictionary<BuildStatusEnum, Color> _buildStatusToColorMap = new Dictionary<BuildStatusEnum, Color>
        {
            { BuildStatusEnum.Working, Color.FromArgb(255, 50, 175, 82) },
            { BuildStatusEnum.Broken, Color.FromArgb(255, 222, 64, 82) },
        };

        protected Color GetBackgroundColor(BuildStatusEnum buildStatusEnum)
        {
            return GetColorForBuildType(_buildStatusToColorMap, buildStatusEnum, Color.FromArgb(255, 40, 95, 152));
        }

        private static Color GetColorForBuildType(Dictionary<BuildStatusEnum, Color> dictionary, BuildStatusEnum newsItemEventType, Color defaultColor)
        {
            Color color;
            if (dictionary.TryGetValue(newsItemEventType, out color))
                return color;
            return defaultColor;
        }

        public void UpdateListItem(BuildStatusDto buildStatus)
        {
            InitializeLabels(buildStatus);
        }

        protected BuildDefinitionSetting GetActiveBuildDefinitionSetting()
        {
            return Settings.FindBuildDefinitionByName(BuildName);
        }

        protected void BuildMenuOpening(
            object sender, 
            CancelEventArgs e, 
            ContextMenuStrip buildMenu, 
            ToolStripMenuItem when, 
            ToolStripMenuItem affectsTrayIcon,
            ToolStripMenuItem stopWatching,
            ToolStripSeparator toolStripSeparator1
            )
        {
            BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();

            IEnumerable<ToolStripMenuItem> toolStripItems = Rule.TriggerTypes.Select(i => WhenMenu(i, buildDefinitionSetting, person: null));
            when.DropDownItems.Clear();
            AddToolStripItems(when.DropDownItems, toolStripItems);

            buildMenu.Items.Clear();
            if (buildDefinitionSetting != null)
            {
                buildMenu.Items.Add(affectsTrayIcon);
                buildMenu.Items.Add(stopWatching);
            }
            buildMenu.Items.Add(when);
            if (buildDefinitionSetting != null)
            {
                buildMenu.Items.Add(toolStripSeparator1);
                AddToolStripItems(buildMenu.Items, buildDefinitionSetting.People.Select(p => PersonMenu(p, buildDefinitionSetting)).ToArray());
            }
        }

        private static void AddToolStripItems(ToolStripItemCollection items, IEnumerable<ToolStripMenuItem> toolStripItems)
        {
            foreach (ToolStripMenuItem toolStripItem in toolStripItems)
            {
                items.Add(toolStripItem);
            }
        }

        private ToolStripMenuItem WhenMenu(KeyValuePair<TriggerType, string> triggerTypeDescription, BuildDefinitionSetting buildDefinitionSetting, string person)
        {
            var triggerType = triggerTypeDescription.Key;
            string buildDefinitionId = buildDefinitionSetting == null ? null : buildDefinitionSetting.Id;
            var rule = Settings.FindRule(triggerType, buildDefinitionId, person);

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


        private void RuleDropDownItemClick(object sender, EventArgs e)
        {
            var toolStripSender = (ToolStripItem)sender;
            RuleDropDownItemTag tag = (RuleDropDownItemTag)toolStripSender.Tag;

            if (tag == null)
            {
                _log.Error("User clicked '" + toolStripSender.Text + "' but it had no tag");
                return;
            }

            var rule = Settings.Rules.FirstOrDefault(r =>
                r.TriggerType == tag.TriggerType &&
                r.BuildDefinitionId == tag.BuildDefinitionId &&
                r.TriggerPerson == tag.TriggerPerson
                );

            // find/add
            if (rule == null)
            {
                // base new rules on any generic high level rules
                var baseRule = Settings.Rules.FirstOrDefault(r =>
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

                Settings.Rules.Add(rule);
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

            Settings.Save();
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

        private IEnumerable<ToolStripMenuItem> GetDurations(Rule rule, LedPattern ledPattern, AudioPattern audioPattern, bool patternIsMatch, string buildDefinitionId, TriggerType triggerType, string person)
        {
            int? duration = null;
            if (rule != null)
                duration = ledPattern == null ? rule.AudioDuration : rule.LightsDuration;

            var durations = Durations.Select(d => new ToolStripMenuItem(d.Value)
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

        private ToolStripMenuItem PersonMenu(string person, BuildDefinitionSetting buildDefinitionSetting)
        {
            var displayName = Settings.TryGetDisplayName(person);
            ToolStripMenuItem menu = new ToolStripMenuItem(displayName);
            var toolStripItems = Rule.TriggerTypes.Select(i => WhenMenu(i, buildDefinitionSetting, person));
            AddToolStripItems(menu.DropDownItems, toolStripItems);
            return menu;
        }


        protected void StopWatchingClick()
        {
            BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();
            if (buildDefinitionSetting == null) return;
            buildDefinitionSetting.Active = false;
            Settings.Save();
            Dispose();
        }

        protected void AffectsTrayIconClick()
        {
            BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();
            if (buildDefinitionSetting == null) return;

            buildDefinitionSetting.AffectsTrayIcon = !buildDefinitionSetting.AffectsTrayIcon;
            Settings.Save();
        }

        protected void EditRulesClick(Label editRules, ContextMenuStrip buildMenu, ToolStripMenuItem affectsTrayIcon)
        {
            BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();
            var location = PointToScreen(editRules.Location);
            buildMenu.Show(location.X + editRules.Width, location.Y + editRules.Height);
            affectsTrayIcon.Checked = buildDefinitionSetting == null || buildDefinitionSetting.AffectsTrayIcon;
        }

        protected void RecalculatePrettyDate(Label startTime)
        {
            startTime.Visible = LocalStartTime.HasValue;
            if (!LocalStartTime.HasValue) return;
            startTime.Text = LocalStartTime.Value.PrettyDate();
        }
    }
}
