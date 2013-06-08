﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
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
        public SirenOfShameSettings Settings { private get; set; }
        protected string Url;
        public DateTime LocalStartTime { get; protected set; }
        public string BuildDefinitionId { get; private set; }
        private static readonly ILog Log = MyLogManager.GetLogger(typeof(ViewBuildBase));

        [Import(typeof(ISirenOfShameDevice))]
        public ISirenOfShameDevice SirenOfShameDevice { private get; set; }

        protected ViewBuildBase()
        {
            IocContainer.Instance.Compose(this);
        }

        protected ViewBuildBase(SirenOfShameSettings settings) : this()
        {
            Settings = settings;
        }

        protected virtual void InitializeLabels(BuildStatusDto buildStatusDto)
        {
            BuildDefinitionId = buildStatusDto.BuildDefinitionId;
            Url = buildStatusDto.Url;
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

        public static Color SuccessColor = Color.FromArgb(255, 81, 163, 81);
        public static Color FailColor = Color.FromArgb(255, 189, 54, 47);
        public static Color PrimaryColor = Color.FromArgb(255, 40, 95, 152);

        private readonly static Dictionary<BuildStatusEnum, Color> BuildStatusToColorMap = new Dictionary<BuildStatusEnum, Color>
        {
            { BuildStatusEnum.Working, SuccessColor },
            { BuildStatusEnum.Broken, FailColor },
        };

        protected Color GetBackgroundColor(BuildStatusEnum buildStatusEnum)
        {
            return GetColorForBuildType(BuildStatusToColorMap, buildStatusEnum, PrimaryColor);
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

        private BuildDefinitionSetting GetActiveBuildDefinitionSetting()
        {
            return Settings.FindBuildDefinitionById(BuildDefinitionId);
        }

        protected void BuildMenuOpening(
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

                var itemsToAppend = buildDefinitionSetting
                    .PeopleMinusUserMappings(Settings)
                    .Distinct()
                    .Select(p => PersonMenu(p, buildDefinitionSetting))
                    .ToArray();
                AddToolStripItems(buildMenu.Items, itemsToAppend);
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
                Log.Error("User clicked '" + toolStripSender.Text + "' but it had no tag");
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

        protected void EditRulesClick(Control editRules, ContextMenuStrip buildMenu, ToolStripMenuItem affectsTrayIcon)
        {
            BuildDefinitionSetting buildDefinitionSetting = GetActiveBuildDefinitionSetting();
            var location = PointToScreen(editRules.Location);
            buildMenu.Show(location.X + editRules.Width, location.Y + editRules.Height);
            affectsTrayIcon.Checked = buildDefinitionSetting == null || buildDefinitionSetting.AffectsTrayIcon;
        }

        protected virtual Label GetStartTimeLabel()
        {
            return null;
        }
        
        public void RecalculatePrettyDate()
        {
            var startTimeLabel = GetStartTimeLabel();
            if (startTimeLabel == null) return;
            bool badDate = LocalStartTime == DateTime.MinValue;
            startTimeLabel.Visible = !badDate;
            if (badDate) return;
            startTimeLabel.Text = LocalStartTime.PrettyDate();
        }

        internal void LaunchUrl()
        {
            if (!string.IsNullOrWhiteSpace(Url) && Url.StartsWith("http"))
            {
                Process.Start(Url);
            }
        }
    }
}
