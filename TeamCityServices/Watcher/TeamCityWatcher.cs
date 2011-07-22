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
        private readonly Dictionary<string, TeamCityBuildStatus> _mostRecentPassOrFailBuildStatus = new Dictionary<string, TeamCityBuildStatus>();
        readonly List<TeamCityBuildStatus> _mostRecentInProgressBuildStatus = new List<TeamCityBuildStatus>();
        private List<BuildStatus>  _mostRecentBuildStatus = new List<BuildStatus>();
        private static Exception _lastError;
        private static ServerUnavailableException _serverUnavailableException;
        private int _outstandingPassOrFailBuildStatusRequests = 0;
        private int? _outstandingInProgressBuildStatusRequests = null;

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
            // only initiate new requests if there aren't any outstanding
            if (_outstandingPassOrFailBuildStatusRequests == 0)
            {
                _outstandingPassOrFailBuildStatusRequests = watchedBuildDefinitions.Length;
                _mostRecentInProgressBuildStatus.Clear();
                _outstandingInProgressBuildStatusRequests = null; // null = don't know On
                foreach (BuildDefinitionSetting watchedBuildDefinition in watchedBuildDefinitions)
                {
                    _service.GetBuildStatus(settings.Url,
                                            watchedBuildDefinition,
                                            settings.UserName,
                                            settings.Password,
                                            OnGetPassOrFailBuildStatusComplete,
                                            OnGetBuildStatusError);
                    _service.GetInProgressBuilds(settings.Url,
                                                 settings.UserName,
                                                 settings.Password,
                                                 OnGetOutstandingInProgressBuildCount,
                                                 OnGetInProgressBuildStatuses,
                                                 OnGetBuildStatusError);
                }
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

        private void OnGetOutstandingInProgressBuildCount(int outstandingInProgressBuildCount)
        {
            _outstandingInProgressBuildStatusRequests = outstandingInProgressBuildCount;
            if (AllOutstandingAsyncRequestsCompleted)
                MergeBuildStatuses();
        }

        /// <summary>
        /// ToDo: This is NOT thread safe!  Help!
        /// </summary>
        protected bool AllOutstandingAsyncRequestsCompleted
        {
            get { return _outstandingInProgressBuildStatusRequests == 0 && _outstandingPassOrFailBuildStatusRequests == 0; }
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

        private void OnGetInProgressBuildStatuses(TeamCityBuildStatus bs)
        {
            _serverUnavailableException = null; // if anything returns successfully clear the server unavailable exception
            _mostRecentInProgressBuildStatus.Add(bs);
            _outstandingInProgressBuildStatusRequests--;
            if (AllOutstandingAsyncRequestsCompleted)
                MergeBuildStatuses();
        }
        
        private void OnGetPassOrFailBuildStatusComplete(TeamCityBuildStatus bs)
        {
            _serverUnavailableException = null; // if anything returns successfully clear the server unavailable exception
            _mostRecentPassOrFailBuildStatus[bs.BuildDefinitionId] = bs;
            // todo: This is NOT thread safe
            _outstandingPassOrFailBuildStatusRequests--;
            if (AllOutstandingAsyncRequestsCompleted)
                MergeBuildStatuses();
        }

        private void MergeBuildStatuses()
        {
            _mostRecentBuildStatus = _mostRecentPassOrFailBuildStatus.Values.Select(i => i.ToBuildStatus()).ToList();
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
