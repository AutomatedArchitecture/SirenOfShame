using System;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class ConfigureSounds : FormBase
    {
        private readonly ILog _log = MyLogManager.GetLogger(typeof (ConfigureSounds));
        private readonly SirenOfShameSettings _settings;

        public ConfigureSounds(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();

            RefreshSoundList();
        }

        private void AddSound(object sender, EventArgs e)
        {
// ReSharper disable once LocalizableElement
            openFileDialog1.Filter = "WAV Files (*.wav)|*.wav";
            openFileDialog1.Multiselect = false;
            var dialogResult = openFileDialog1.ShowDialog(this);
            if (dialogResult == DialogResult.OK && openFileDialog1.SafeFileName != null)
            {
                var soundsDir = GetSoundsDirEnsureExists();
                var destinationFileName = Path.Combine(soundsDir, openFileDialog1.SafeFileName);
                File.Copy(openFileDialog1.FileName, destinationFileName, overwrite: true);
                
                Sound sound = new Sound
                {
                    Location = destinationFileName,
                    DisplayName = openFileDialog1.SafeFileName
                };

                _settings.Sounds.Add(sound);
                _settings.Save();

                var listViewItem = sound.AsListViewItem();
                _soundsList.Items.Add(listViewItem);
            }
        }

        private static string GetSoundsDirEnsureExists()
        {
            var sosAppDataFolder = SirenOfShameSettings.GetSosAppDataFolder();
            var soundsDir = Path.Combine(sosAppDataFolder, "Sounds");
            if (!Directory.Exists(soundsDir))
            {
                Directory.CreateDirectory(soundsDir);
            }
            return soundsDir;
        }

        private void SoundsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableDisableMenuButtons();
        }

        private void EnableDisableMenuButtons()
        {
            var anySoundsSelected = _soundsList.SelectedIndices.Count > 0;
            _delete.Enabled = anySoundsSelected;
            _preview.Enabled = anySoundsSelected;
        }

        private void WithFirstSelectedSound(Action<Sound> action)
        {
            if (_soundsList.SelectedItems.Count == 0) return;
            
            ListViewItem lvi = _soundsList.SelectedItems[0];
            var sound = _settings.Sounds.First(r => lvi.Tag == r);
            action(sound);
        }
        
        private void Delete_Click(object sender, EventArgs e)
        {
            WithFirstSelectedSound(DeleteSound);
        }

        private void DeleteSound(Sound sound)
        {
            try
            {
                var sosAppDataFolder = SirenOfShameSettings.GetSosAppDataFolder();
                var fileIsInOurFolder = sound.Location.StartsWith(sosAppDataFolder);
                // should be, but we can't assume someone hasn't tampered with the settings file
                if (fileIsInOurFolder)
                {
                    File.Delete(sound.Location);
                }
            }
            catch (Exception ex)
            {
                _log.Warn("Unable to delete file", ex);
            }
            _settings.Sounds.Remove(sound);
            RefreshSoundList();
            _settings.Save();
        }

        private void RefreshSoundList()
        {
            _soundsList.Items.Clear();
            var listViewItems = _settings.Sounds.Select(i => i.AsListViewItem()).ToArray();
            _soundsList.Items.AddRange(listViewItems);
            EnableDisableMenuButtons();
        }

        private void Preview_Click(object sender, EventArgs e)
        {
            WithFirstSelectedSound(PreviewSound);
        }

        private void PreviewSound(Sound sound)
        {
            SoundPlayer player = new SoundPlayer(sound.Location);
            try
            {
                player.Play();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
    }
}
