using System;
using System.Windows.Forms;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.HardwareTestGui
{
    public partial class FullTest : UserControl
    {
        private DateTime? _ledEndTime;
        private DateTime? _audioEndTime;

        public FullTest()
        {
            InitializeComponent();
            if (!DesignMode && Program.SirenOfShameDevice != null)
            {
                Program.SirenOfShameDevice.Connected += SirenOfShameDevice_Connected;
                Program.SirenOfShameDevice.Disconnected += SirenOfShameDevice_Disconnected;

                _timer.Enabled = true;
            }
        }

        private void SirenOfShameDevice_Disconnected(object sender, EventArgs e)
        {
            _audioPatterns.Items.Clear();
            _ledPatterns.Items.Clear();
            EnabledControls(false);
            _deviceInfo.Text = "Disconnected";
        }

        private void SirenOfShameDevice_Connected(object sender, EventArgs e)
        {
            Reload();
            EnabledControls(true);
            _deviceInfo.Text = Program.GetDeviceInfoAsString();
        }

        private void EnabledControls(bool enable)
        {
            _audioStart.Enabled = enable;
            _audioStop.Enabled = enable;
            _ledsStart.Enabled = enable;
            _ledsStop.Enabled = enable;
        }

        private void Reload()
        {
            ReloadAudioPatterns();
            ReloadLedPatterns();
        }

        private void ReloadLedPatterns()
        {
            this.Invoke(() =>
            {
                _ledPatterns.Items.Clear();
                foreach (LedPattern ledPattern in Program.SirenOfShameDevice.LedPatterns)
                {
                    ListViewItem lvi = new ListViewItem(ledPattern.Name);
                    lvi.SubItems.Add(ledPattern.Name);
                    lvi.Tag = ledPattern;
                    _ledPatterns.Items.Add(lvi);
                }
                if (_ledPatterns.Items.Count > 0)
                {
                    _ledPatterns.Items[0].Selected = true;
                }
            });
        }

        private void ReloadAudioPatterns()
        {
            this.Invoke(() =>
            {
                _audioPatterns.Items.Clear();
                foreach (AudioPattern audioPattern in Program.SirenOfShameDevice.AudioPatterns)
                {
                    ListViewItem lvi = new ListViewItem(audioPattern.Name);
                    lvi.SubItems.Add(audioPattern.Name);
                    lvi.Tag = audioPattern;
                    _audioPatterns.Items.Add(lvi);
                }
                if (_audioPatterns.Items.Count > 0)
                {
                    _audioPatterns.Items[0].Selected = true;
                }
            });
        }

        private void _audioStart_Click(object sender, EventArgs e)
        {
            StartAudio();
        }

        private void _audioStop_Click(object sender, EventArgs e)
        {
            StopAudio();
        }

        private void _ledsStart_Click(object sender, EventArgs e)
        {
            StartLeds();
        }

        private void _ledsStop_Click(object sender, EventArgs e)
        {
            StopLeds();
        }

        private void StartAudio()
        {
            AudioPattern selectedAudioPattern = (AudioPattern)_audioPatterns.SelectedItems[0].Tag;
            _audioEndTime = DateTime.Now.AddMilliseconds((double)(_audioDuration.Value * 100));
            Program.SirenOfShameDevice.PlayAudioPattern(selectedAudioPattern, new TimeSpan(0, 0, 0, 0, (int)(_audioDuration.Value * 100)));
        }

        private void StopAudio()
        {
            Program.SirenOfShameDevice.PlayAudioPattern(null, null);
        }

        private void StartLeds()
        {
            LedPattern selectedLedPattern = (LedPattern)_ledPatterns.SelectedItems[0].Tag;
            _ledEndTime = DateTime.Now.AddMilliseconds((double)(_ledDuration.Value * 100));
            Program.SirenOfShameDevice.PlayLightPattern(selectedLedPattern, new TimeSpan(0, 0, 0, 0, (int)(_ledDuration.Value * 100)));
        }

        private void StopLeds()
        {
            Program.SirenOfShameDevice.PlayLightPattern(null, null);
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (Program.SirenOfShameDevice.IsConnected)
            {
                try
                {
                    var deviceInfo = Program.SirenOfShameDevice.ReadDeviceInfo();
                    if (deviceInfo.AudioMode == 0)
                    {
                        _audioEndTime = null;
                    }
                    if (deviceInfo.LedMode == 0)
                    {
                        _ledEndTime = null;
                    }
                    _deviceInfo.Text = Program.GetDeviceInfoAsString(deviceInfo);
                }
                catch (Exception ex)
                {
                    _deviceInfo.Text = "Lost connection with device...\n" + ex;
                }
            }

            if (_audioEndTime != null)
            {
                var audioTime = _audioEndTime.Value - DateTime.Now;
                _audioRunTime.Text = audioTime.ToString();
            }
            else
            {
                _audioRunTime.Text = "Stopped";
            }

            if (_ledEndTime != null)
            {
                var ledTime = _ledEndTime.Value - DateTime.Now;
                _ledRunTime.Text = ledTime.ToString();
            }
            else
            {
                _ledRunTime.Text = "Stopped";
            }
        }

        private void _configureSiren_Click(object sender, EventArgs e)
        {
            const string fileName = "HardwareTestGui.Settings.xml";
            SirenOfShameSettings settings = SirenOfShameSettings.GetAppSettings(fileName);
            ConfigureSirenDialog dlg = new ConfigureSirenDialog(settings, Program.SirenOfShameDevice);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                settings.Save(fileName);
            }
        }
    }
}
