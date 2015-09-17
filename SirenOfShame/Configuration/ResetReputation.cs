using System;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class ResetReputation : Form
    {
        private readonly SirenOfShameSettings _settings;

        public ResetReputation(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();
            Load += OnLoad;
        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            RecalculateDateEnabledness();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RecalculateDateEnabledness()
        {
            ResetDate.Enabled = ResetAndRebuildSinceDate.Checked;
            var anyCheckboxChecked = ResetAndRebuildSinceDate.Checked ||
                                     ResetAndRebuildFromStart.Checked ||
                                     ResetOnly.Checked;
            ResetButton.Enabled = anyCheckboxChecked;
        }

        private void ResetOnly_CheckedChanged(object sender, EventArgs e)
        {
            RecalculateDateEnabledness();
        }

        private void ResetAndRebuildFromStart_CheckedChanged(object sender, EventArgs e)
        {
            RecalculateDateEnabledness();
        }

        private void ResetAndRebuildSinceDate_CheckedChanged(object sender, EventArgs e)
        {
            RecalculateDateEnabledness();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            _settings.Backup();
        }
    }
}
