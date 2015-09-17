using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Configuration
{
    public partial class ResetReputation : Form
    {
        private readonly SirenOfShameSettings _settings;
        public SosDb SosDb = new SosDb();

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
            foreach (var person in _settings.People)
            {
                person.TotalBuilds = 0;
                person.FailedBuilds = 0;
                person.Achievements = new List<AchievementSetting>();
                person.CumulativeBuildTime = 0;
                person.CurrentBuildRatio = 0;
                person.CurrentSuccessInARow = 0;
                person.LowestBuildRatioAfter50Builds = 0;
                person.MaxBuildsInOneDay = 0;
                person.NumberOfTimesFixedSomeoneElsesBuild = 0;
                person.NumberOfTimesPerformedBackToBackBuilds = 0;
            }
            _settings.Save();

            if (ResetAndRebuildFromStart.Checked || ResetAndRebuildSinceDate.Checked)
            {
                SosDb.GetMostRecentNewsItems(_settings, OnGetNewsItems);
            }
        }

        private void OnGetNewsItems(IList<NewNewsItemEventArgs> newNewsItemEventArgses)
        {
            foreach (var newNewsItemEventArgse in newNewsItemEventArgses)
            {
                
            }
        }
    }
}
