using System;
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
        private ILog _log = MyLogManager.GetLogger(typeof (ConfigureSounds));
        private readonly SirenOfShameSettings _settings;

        public ConfigureSounds(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();

            RefreshSoundList();
        }

        private void AddSound(object sender, System.EventArgs e)
        {
            openFileDialog1.Filter = "WAV Files (*.wav)|*.wav";
            openFileDialog1.Multiselect = false;
            var dialogResult = openFileDialog1.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                Sound sound = new Sound
                {
                    Location = openFileDialog1.FileName,
                    Name = openFileDialog1.SafeFileName
                };

                _settings.Sounds.Add(sound);
                _settings.Save();

                var listViewItem = sound.AsListViewItem();
                _soundsList.Items.Add(listViewItem);
            }
        }

        private void SoundsList_SelectedIndexChanged(object sender, System.EventArgs e)
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
        
        private void Delete_Click(object sender, System.EventArgs e)
        {
            WithFirstSelectedSound(sound =>
            {
                _settings.Sounds.Remove(sound);
                RefreshSoundList();
                _settings.Save();
            });
        }

        private void RefreshSoundList()
        {
            _soundsList.Items.Clear();
            var listViewItems = _settings.Sounds.Select(i => i.AsListViewItem()).ToArray();
            _soundsList.Items.AddRange(listViewItems);
            EnableDisableMenuButtons();
        }

        private void _preview_Click(object sender, System.EventArgs e)
        {
            WithFirstSelectedSound(sound =>
            {
                SoundPlayer player = new SoundPlayer(sound.Location);
                try
                {
                    player.Play();
                }
                catch (InvalidOperationException ex)
                {
                    _log.Error(ex);
                }
            });
        }
    }
}
