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
        private TimeSpan _timeSpan;

        public TimeboxEnforcer()
        {
            _timer.Interval = 1000;
            _timer.Tick += TimerTick;
            IocContainer.Instance.Compose(this); 
            InitializeComponent();

            UpdateDurationText();

            UpdateTimer(false);

            _timeboxAudio.Items.Add(new { Name = "None" });
            _timeboxAudio.Items.AddRange(SirenOfShameDevice.AudioPatterns.ToArray());
            _timeboxAudio.DisplayMember = "Name";
            _timeboxAudio.SelectedIndex = 1;

            _timeboxLights.Items.Add(new {Name = "None"});
            _timeboxLights.Items.AddRange(SirenOfShameDevice.LedPatterns.ToArray());
            _timeboxLights.DisplayMember = "Name";
            _timeboxLights.SelectedIndex = 1;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            TimeSpan newTimeSpan = _timeSpan - new TimeSpan(0, 0, 0, 1);
            if (newTimeSpan.Ticks == 0)
            {
                if (_timeboxAudio.SelectedIndex != 0)
                    SirenOfShameDevice.SetAudio((AudioPattern)_timeboxAudio.SelectedItem, new TimeSpan(0, 0, 0, 10));
                if (_timeboxLights.SelectedIndex != 0)
                    SirenOfShameDevice.SetLight((LedPattern)_timeboxLights.SelectedItem, new TimeSpan(0, 0, 0, 10));
            }
            UpdateDuration(newTimeSpan);
        }

        private void UpdateDuration(TimeSpan newTimeSpan)
        {
            _timeSpan = newTimeSpan;
            _countdown.Text = string.Format("{0}:{1:00}", Math.Abs((int)_timeSpan.TotalMinutes), Math.Abs(_timeSpan.Seconds));
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
            _start.Text = start ? "Stop" : "Start";
            if (start)
            {
                _timer.Start();
            } else
            {
                _timer.Stop();
            }
        }
        
        private void StartClick(object sender, EventArgs e)
        {
            bool running = _timer.Enabled;
            UpdateTimer(!running);
        }
    }
}
