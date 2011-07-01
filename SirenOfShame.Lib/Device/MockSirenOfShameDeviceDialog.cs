using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SirenOfShame.Lib.Device
{
    public partial class MockSirenOfShameDeviceDialog : Form
    {
        private AudioPattern _currentAudioPattern;
        private TimeSpan? _currentAudioDuration;
        private LedPattern _currentLightPattern;
        private TimeSpan? _currentLightDuration;
        public event EventHandler ConnectedChanged;

        public MockSirenOfShameDeviceDialog()
        {
            InitializeComponent();
        }

        public bool Connected
        {
            get { return _connected.Checked; }
        }

        public IEnumerable<AudioPattern> AudioPatterns
        {
            get
            {
                return _audioPatterns.Text
                    .Split('\n')
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(s => new AudioPattern { Name = s });
            }
        }

        public IEnumerable<LedPattern> LedPatterns
        {
            get
            {
                return _ledPatterns.Text
                    .Split('\n')
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(s => new LedPattern { Name = s });
            }
        }

        public string AudioPatternText
        {
            get { return _audioPatterns.Text; }
            set { _audioPatterns.Text = value; }
        }

        public string LedPatternText
        {
            get { return _ledPatterns.Text; }
            set { _ledPatterns.Text = value; }
        }

        public AudioPattern CurrentAudioPattern
        {
            get { return _currentAudioPattern; }
        }

        public TimeSpan? CurrentAudioDuration
        {
            get { return _currentAudioDuration; }
        }

        public LedPattern CurrentLightPattern
        {
            get { return _currentLightPattern; }
        }

        public TimeSpan? CurrentLightDuration
        {
            get { return _currentLightDuration; }
        }

        private void _connected_CheckedChanged(object sender, EventArgs e)
        {
            if (ConnectedChanged != null)
            {
                ConnectedChanged(this, new EventArgs());
            }
        }

        public void SetAudio(AudioPattern pattern, TimeSpan? duration)
        {
            _currentAudioPattern = pattern;
            _currentAudioDuration = duration;
        }

        public void SetLight(LedPattern pattern, TimeSpan? duration)
        {
            _currentLightPattern = pattern;
            _currentLightDuration = duration;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (CurrentAudioDuration != null)
            {
                _currentAudioDuration = CurrentAudioDuration.Value.Add(new TimeSpan(0, 0, 0, 0, -_timer.Interval));
                if (CurrentAudioDuration.Value.TotalMilliseconds < 0)
                {
                    _currentAudioPattern = null;
                    _currentAudioDuration = null;
                }
            }
            if (CurrentLightDuration != null)
            {
                _currentLightDuration = CurrentLightDuration.Value.Add(new TimeSpan(0, 0, 0, 0, -_timer.Interval));
                if (CurrentLightDuration.Value.TotalMilliseconds < 0)
                {
                    _currentLightPattern = null;
                    _currentLightDuration = null;
                }
            }
            _audioPattern.Text = CurrentAudioPattern == null ? "None" : CurrentAudioPattern.Name + "(" + CurrentAudioDuration + ")";
            _ledPattern.Text = CurrentLightPattern == null ? "None" : CurrentLightPattern.Name + "(" + CurrentLightDuration + ")";
        }
    }
}
