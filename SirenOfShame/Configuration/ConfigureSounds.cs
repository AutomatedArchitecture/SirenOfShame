using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class ConfigureSounds : FormBase
    {
        private readonly SirenOfShameSettings _settings;

        public ConfigureSounds(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();

            RefreshSoundList();
        }

        private void AddSound(object sender, System.EventArgs e)
        {
            var dialogResult = openFileDialog1.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                Sound sound = new Sound
                {
                    Location = openFileDialog1.SafeFileName,
                    Name = openFileDialog1.FileName
                };

                _settings.Sounds.Add(sound);
                _settings.Save();

                var listViewItem = sound.AsListViewItem();
                _soundsList.Items.Add(listViewItem);
            }
        }

        private void SoundsList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ShowHideDeleteButton();
        }

        private void ShowHideDeleteButton()
        {
            _delete.Enabled = _soundsList.SelectedIndices.Count > 0;
        }

        private void Delete_Click(object sender, System.EventArgs e)
        {
            if (_soundsList.SelectedItems.Count == 0) return;
            ListViewItem lvi = _soundsList.SelectedItems[0];
            var mapping = _settings.Sounds.First(r => lvi.Tag == r);
            _settings.Sounds.Remove(mapping);
            RefreshSoundList();
            _settings.Save();
        }

        private void RefreshSoundList()
        {
            _soundsList.Items.Clear();
            var listViewItems = _settings.Sounds.Select(i => i.AsListViewItem()).ToArray();
            _soundsList.Items.AddRange(listViewItems);
            ShowHideDeleteButton();
        }
    }
}
