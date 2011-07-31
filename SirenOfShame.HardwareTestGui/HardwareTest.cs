using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Device.SdCardFileSystem;
using SirenOfShame.Lib.Services;

namespace SirenOfShame.HardwareTestGui
{
    public partial class HardwareTest : Form
    {
        private readonly ISirenOfShameDevice _sirenOfShameDevice;

        public HardwareTest()
        {
            InitializeComponent();
            _sirenOfShameDevice = new SirenOfShameDevice();
            _sirenOfShameDevice.Connected += _sirenOfShame_Connected;
            _sirenOfShameDevice.Disconnected += _sirenOfShame_Disconnected;
        }

        void _sirenOfShame_Disconnected(object sender, EventArgs e)
        {
            BeginInvoke((Action)(() =>
            {
                _connect.Enabled = true;
                _disconnect.Enabled = false;
            }));
        }

        void _sirenOfShame_Connected(object sender, EventArgs e)
        {
            BeginInvoke((Action)(() =>
            {
                Reload();
                _connect.Enabled = false;
                _disconnect.Enabled = true;
            }));
        }

        private void _connect_Click(object sender, EventArgs e)
        {
            _sirenOfShameDevice.TryConnect();
        }

        private void Reload()
        {
            ReloadAudioPatterns();
            ReloadLedPatterns();
        }

        private void ReloadLedPatterns()
        {
            _ledPatterns.Items.Clear();
            foreach (LedPattern ledPattern in _sirenOfShameDevice.LedPatterns)
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
        }

        private void ReloadAudioPatterns()
        {
            _audioPatterns.Items.Clear();
            foreach (AudioPattern audioPattern in _sirenOfShameDevice.AudioPatterns)
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
        }

        private void _audioStart_Click(object sender, EventArgs e)
        {
            StartAudio();
        }

        private void StartAudio()
        {
            AudioPattern selectedAudioPattern = (AudioPattern)_audioPatterns.SelectedItems[0].Tag;
            _sirenOfShameDevice.SetAudio(selectedAudioPattern, new TimeSpan(0, 0, 0, 0, (int)(_audioDuration.Value * 100)));
        }

        private void _audioStop_Click(object sender, EventArgs e)
        {
            StopAudio();
        }

        private void StopAudio()
        {
            _sirenOfShameDevice.SetAudio(null, null);
        }

        private void _ledStart_Click(object sender, EventArgs e)
        {
            StartLeds();
        }

        private void StartLeds()
        {
            LedPattern selectedLedPattern = (LedPattern)_ledPatterns.SelectedItems[0].Tag;
            _sirenOfShameDevice.SetLight(selectedLedPattern, new TimeSpan(0, 0, 0, 0, (int)(_ledDuration.Value * 100)));
        }

        private void _ledStop_Click(object sender, EventArgs e)
        {
            StopLeds();
        }

        private void StopLeds()
        {
            _sirenOfShameDevice.SetLight(null, null);
        }

        private void _startBoth_Click(object sender, EventArgs e)
        {
            StartAudio();
            StartLeds();
        }

        private void _stopBoth_Click(object sender, EventArgs e)
        {
            StopAudio();
            StopLeds();
        }

        private void _firmwareUpgrade_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "HEX File (*.hex)|*.hex"
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                using (Stream stream = File.OpenRead(dlg.FileName))
                {
                    _sirenOfShameDevice.PerformFirmwareUpgrade(stream, progress => BeginInvoke((Action)(() =>
                    {
                        _uploadProgress.Value = progress;
                    })));
                    MessageBox.Show("Firmware upgrade complete");
                }
            }
        }

        private void _send_Click(object sender, EventArgs e)
        {
            AudioFileService _audioFileService = new AudioFileService();
            Thread t = new Thread(() => _sirenOfShameDevice.UploadCustomPatterns(
                new List<UploadAudioPattern>{
                    new UploadAudioPatternStream(_audioPatternName1.Text, _audioFileService.Convert(_audioPattern1.Text)),
                    new UploadAudioPatternStream(_audioPatternName2.Text, _audioFileService.Convert(_audioPattern2.Text))
                },
                new List<UploadLedPattern>
                {
                    new UploadLedPattern(_ledPatternName1.Text, LedFileService.TextToPattern(_ledPattern1.Text)),
                    new UploadLedPattern(_ledPatternName2.Text, LedFileService.TextToPattern(_ledPattern2.Text)),
                },
                i => BeginInvoke((Action)(() => { _uploadProgress.Value = i; }))));
            t.Start();
        }

        private void _readDeviceInfo_Click(object sender, EventArgs e)
        {
            _deviceInfo.Text = "";
            SirenOfShameInfo deviceInfo = _sirenOfShameDevice.ReadDeviceInfo();
            StringBuilder info = new StringBuilder();
            info.AppendLine("Version: " + deviceInfo.Version + "\n");
            info.AppendLine("HardwareType: " + deviceInfo.HardwareType + "\n");
            info.AppendLine("AudioMode: " + deviceInfo.AudioMode + "\n");
            info.AppendLine("AudioPlayDuration: " + deviceInfo.AudioPlayDuration + "\n");
            info.AppendLine("LedMode: " + deviceInfo.LedMode + "\n");
            info.AppendLine("LedPlayDuration: " + deviceInfo.LedPlayDuration + "\n");
            _deviceInfo.Text = info.ToString();
        }

        private void _disconnect_Click(object sender, EventArgs e)
        {
            _sirenOfShameDevice.Disconnect();
        }
    }
}
