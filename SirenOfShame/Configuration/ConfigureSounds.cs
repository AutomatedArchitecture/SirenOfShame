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
        }

        private void AddSoundClick(object sender, System.EventArgs e)
        {
            var dialogResult = openFileDialog1.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                Sound sound = new Sound
                {
                    Location = openFileDialog1.SafeFileName,
                    Name = openFileDialog1.FileName
                };

                var listViewItem = sound.AsListViewItem();
                _soundsList.Items.Add(listViewItem);
            }
        }
    }
}
