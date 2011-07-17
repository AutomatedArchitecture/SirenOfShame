using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame
{
    public partial class TimeboxEnforcer : FormBase
    {
        [Import(typeof(ISirenOfShameDevice))]
        public ISirenOfShameDevice SirenOfShameDevice { get; set; }

        private Timer _timer = new Timer();
        private DateTime _startTimer;

        public TimeboxEnforcer()
        {
            _timer.Interval = 500;
            _timer.Tick += TimerTick;
            IocContainer.Instance.Compose(this); 
            InitializeComponent();

            UpdateDurationText();

            UpdateTimer(false);

            InitializeCombobox(_timeboxAudio, SirenOfShameDevice.AudioPatterns);
            InitializeCombobox(_timeboxLights, SirenOfShameDevice.LedPatterns);
            InitializeCombobox(_warningAudio, SirenOfShameDevice.AudioPatterns);
            InitializeCombobox(_warningLights, SirenOfShameDevice.LedPatterns);
        }

        private void InitializeCombobox(ComboBox combobox, IEnumerable<object> patterns)
        {
            combobox.Items.Add(new {Name = "None"});
            combobox.Items.AddRange(patterns.ToArray());
            combobox.DisplayMember = "Name";
            if (combobox.Items.Count > 1)
            {
                combobox.SelectedIndex = 1;
            }
        }

        private TimeSpan GetTimespanOrDefault(TextBox textBox, int defaultSeconds)
        {
            int duration;
            if (!Int32.TryParse(textBox.Text, out duration))
            {
                duration = defaultSeconds;
            }
            return new TimeSpan(0, 0, 0, duration);
        }

        private bool _timerHitZero = false;
        private bool _timerWarningHit = false;

        private void TimerTick(object sender, EventArgs e)
        {
            TimeSpan newTimeSpan = new TimeSpan(0, 0, _duration.Value) - new TimeSpan(0, 0, (int)(DateTime.Now - _startTimer).TotalSeconds);
            UpdateDuration(newTimeSpan);
            if (newTimeSpan.Ticks < 0 && !_timerHitZero)
            {
                _timerHitZero = true;
                if (_timeboxAudio.SelectedIndex != 0)
                {
                    var duration = GetTimespanOrDefault(_timeboxAudioDuration, 10);
                    SirenOfShameDevice.SetAudio((AudioPattern)_timeboxAudio.SelectedItem, duration);
                }
                if (_timeboxLights.SelectedIndex != 0)
                {
                    var duration = GetTimespanOrDefault(_timeboxLightDuration, 10);
                    SirenOfShameDevice.SetLight((LedPattern)_timeboxLights.SelectedItem, duration);
                }
                SosMessageBox.Show("Meeting adjourned", "This meeting is officially over.", "Ok");
            }

            if (newTimeSpan.TotalMinutes < 5 && !_timerWarningHit)
            {
                _timerWarningHit = true;
                if (_warningAudio.SelectedIndex != 0)
                {
                    var duration = GetTimespanOrDefault(_warningAudioDuration, 10);
                    SirenOfShameDevice.SetAudio((AudioPattern)_warningAudio.SelectedItem, duration);
                }
                if (_warningLights.SelectedIndex != 0)
                {
                    var duration = GetTimespanOrDefault(_warningLightDuration, 10);
                    SirenOfShameDevice.SetLight((LedPattern)_warningLights.SelectedItem, duration);
                }
            }
        }

        private void UpdateDuration(TimeSpan newTimeSpan)
        {
            _countdown.Text = string.Format("{0}:{1:00}", Math.Abs((int)newTimeSpan.TotalMinutes), Math.Abs(newTimeSpan.Seconds));
        }
        
        private void CloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void DurationValueChanged(object sender, EventArgs e)
        {
            UpdateDurationText();
        }

        private void UpdateDurationText()
        {
            _durationText.Text = string.Format("{0} Minutes", _duration.Value);
            UpdateDuration(new TimeSpan(0, 0, _duration.Value, 0));
        }

        private void UpdateTimer(bool start)
        {
            _start.Text = start ? "Reset" : "Start";
            if (start)
            {
                _startTimer = DateTime.Now;
                _timerHitZero = false;
                _timerWarningHit = false;
                _timer.Start();
            } else
            {
                _timer.Stop();
                UpdateDurationText();
            }
        }
        
        private void StartClick(object sender, EventArgs e)
        {
            bool running = _timer.Enabled;
            UpdateTimer(!running);
        }
    }
}
