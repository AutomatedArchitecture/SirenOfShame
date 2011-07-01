using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Device.SdCardFileSystem;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.SirenConfiguration
{
    public partial class ConfigureSirenDialog : FormBase
    {
        private static readonly ILog Log = MyLogManager.GetLogger(typeof(ConfigureSirenDialog));
        private readonly SirenOfShameSettings _settings;

        [Import(typeof(AudioFileService))]
        public AudioFileService AudioFileService { get; set; }

        [Import(typeof(LedFileService))]
        public LedFileService LedFileService { get; set; }

        [Import(typeof(ISirenOfShameDevice))]
        public ISirenOfShameDevice SirenOfShameDevice { get; set; }

        public ConfigureSirenDialog(SirenOfShameSettings settings)
        {
            IocContainer.Instance.Compose(this);
            InitializeComponent();
            _settings = settings;

            _audioPatterns.Items.Clear();
            foreach (var audioPattern in _settings.AudioPatterns)
            {
                AddAudioPattern(audioPattern);
            }

            _ledPatterns.Items.Clear();
            foreach (var ledPattern in _settings.LedPatterns)
            {
                AddLedPattern(ledPattern);
            }
        }

        private void _audioAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                AddAudioPattern(dlg.FileName);
            }
        }

        private void AddAudioPattern(string fileName)
        {
            ListViewItem item = new ListViewItem
            {
                Text = Path.GetFileNameWithoutExtension(fileName),
                Tag = fileName
            };
            _audioPatterns.Items.Add(item);
        }

        private void _ledAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "LED Pattern Files (*.sosled)|*.sosled|All Files (*.*)|*.*"
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                AddLedPattern(dlg.FileName);
            }
        }

        private void AddLedPattern(string fileName)
        {
            ListViewItem item = new ListViewItem
            {
                Text = Path.GetFileNameWithoutExtension(fileName),
                Tag = fileName
            };
            _ledPatterns.Items.Add(item);
        }

        private void _audioRemove_Click(object sender, EventArgs e)
        {
            foreach (var selectedItem in _audioPatterns.SelectedItems.Cast<ListViewItem>().ToList())
            {
                _audioPatterns.Items.Remove(selectedItem);
            }
        }

        private void _ledRemove_Click(object sender, EventArgs e)
        {
            foreach (var selectedItem in _ledPatterns.SelectedItems.Cast<ListViewItem>().ToList())
            {
                _ledPatterns.Items.Remove(selectedItem);
            }
        }

        private void _upload_Click(object sender, EventArgs e)
        {
            _progress.Value = 0;
            _progress.Visible = true;
            try
            {
                Cursor = Cursors.WaitCursor;
                IEnumerable<UploadAudioPattern> audioPatterns = GetAudioPatterns();
                IEnumerable<UploadLedPattern> ledPatterns = GetLedPatterns();
                SirenOfShameDevice.UploadCustomPatterns(audioPatterns, ledPatterns, progress =>
                {
                    Invoke(() => { _progress.Value = progress; });
                });
                _progress.Value = 100;
                _progress.Visible = false;
            }
            catch (Exception ex)
            {
                Log.Error("Could not upload", ex);
                ExceptionMessageBox.Show(this, "Error Uploading", "Could not upload patterns", ex);
                _progress.Visible = false;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private IEnumerable<UploadLedPattern> GetLedPatterns()
        {
            foreach (var item in _ledPatterns.Items.Cast<ListViewItem>())
            {
                var name = item.Text;
                var pattern = LedFileService.Read((string)item.Tag);
                yield return new UploadLedPattern(name, pattern);
            }
        }

        private IEnumerable<UploadAudioPattern> GetAudioPatterns()
        {
            foreach (var item in _audioPatterns.Items.Cast<ListViewItem>())
            {
                var name = item.Text;
                var stream = AudioFileService.Convert((string)item.Tag);
                yield return new UploadAudioPatternStream(name, stream);
            }
        }

        private void ConfigureSirenDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            _settings.AudioPatterns.Clear();
            foreach (var item in _audioPatterns.Items.Cast<ListViewItem>())
            {
                _settings.AudioPatterns.Add((string)item.Tag);
            }

            _settings.LedPatterns.Clear();
            foreach (var item in _ledPatterns.Items.Cast<ListViewItem>())
            {
                _settings.LedPatterns.Add((string)item.Tag);
            }

            _settings.Save();
        }
    }
}
