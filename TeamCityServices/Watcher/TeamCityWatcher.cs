using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private static ServerUnavailableException _serverUnavailableException;

        public TeamCityWatcher(SirenOfShameSettings settings, TeamCityCiEntryPoint teamCityCiEntryPoint)
            : base(settings)
        {
            _teamCityCiEntryPoint = teamCityCiEntryPoint;
        }

        protected override IEnumerable<BuildStatus> GetBuildStatus()
        {
            // 1. Perform async request
            var settings = Settings.FindAddSettings(_teamCityCiEntryPoint.Name);
            var watchedBuildDefinitions = GetAllWatchedBuildDefinitions().ToArray();
            foreach (BuildDefinitionSetting watchedBuildDefinition in watchedBuildDefinitions)
            {
                BuildDefinitionSetting definition = watchedBuildDefinition;
                _service.GetBuildStatus(settings.Url, watchedBuildDefinition.Id, settings.UserName, settings.Password, GetBuildStatusComplete(definition), OnGetBuildStatusError);
            }

            // 2. Return result of any previous call to GetBuildStatus
            if (_serverUnavailableException != null)
            {
                throw _serverUnavailableException;
            }
            if (_lastError != null)
            {
                var ex = _lastError;
                _lastError = null;
                throw ex;
            }

            return _mostRecentBuildStatus;
        }

        private static void OnGetBuildStatusError(Exception ex)
        {
            if (
                (typeof(WebException).IsAssignableFrom(ex.GetType())) &&
                ex.Message.StartsWith("The remote name could not be resolved:")
                )
            {
                _lastError = null;
                _serverUnavailableException = new ServerUnavailableException();
            }
            else
            {
                _serverUnavailableException = null;
                _lastError = ex;
            }
        }

        private TeamCityService.GetBuildStatusCompleteDelegate GetBuildStatusComplete(BuildDefinitionSetting definition)
        {
            return bs =>
            {
                _serverUnavailableException = null; // if anything returns successfully clear the server unavailable exception
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
                mostRecentBuildStatus.FinishedTime = bs.FinishedTime;
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
