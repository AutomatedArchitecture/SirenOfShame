using System;
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

            _timeboxAudio.Items.Add(new { Name = "None" });
            _timeboxAudio.Items.AddRange(SirenOfShameDevice.AudioPatterns.ToArray());
            _timeboxAudio.DisplayMember = "Name";
            if (_timeboxAudio.Items.Count > 1)
                _timeboxAudio.SelectedIndex = 1;

            _timeboxLights.Items.Add(new {Name = "None"});
            _timeboxLights.Items.AddRange(SirenOfShameDevice.LedPatterns.ToArray());
            _timeboxLights.DisplayMember = "Name";
            if (_timeboxLights.Items.Count > 1)
                _timeboxLights.SelectedIndex = 1;
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

        private void TimerTick(object sender, EventArgs e)
        {
            TimeSpan newTimeSpan = new TimeSpan(0, _duration.Value, 0) - new TimeSpan(0, 0, (int)(DateTime.Now - _startTimer).TotalSeconds);
            if (newTimeSpan.Ticks == 0)
            {
                if (_timeboxAudio.SelectedIndex != 0)
                {
                    var duration = GetTimespanOrDefault(_audioDuration, 10);
                    SirenOfShameDevice.SetAudio((AudioPattern)_timeboxAudio.SelectedItem, duration);
                }
                if (_timeboxLights.SelectedIndex != 0)
                {
                    var duration = GetTimespanOrDefault(_lightDuration, 10);
                    SirenOfShameDevice.SetLight((LedPattern)_timeboxLights.SelectedItem, duration);
                }
            }
            UpdateDuration(newTimeSpan);
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
