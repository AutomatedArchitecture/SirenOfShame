using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Services
{
    public class ReputationResetService
    {
        private readonly SirenOfShameSettings _settings;
        private readonly SosDb _sosDb = new SosDb();
        private readonly ILog _log = MyLogManager.GetLogger(typeof (ReputationResetService));

        public ReputationResetService(SirenOfShameSettings settings)
        {
            _settings = settings;
        }

        public void ResetOnly()
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

        }

        public void RebuildSince(DateTime? since, Action<string> reportProgress)
        {
            try
            {
                var buildStatuses = _sosDb.ReadAll();
                var sortedBuildStatuses = buildStatuses
                    .Where(buildStatus => since == null || buildStatus.StartedTime > since)
                    .OrderByDescending(buildStatus => buildStatus.LocalStartTime)
                    .ToList();

                var totalBuilds = sortedBuildStatuses.Count;

                var rulesEngine = new RulesEngine(_settings)
                {
                    DisableSosOnline = true,
                    DisableWritingToSosDb = true
                };
                int i = 0;
                foreach (var buildStatus in sortedBuildStatuses)
                {
                    rulesEngine.ExecuteNewBuilds(new List<BuildStatus> {buildStatus});
                    if (i%10 == 0)
                    {
                        reportProgress(string.Format("Processing {0}/{1}", i, totalBuilds));
                    }
                    i++;
                }
                _settings.Save();
                reportProgress("Completed reset successfully");
            }
            catch (Exception ex)
            {
                _log.Error("Error in sync", ex);
                reportProgress("Error, check the logs. " + ex.Message);
            }
        }
    }
}
