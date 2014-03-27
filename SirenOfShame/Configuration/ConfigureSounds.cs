using System;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class ConfigureSounds : FormBase
    {
        private readonly SirenOfShameSettings _settings;
        readonly SoundService _soundService = new SoundService();

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
                var sound = _soundService.AddSound(_settings, openFileDialog1.FileName, openFileDialog1.SafeFileName);
                var listViewItem = sound.AsListViewItem();
                _soundsList.Items.Add(listViewItem);
            }
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
            _soundService.DeleteSound(sound);
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
            WithFirstSelectedSound(_soundService.Play);
        }
    }
}
