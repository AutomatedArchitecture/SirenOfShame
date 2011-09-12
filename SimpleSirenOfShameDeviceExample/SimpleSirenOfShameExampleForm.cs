using System;
using System.Text;
using System.Windows.Forms;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;

namespace SimpleSirenOfShameDeviceExample
{
    public partial class SimpleSirenOfShameExampleForm : Form
    {
        private readonly ISirenOfShameDevice _sirenOfShameDevice;

        public SimpleSirenOfShameExampleForm()
        {
            InitializeComponent();
            EnableButtons(false);
            _sirenOfShameDevice = new SirenOfShameDevice();
            _sirenOfShameDevice.Connected += SirenOfShameDeviceConnected;
            _sirenOfShameDevice.Disconnected += SirenOfShameDeviceDisconnected;
        }

        void SirenOfShameDeviceDisconnected(object sender, EventArgs e)
        {
            EnableButtons(false);
        }

        void SirenOfShameDeviceConnected(object sender, EventArgs e)
        {
            EnableButtons(true);
        }

        private void EnableButtons(bool sosDeviceConnected)
        {
            _tryConnect.Enabled = !sosDeviceConnected;
            _disconnect.Enabled = sosDeviceConnected;
            _playAudio.Enabled = sosDeviceConnected;
            _stopAudio.Enabled = sosDeviceConnected;
            _audioDuration.Enabled = sosDeviceConnected;
            _audioPattern.Enabled = sosDeviceConnected;
            _playLed.Enabled = sosDeviceConnected;
            _stopLed.Enabled = sosDeviceConnected;
            _ledDuration.Enabled = sosDeviceConnected;
            _ledPattern.Enabled = sosDeviceConnected;
            _refreshDeviceInfo.Enabled = sosDeviceConnected;
            if (_sirenOfShameDevice != null)
            {
                _connectionStatus.Text = _sirenOfShameDevice.IsConnected ? "Connected" : "Not Connected";
            }
            RefreshDeviceInfo();
            RefreshAudioPatterns();
            RefreshLedPatterns();
        }

        private void RefreshAudioPatterns()
        {
            _audioPattern.Items.Clear();
            if (_sirenOfShameDevice == null)
            {
                return;
            }

            foreach (var audioPattern in _sirenOfShameDevice.AudioPatterns)
            {
                _audioPattern.Items.Add(audioPattern);
            }
        }

        private void RefreshLedPatterns()
        {
            _ledPattern.Items.Clear();
            if (_sirenOfShameDevice == null)
            {
                return;
            }

            foreach (var ledPattern in _sirenOfShameDevice.LedPatterns)
            {
                _ledPattern.Items.Add(ledPattern);
            }
        }

        private void RefreshDeviceInfo()
        {
            if (_sirenOfShameDevice == null)
            {
                _deviceInfo.Text = "Not Connected";
                return;
            }

            var deviceInfo = _sirenOfShameDevice.ReadDeviceInfo();

            StringBuilder deviceInfoText = new StringBuilder();
            deviceInfoText.AppendLine("FirmwareVersion: " + deviceInfo.FirmwareVersion + "\n");
            deviceInfoText.AppendLine("HardwareType: " + deviceInfo.HardwareType + "\n");
            deviceInfoText.AppendLine("HardwareVersion: " + deviceInfo.HardwareVersion + "\n");
            deviceInfoText.AppendLine("AudioMode: " + deviceInfo.AudioMode + "\n");
            deviceInfoText.AppendLine("AudioPlayDuration: " + deviceInfo.AudioPlayDuration + "\n");
            deviceInfoText.AppendLine("LedMode: " + deviceInfo.LedMode + "\n");
            deviceInfoText.AppendLine("LedPlayDuration: " + deviceInfo.LedPlayDuration + "\n");
            deviceInfoText.AppendLine("External Memory Size: " + SiUnitHelpers.ToBinaryString(deviceInfo.ExternalMemorySize) + "B\n");
            _deviceInfo.Text = deviceInfoText.ToString();
        }

        protected override void WndProc(ref Message m)
        {
            if (_sirenOfShameDevice != null)
            {
                _sirenOfShameDevice.WndProc(ref m);
            }
            base.WndProc(ref m);
        }

        private void _tryConnect_Click(object sender, EventArgs e)
        {
            _sirenOfShameDevice.TryConnect();
        }

        private void _disconnect_Click(object sender, EventArgs e)
        {
            _sirenOfShameDevice.Disconnect();
        }

        private void _refreshDeviceInfo_Click(object sender, EventArgs e)
        {
            RefreshDeviceInfo();
        }

        private void _playAudio_Click(object sender, EventArgs e)
        {
            var pattern = (AudioPattern)_audioPattern.SelectedItem;
            var duration = new TimeSpan(0, 0, 0, (int)_audioDuration.Value);
            _sirenOfShameDevice.PlayAudioPattern(pattern, duration);
        }

        private void _stopAudio_Click(object sender, EventArgs e)
        {
            _sirenOfShameDevice.StopAudioPattern();
        }

        private void _playLed_Click(object sender, EventArgs e)
        {
            var pattern = (LedPattern)_ledPattern.SelectedItem;
            var duration = new TimeSpan(0, 0, 0, (int)_ledDuration.Value);
            _sirenOfShameDevice.PlayLightPattern(pattern, duration);
        }

        private void _stopLed_Click(object sender, EventArgs e)
        {
            _sirenOfShameDevice.StopLightPattern();
        }
    }
}
