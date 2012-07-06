using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace SirenOfShame.Configuration
{
    public partial class FindOldAchievements : Form
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(FindOldAchievements));
        private readonly SirenOfShameSettings _settings;

        public FindOldAchievements(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();
        }

        private void OkClick(object sender, EventArgs e)
        {
            if (_findOldAchievements.Checked)
                RecalculateAchievements();
            Close();
            if (_configureSosOnline.Checked)
                ShowConfigureSosOnline();
        }

        private void ShowConfigureSosOnline()
        {
            var dialog = new ConfigureSosOnline(_settings);
            dialog.ShowDialog();
        }

        private void RecalculateAchievements()
        {
            var sosDb = new SosDb();
            var allSettings = sosDb
                .ReadAll(_settings.GetAllActiveBuildDefinitions())
                .OrderBy(i => i.StartedTime)
                .ToList();
            foreach (var person in _settings.People)
            {
                person.TotalBuilds = 0;
                person.FailedBuilds = 0;
                person.Achievements.Clear();
                person.CumulativeBuildTime = 0;
            }
            _status.Visible = true;
            var allActiveBuildDefinitionsOrderedChronoligically = new List<BuildStatus>();
            var buildCount = allSettings.Count;
            progressBar1.Maximum = buildCount;
            foreach (var buildStatus in allSettings)
            {
                _status.Text = string.Format("Processing {0:d}", buildStatus.StartedTime);
                progressBar1.Value++;
                var person = _settings.People.FirstOrDefault(i => i.RawName == buildStatus.RequestedBy);
                if (person == null)
                {
                    _log.Error("Could not find " + buildStatus.RequestedBy);
                    continue;
                }
                person.TotalBuilds++;
                if (buildStatus.BuildStatusEnum == BuildStatusEnum.Broken)
                    person.FailedBuilds++;
                allActiveBuildDefinitionsOrderedChronoligically.Add(buildStatus);
                var newAchievements = person
                    .CalculateNewAchievements(_settings, buildStatus, allActiveBuildDefinitionsOrderedChronoligically)
                    .ToList();

                if (!newAchievements.Any()) continue;
                
                person.AddAchievements(newAchievements);
                foreach (var achievementLookup in newAchievements)
                {
                    NewAchievement.ShowForm(_settings, achievementLookup, person, this);
                }
            }
            _settings.Save();
        }

        public static void TryFindOldAchievements(SirenOfShameSettings settings)
        {
            FindOldAchievements form = new FindOldAchievements(settings);
            form.Show();
        }
    }
}
