using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using SoxLib.Helpers;
using log4net;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Device.SdCardFileSystem;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib
{
    public partial class ConfigureSirenDialog : FormBase
    {
        private static readonly ILog Log = MyLogManager.GetLogger(typeof(ConfigureSirenDialog));
        private readonly SirenOfShameSettings _settings;
        private readonly ISirenOfShameDevice _sirenOfShameDevice;
        private readonly AudioFileService _audioFileService = new AudioFileService();
        private readonly LedFileService _ledFileService = new LedFileService();

        public ConfigureSirenDialog(SirenOfShameSettings settings, ISirenOfShameDevice sirenOfShameDevice)
        {
            InitializeComponent();
            _sirenOfShameDevice = sirenOfShameDevice;
            _sirenOfShameDevice.UploadProgress += (sender, args) => Invoke(() => { _progress.Value = args.Value; });
            // todo: Convert to async/await
            _sirenOfShameDevice.UploadCompleted += UploadCompleted;
            _settings = settings;

            _audioPatterns.Items.Clear();
            var audioPatternSettings = _settings.GetAllAudioPatternSettingsAlsoOnDevice(_sirenOfShameDevice);
            foreach (var audioPattern in audioPatternSettings)
            {
                AddOrUpdateAudioPattern(audioPattern);
            }

            _ledPatterns.Items.Clear();
            foreach (var ledPattern in _settings.LedPatterns)
            {
                AddLedPattern(ledPattern);
            }
        }

        private void UploadCompleted(object sender, UploadCompletedEventHandlerArgs args)
        {
            Invoke(() =>
                {
                    _progress.Value = 100;
                    _progress.Visible = false;
                    if (args.Exception != null)
                    {
                        OnUploadError(args.Exception);
                    }
                });
        }

        private void AudioAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var fileName = dlg.FileName;
                    var setting = new AudioPatternSetting
                    {
                        FileName = fileName,
                        Name = Path.GetFileNameWithoutExtension(fileName),
                    };
                    AddOrUpdateAudioPattern(setting);
                }
                catch (Exception ex)
                {
                    ExceptionMessageBox.Show(this, "Error Adding Audio Pattern", "Could not add audio pattern.", ex);
                }
            }
        }

        private void AddOrUpdateAudioPattern(AudioPatternSetting setting)
        {
            string lengthStr = _audioFileService.GetLength(setting.FileName).ToString(@"mm\:ss\.fff");
            ListViewItem item = _audioPatterns.Items.Cast<ListViewItem>().FirstOrDefault(lvi => string.Equals(((AudioPatternSetting)lvi.Tag).FileName, setting.FileName, StringComparison.InvariantCultureIgnoreCase));
            if (item != null)
            {
                item.Text = setting.Name;
                item.SubItems[1].Text = lengthStr;
            }
            else
            {
                item = new ListViewItem
                {
                    Text = setting.Name,
                    Tag = setting
                };
                item.SubItems.Add(lengthStr);
                _audioPatterns.Items.Add(item);
            }
        }

        private void _ledAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "LED Pattern Files (*.sosled)|*.sosled|All Files (*.*)|*.*"
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                string outputFileName = Path.Combine(Path.GetDirectoryName(_settings.FileName), Path.GetFileNameWithoutExtension(dlg.FileName) + ".sosled");
                File.Delete(outputFileName);
                File.Copy(dlg.FileName, outputFileName);
                var setting = new LedPatternSetting
                {
                    FileName = outputFileName,
                    Name = Path.GetFileNameWithoutExtension(outputFileName)
                };
                AddLedPattern(setting);
            }
        }

        private void AddLedPattern(LedPatternSetting setting)
        {
            ListViewItem item = new ListViewItem
            {
                Text = setting.Name,
                Tag = setting
            };
            item.SubItems.Add(_ledFileService.GetLength(setting.FileName).ToString(@"mm\:ss\.fff"));
            _ledPatterns.Items.Add(item);
        }

        private void _upload_Click(object sender, EventArgs e)
        {
            _progress.Value = 0;
            _progress.Visible = true;
            try
            {
                var audioPatterns = GetAudioPatterns().ToList();
                var ledPatterns = GetLedPatterns().ToList();
                _sirenOfShameDevice.UploadCustomPatternsAsync(_settings, audioPatterns, ledPatterns);
            }
            catch (Exception ex)
            {
                OnUploadError(ex);
            }
        }

        private void OnUploadError(Exception ex)
        {
            Log.Error("Could not upload", ex);
            ExceptionMessageBox.Show(this, "Error Uploading", "Could not upload patterns", ex);
            _progress.Visible = false;
        }

        private IEnumerable<UploadLedPattern> GetLedPatterns()
        {
            foreach (var item in _ledPatterns.Items.Cast<ListViewItem>())
            {
                var setting = (LedPatternSetting)item.Tag;
                var name = setting.Name;
                var pattern = _ledFileService.Read(setting.FileName);
                yield return new UploadLedPattern(name, pattern, setting.FileName);
            }
        }

        private IEnumerable<AudioPatternSetting> GetAudioPatterns()
        {
            return _audioPatterns.Items.Cast<ListViewItem>().Select(item => (AudioPatternSetting)item.Tag);
        }

        private void ConfigureSirenDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void _audioPatterns_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var lvi = _audioPatterns.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
                if (lvi != null)
                {
                    _audioContextMenu.Show(_audioPatterns, e.Location);
                }
            }
        }

        private void _audioPatterns_MouseDown(object sender, MouseEventArgs e)
        {
            var lvi = _audioPatterns.GetItemAt(e.X, e.Y);
            if (lvi != null)
            {
                lvi.Selected = true;
            }
        }

        private void _audioRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to remove this audio pattern?", "Remove Audio Pattern", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var lvi = _audioPatterns.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
                if (lvi != null)
                {
                    _audioPatterns.Items.Remove(lvi);
                }
            }
        }

        private void _audioPatterns_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                return;
            }
            var lvi = _audioPatterns.Items[e.Item];
            AudioPatternSetting setting = (AudioPatternSetting)lvi.Tag;
            setting.Name = e.Label;
            e.CancelEdit = true;
            lvi.Text = setting.Name;
        }

        private void _audioRename_Click(object sender, EventArgs e)
        {
            var lvi = _audioPatterns.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (lvi != null)
            {
                lvi.BeginEdit();
            }
        }

        private void _audioPlay_Click(object sender, EventArgs e)
        {
            var lvi = _audioPatterns.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (lvi != null)
            {
                var setting = (AudioPatternSetting)lvi.Tag;
                string fileName = _audioFileService.ConvertToWav(setting.FileName);
                var player = new SoundPlayer(fileName);
                player.PlayLooping();
                Thread.Sleep(5000);
                player.Stop();
            }
        }

        private void _audioEdit_Click(object sender, EventArgs e)
        {
            var lvi = _audioPatterns.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (lvi != null)
            {
                var setting = (AudioPatternSetting)lvi.Tag;
                new WavEditor(setting).ShowDialog(this);
                AddOrUpdateAudioPattern(setting);
            }
        }

        private void _ledPatterns_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                return;
            }
            var lvi = _ledPatterns.Items[e.Item];
            LedPatternSetting setting = (LedPatternSetting)lvi.Tag;
            setting.Name = e.Label;
            e.CancelEdit = true;
            lvi.Text = setting.Name;
        }

        private void _ledPatterns_MouseDown(object sender, MouseEventArgs e)
        {
            var lvi = _ledPatterns.GetItemAt(e.X, e.Y);
            if (lvi != null)
            {
                lvi.Selected = true;
            }
        }

        private void _ledPatterns_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var lvi = _ledPatterns.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
                if (lvi != null)
                {
                    _ledContextMenu.Show(_ledPatterns, e.Location);
                }
            }
        }

        private void _ledPlay_Click(object sender, EventArgs e)
        {
            var lvi = _ledPatterns.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (lvi != null)
            {
                var setting = (LedPatternSetting)lvi.Tag;
                if (_sirenOfShameDevice.TryConnect())
                {
                    var rows = _ledFileService.GetRows(File.ReadAllText(setting.FileName)).ToList();
                    for (int i = 0; i < 5; i++)
                    {
                        foreach (var row in rows)
                        {
                            _sirenOfShameDevice.ManualControl(new ManualControlData
                            {
                                Siren = false,
                                Led0 = row[0],
                                Led1 = row[1],
                                Led2 = row[2],
                                Led3 = row[3],
                                Led4 = row[4]
                            });
                            Thread.Sleep(100);
                        }
                    }

                    _sirenOfShameDevice.ManualControl(new ManualControlData
                    {
                        Siren = false,
                        Led0 = 0,
                        Led1 = 0,
                        Led2 = 0,
                        Led3 = 0,
                        Led4 = 0
                    });
                }
            }
        }

        private void _ledRename_Click(object sender, EventArgs e)
        {
            var lvi = _ledPatterns.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (lvi != null)
            {
                lvi.BeginEdit();
            }
        }

        private void _ledRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to remove this LED pattern?", "Remove LED Pattern", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var lvi = _ledPatterns.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
                if (lvi != null)
                {
                    _ledPatterns.Items.Remove(lvi);
                }
            }
        }
    }
}
