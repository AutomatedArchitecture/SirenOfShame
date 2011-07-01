using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public sealed partial class AddRule : Form
    {
        private readonly SirenOfShameSettings _settings;
        private readonly Rule _rule;

        readonly BuildDefinitionSetting _anyBuild = new BuildDefinitionSetting { Name = "Any" };

        [Import(typeof(ISirenOfShameDevice))]
        public ISirenOfShameDevice SirenOfShameDevice { get; set; }

        public AddRule(SirenOfShameSettings settings, Rule rule)
            : this(settings)
        {
            _rule = rule;
            
            _when.Text = Rule.TriggerTypes[rule.TriggerType];
            SetAlertType(rule.AlertType);
            _inheritAudio.Checked = rule.InheritAudioSettings;
            _turnOnAudio.Checked = !rule.InheritAudioSettings;
            _inheritLightSetting.Checked = rule.InheritLedSettings;
            _turnOnLights.Checked = !rule.InheritLedSettings;
            _lights.SelectedItem = rule.LedPattern;
            _audio.SelectedItem = rule.AudioPattern;
            _lightsDurationTextBox.Text = rule.LightsDuration == null ? "" : rule.LightsDuration.ToString();
            _playLightsUntilBuildPasses.Checked = rule.LightsDuration == null;
            _customAudioDuration.Checked = rule.AudioDuration != null;
            _audioDurationTextBox.Text = rule.AudioDuration == null ? "" : rule.AudioDuration.ToString();
            _customLightsDuration.Checked = rule.LightsDuration != null;
            _playAudioUntilBuildPasses.Checked = rule.AudioDuration == null;
            _whoIsAnyone.Checked = rule.TriggerPerson == null;
            _whoIsCustom.Checked = rule.TriggerPerson != null;
            _who.Text = rule.TriggerPerson;
            if (rule.BuildDefinitionId == null)
            {
                _build.SelectedIndex = 0;
            }
            else
            {
                BuildDefinitionSetting buildDefinitionSetting = _settings.BuildDefinitionSettings.FirstOrDefault(bds => bds.Id == rule.BuildDefinitionId);
                _build.SelectedItem = buildDefinitionSetting;
            }

            _add.Text = "Update";
            Text = "Update Rule";
        }

        public AddRule(SirenOfShameSettings settings)
        {
            IocContainer.Instance.Compose(this);
            _settings = settings;
            InitializeComponent();

            EnableAudioSettings(enable: false);
            EnableLightSettings(enable: false);

            _when.DataSource = Rule.TriggerTypes.Select(i => new KeyValuePair<TriggerType, string>(i.Key, i.Value )).ToList();
            _when.DisplayMember = "Value";
            
            var buildDefinitions = _settings.BuildDefinitionSettings
                .Where(bd => bd.Active)
                .ToList();
            buildDefinitions.Insert(0, _anyBuild);
            _build.DataSource = buildDefinitions;
            _build.DisplayMember = "Name";

            _audio.DataSource = SirenOfShameDevice.AudioPatterns.ToList();
            _audio.DisplayMember = "Name";

            _lights.DataSource = SirenOfShameDevice.LedPatterns.ToList();
            _lights.DisplayMember = "Name";

            _who.DataSource = _settings.BuildDefinitionSettings.SelectMany(bds => bds.People).Distinct().ToList();
        }

        private AlertType GetAlertType()
        {
            if (_inheritAlerts.Checked) return AlertType.Inherit;
            if (_trayAlert.Checked) return AlertType.TrayAlert;
            if (_modalDialog.Checked) return AlertType.ModalDialog;
            if (_noAlert.Checked) return AlertType.NoAlert;
            throw new Exception("Must select an alert type.");
        }
        
        private void SetAlertType(AlertType alertType)
        {
            _inheritAlerts.Checked = alertType == AlertType.Inherit;
            _trayAlert.Checked = alertType == AlertType.TrayAlert;
            _modalDialog.Checked = alertType == AlertType.ModalDialog;
            _noAlert.Checked = alertType == AlertType.NoAlert;
        }
        
        private void AddClick(object sender, EventArgs e)
        {
            Rule rule = _rule ?? new Rule();

            rule.TriggerType = ((KeyValuePair<TriggerType, string>) _when.SelectedItem).Key;
            rule.BuildDefinitionId = GetBuildDefinitionId();
            rule.TriggerPerson = _whoIsAnyone.Checked ? null : _who.Text;
            rule.AlertType = GetAlertType();
            rule.AudioDuration = GetAudioDuration();
            rule.LightsDuration = GetLightsDuration();
            rule.InheritAudioSettings = _inheritAudio.Checked;
            rule.InheritLedSettings = _inheritLightSetting.Checked;

            if (!rule.InheritLedSettings)
                rule.LedPattern = GetLedPattern();
            if (!rule.InheritAudioSettings)
                rule.AudioPattern = GetAudioPattern();

            if (_rule == null)
                _settings.Rules.Add(rule);
            _settings.Save();
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            // turn off any tests that might be in progress
            TestAudio(false);
            TestLights(false);
            base.OnClosed(e);
        }

        private LedPattern GetLedPattern()
        {
            return _turnOnLights.Checked ? (LedPattern)_lights.SelectedItem : null;
        }

        private AudioPattern GetAudioPattern()
        {
            return _turnOnAudio.Checked ? (AudioPattern)_audio.SelectedItem : null;
        }

        private int? GetLightsDuration()
        {
            if (_playLightsUntilBuildPasses.Checked) return null;
            int result;
            return int.TryParse(_lightsDurationTextBox.Text, out result) ? result : (int?)null;
        }

        private int? GetAudioDuration()
        {
            if (_playAudioUntilBuildPasses.Checked) return null;
            int result;
            return int.TryParse(_audioDurationTextBox.Text, out result) ? result : (int?)null;
        }

        private string GetBuildDefinitionId()
        {
            return _build.SelectedItem == _anyBuild ? null : ((BuildDefinitionSetting) _build.SelectedItem).Id;
        }

        private void CancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void WhoIsCustomCheckedChanged(object sender, EventArgs e)
        {
            EnableWho(enable: true);
        }

        private void EnableWho(bool enable)
        {
            _who.Enabled = enable;
            if (!enable)
                _who.Text = "";
        }

        private void WhoIsAnyoneCheckedChanged(object sender, EventArgs e)
        {
            EnableWho(enable: false);
        }

        private void InheritAudioSettingsCheckedChanged(object sender, EventArgs e)
        {
            EnableAudioSettings(enable: false);
        }

        private void EnableAudioSettings(bool enable)
        {
            _audio.Enabled = enable;

            _playAudioUntilBuildPasses.Enabled = enable;
            _audioDurationTextBox.Enabled = enable;
            _customAudioDuration.Enabled = enable;
            _testAudio.Enabled = enable;
        }

        private void EnableLightSettings(bool enable)
        {
            _lights.Enabled = enable;
            
            _playLightsUntilBuildPasses.Enabled = enable;
            _lightsDurationTextBox.Enabled = enable;
            _customLightsDuration.Enabled = enable;
            _testLights.Enabled = enable;
        }

        private void TurnOnAudioCheckedChanged(object sender, EventArgs e)
        {
            EnableAudioSettings(true);
        }

        private void InheritLightSettingCheckedChanged(object sender, EventArgs e)
        {
            EnableLightSettings(false);
        }

        private void TurnOnLightsCheckedChanged(object sender, EventArgs e)
        {
            EnableLightSettings(true);
        }

        private void TestAudioClick(object sender, EventArgs e)
        {
            TestAudio(!_testingAudio);
        }

        private void TestAudio(bool testing)
        {
            if (!SirenOfShameDevice.IsConnected) return;
            _testingAudio = testing;
            _testAudio.ImageIndex = testing ? 1 : 0;
            var audioPattern = testing ? GetAudioPattern() : null;
            SirenOfShameDevice.SetAudio(audioPattern, null);
        }

        private bool _testingLights;
        private bool _testingAudio;

        private void TestLights(bool testing)
        {
            if (!SirenOfShameDevice.IsConnected) return;
            _testingLights = testing;
            _testLights.ImageIndex = testing ? 1 : 0;
            var ledPattern = testing ? GetLedPattern() : null;
            SirenOfShameDevice.SetLight(ledPattern, null);
        }
        
        private void TestLightsClick(object sender, EventArgs e)
        {
            TestLights(!_testingLights);
            _testingLights = true;
        }
    }
}
