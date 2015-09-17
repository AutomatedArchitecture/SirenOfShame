using System;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class ResetReputation : Form
    {
        private readonly SirenOfShameSettings _settings;
        private readonly ILog _log = MyLogManager.GetLogger(typeof (ResetReputation));

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
            try
            {
                resetStatus.Visible = true;
                resetStatus.Text = "Resetting status";
                var reputationResetService = new ReputationResetService(_settings);
                reputationResetService.ResetOnly();
                if (ResetAndRebuildFromStart.Checked || ResetAndRebuildSinceDate.Checked)
                {
                    DateTime? since = ResetAndRebuildFromStart.Checked ? (DateTime?) null : ResetDate.Value;
                    reputationResetService.RebuildSince(since);
                }
                resetStatus.Text = "Reset completed successfully";
            }
            catch (Exception ex)
            {
                _log.Error("Error during reset", ex);
                resetStatus.Text = "Error occurred, check the logs. " + ex.Message;
            }
        }
    }
}
