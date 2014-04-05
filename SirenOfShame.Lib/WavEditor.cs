using System;
using System.IO;
using System.Media;
using System.Windows.Forms;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib
{
    public partial class WavEditor : Form
    {
        private readonly AudioPatternSetting _setting;
        private readonly AudioFileService _audioFileService = new AudioFileService();
        private readonly SoundPlayer _player;
        private const int SAMPLE_RATE = SirenOfShameDevice.AudioSampleRate;

        public WavEditor(AudioPatternSetting setting)
        {
            InitializeComponent();
            _setting = setting;
            _player = new SoundPlayer();
            MouseWheel += WavEditor_MouseWheel;
            if (_setting != null)
            {
                var data = File.ReadAllBytes(_setting.FileName);
                _viewer.Data = data;
            }
        }

        void WavEditor_MouseWheel(object sender, MouseEventArgs e)
        {
            _viewer.OnMouseWheel(sender, e);
        }

        private void _crop_Click(object sender, EventArgs e)
        {
            _viewer.Data = GetSelectedData();
            _viewer.SelectionStart = null;
            _viewer.SelectionEnd = null;
        }

        private byte[] GetSelectedData()
        {
            if (_viewer.SelectionStart != null && _viewer.SelectionEnd != null)
            {
                byte[] newData = new byte[_viewer.SelectionEnd.Value - _viewer.SelectionStart.Value];
                Array.Copy(_viewer.Data, _viewer.SelectionStart.Value, newData, 0, newData.Length);
                return newData;
            }
            return _viewer.Data;
        }

        private void _save_Click(object sender, EventArgs e)
        {
            File.Delete(_setting.FileName);
            File.WriteAllBytes(_setting.FileName, _viewer.Data);
        }

        private void _play_Click(object sender, EventArgs e)
        {
            byte[] data = _audioFileService.ConvertToWav(GetSelectedData());
            using (var stream = new MemoryStream(data))
            {
                _player.Stream = stream;
                _player.Load();
                _player.Play();
            }
        }

        private void _playTimes5_Click(object sender, EventArgs e)
        {
            byte[] data = _audioFileService.ConvertToWav(ArrayHelpers.Repeat(GetSelectedData(), 5));
            using (var stream = new MemoryStream(data))
            {
                _player.Stream = stream;
                _player.Load();
                _player.Play();
            }
        }

        private void _stop_Click(object sender, EventArgs e)
        {
            _player.Stop();
        }

        private void _appendSilence_Click(object sender, EventArgs e)
        {
            AppendTimeDialog dlg = new AppendTimeDialog();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                int samplesToAdd = (int)(dlg.TimeSpan.TotalSeconds * SAMPLE_RATE);
                byte[] newData = new byte[_viewer.Data.Length + samplesToAdd];
                Array.Copy(_viewer.Data, 0, newData, 0, _viewer.Data.Length);
                for (int i = _viewer.Data.Length; i < newData.Length; i++)
                {
                    newData[i] = 0xff / 2;
                }
                _viewer.Data = newData;
            }
        }
    }
}
