using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace TeamCityServices.Watcher
{
    public class TeamCityWatcher : WatcherBase
    {
        private readonly TeamCityCiEntryPoint _teamCityCiEntryPoint;
        private readonly TeamCityService _service = new TeamCityService();
        private readonly List<BuildStatus> _mostRecentBuildStatus = new List<BuildStatus>();
        private static Exception _lastError;

        public TeamCityWatcher(SirenOfShameSettings settings, TeamCityCiEntryPoint teamCityCiEntryPoint)
            : base(settings)
        {
            _teamCityCiEntryPoint = teamCityCiEntryPoint;
        }

        protected override IEnumerable<BuildStatus> GetBuildStatus()
        {
            if (_lastError != null)
            {
                throw _lastError;
            }

            var settings = Settings.FindAddSettings(_teamCityCiEntryPoint.Name);
            var watchedBuildDefinitions = GetAllWatchedBuildDefinitions().ToArray();
            foreach (BuildDefinitionSetting watchedBuildDefinition in watchedBuildDefinitions)
            {
                BuildDefinitionSetting definition = watchedBuildDefinition;
                _service.GetBuildStatus(settings.Url, watchedBuildDefinition.Id, settings.UserName, settings.Password, GetBuildStatusComplete(definition), OnGetBuildStatusError);
            }
            return _mostRecentBuildStatus;
        }

        private static void OnGetBuildStatusError(Exception ex)
        {
            _lastError = ex;
        }

        private TeamCityService.GetBuildStatusCompleteDelegate GetBuildStatusComplete(BuildDefinitionSetting definition)
        {
            return bs =>
            {
                var mostRecentBuildStatus = _mostRecentBuildStatus.FirstOrDefault(mrbs => mrbs.Id == bs.BuildDefinitionId);
                if (mostRecentBuildStatus == null)
                {
                    mostRecentBuildStatus = new BuildStatus
                    {
                        Id = bs.BuildDefinitionId,
                        Name = definition.Name
                    };
                    _mostRecentBuildStatus.Add(mostRecentBuildStatus);
                }
                mostRecentBuildStatus.BuildStatusEnum = bs.BuildStatus;
                mostRecentBuildStatus.RequestedBy = bs.RequestedBy;
                mostRecentBuildStatus.StartedTime = bs.StartedTime;
            };
        }

        private IEnumerable<BuildDefinitionSetting> GetAllWatchedBuildDefinitions()
        {
            var activeBuildDefinitionSettings = Settings.BuildDefinitionSettings.Where(bd => bd.Active && bd.BuildServer == _teamCityCiEntryPoint.Name && bd.BuildServer == Settings.ServerType);
            return activeBuildDefinitionSettings;
        }

        public override void StopWatching()
        {
        }

        public override void Dispose()
        {
        }
    }
}
