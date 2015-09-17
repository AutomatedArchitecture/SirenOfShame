using System;
using System.Windows.Forms;

namespace SirenOfShame.Configuration
{
    public partial class ResetReputation : Form
    {
        public ResetReputation()
        {
            InitializeComponent();
            Load += OnLoad;
        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            RecalculateDateEnabledness();
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void RecalculateDateEnabledness()
        {
            ResetDate.Enabled = ResetAndRebuildSinceDate.Checked;
        }

        private void ResetOnly_CheckedChanged(object sender, System.EventArgs e)
        {
            RecalculateDateEnabledness();
        }

        private void ResetAndRebuildFromStart_CheckedChanged(object sender, System.EventArgs e)
        {
            RecalculateDateEnabledness();
        }

        private void ResetAndRebuildSinceDate_CheckedChanged(object sender, System.EventArgs e)
        {
            RecalculateDateEnabledness();
        }
    }
}
