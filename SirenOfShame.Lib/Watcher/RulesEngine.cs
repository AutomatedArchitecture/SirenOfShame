using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Dto;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Util;
using Timer = System.Windows.Forms.Timer;

namespace SirenOfShame.Lib.Watcher
{
    public class RulesEngine
    {
        private struct ChangedBuildStatusesAndTheirPreviousState
        {
            public BuildStatus ChangedBuildStatus { get; set; }
            public BuildStatusEnum? PreviousWorkingOrBrokenBuildStatus { get; set; }
            public BuildStatusEnum? PreviousBuildStatus { get; set; }
        }

        public const int NEWS_ITEMS_TO_GET_ON_STARTUP = 10;
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(RulesEngine));
        private IDictionary<string, BuildStatus> PreviousWorkingOrBrokenBuildStatus { get; set; }
        private IDictionary<string, BuildStatus> PreviousBuildStatus { get; set; }
        
        private Thread _watcherThread;
        readonly Timer _timer = new Timer();
        private readonly SosOnlineService _sosOnlineService = new SosOnlineService();

        private readonly SirenOfShameSettings _settings;
        private readonly IList<WatcherBase> _watchers = new List<WatcherBase>();
        public SosDb SosDb = new SosDb();
        public bool DisableSosOnline { get; set; }
        public bool DisableWritingToSosDb { get; set; }

        public event UpdateStatusBarEvent UpdateStatusBar;
        public event StatusChangedEvent RefreshStatus;
        public event StatsChangedEvent StatsChanged;
        public event TrayNotifyEvent TrayNotify;
        public event ModalDialogEvent ModalDialog;
        public event SetAudioEvent SetAudio;
        public event SetLightsEvent SetLights;
        public event SetTrayIconEvent SetTrayIcon;
        public event NewAlertEvent NewAlert;
        public event PlayWindowsAudioEvent PlayWindowsAudio;
        public event NewAchievementEvent NewAchievement;
        public event NewNewsItemEvent NewNewsItem;
        public event NewUserEvent NewUser;

        private void InvokeNewUser(string requestedBy)
        {
            var handler = NewUser;
            if (handler != null) handler(this, new NewUserEventArgs
            {
                RawName = requestedBy
            });
        }

        private void InvokeNewNewsItem(NewNewsItemEventArgs args, bool newsIsBothLocalAndNew)
        {
            var newNewsItem = NewNewsItem;
            if (newsIsBothLocalAndNew)
                SosDb.ExportNewNewsItem(args);
            if (newNewsItem != null) newNewsItem(this, args);
        }

        private void InvokeNewAchievement(PersonSetting person, List<AchievementLookup> achievements)
        {
            var newAchievement = NewAchievement;
            if (newAchievement != null) newAchievement(this, new NewAchievementEventArgs { Person = person, Achievements = achievements });
            achievements.ForEach(i => InvokeNewNewsItem(i.AsNewNewsItem(person), newsIsBothLocalAndNew: true));
        }

        public void InvokePlayWindowsAudio(string location)
        {
            if (_settings.Mute) return;

            PlayWindowsAudioEvent playWindowsAudio = PlayWindowsAudio;
            if (playWindowsAudio != null) playWindowsAudio(this, new PlayWindowsAudioEventArgs { Location = location });
        }

        private void InvokeSetTrayIcon(TrayIcon trayIcon)
        {
            SetTrayIconEvent setTrayIcon = SetTrayIcon;
            if (setTrayIcon != null) setTrayIcon(this, new SetTrayIconEventArgs { TrayIcon = trayIcon });
        }

        private void InvokeNewAlert(NewAlertEventArgs args)
        {
            NewAlertEvent newAlert = NewAlert;
            if (newAlert != null) newAlert(this, args);
        }

        public void InvokeTrayNotify(ToolTipIcon tipIcon, string title, string tipText)
        {
            TrayNotifyEvent handler = TrayNotify;
            if (handler != null) handler(this, new TrayNotifyEventArgs { TipIcon = tipIcon, TipText = tipText, Title = title });
        }

        public RulesEngine(SirenOfShameSettings settings)
        {
            DisableSosOnline = false;
            DisableWritingToSosDb = false;
            ResetPreviousWorkingOrBrokenStatuses();
            _settings = settings;
            _timer.Interval = 1000;
            _timer.Tick += TimerTick;
        }

        private bool _serverPreviouslyUnavailable;

        private void BuildWatcherServerUnavailable(object sender, ServerUnavailableEventArgs args)
        {
            InvokeUpdateStatusBar("Build server unavailable, attempting to reconnect", args.Exception);
            SetStatusUnknown();
            // only notify that it was unavailable once
            if (_serverPreviouslyUnavailable)
            {
                return;
            }

            InvokeTrayNotify(ToolTipIcon.Error, "Build Server Unavailable", "The connection will be restored when possible.");
            ResetPreviousWorkingOrBrokenStatuses();
            _serverPreviouslyUnavailable = true;
        }

        private void ResetPreviousWorkingOrBrokenStatuses()
        {
            PreviousWorkingOrBrokenBuildStatus = new Dictionary<string, BuildStatus>();
            PreviousBuildStatus = new Dictionary<string, BuildStatus>();
            _previousBuildStatuses = new BuildStatus[] { };
        }

        private BuildStatus[] _previousBuildStatuses = { };
        private bool _restarting = false;

        private void InvokeUpdateStatusBar(string statusText, Exception exception = null)
        {
            string datedStatusText = null;
            if (!string.IsNullOrEmpty(statusText))
            {
                datedStatusText = string.Format("{0:G} - {1}", DateTime.Now, statusText);
            }
            var updateStatusBar = UpdateStatusBar;
            if (updateStatusBar == null) return;
            updateStatusBar(this, new UpdateStatusBarEventArgs { StatusText = datedStatusText, Exception = exception });
        }

        private void BuildWatcherStatusChecked(object sender, StatusCheckedEventArgsArgs args)
        {
            ExecuteNewBuilds(args.BuildStatuses);
        }

        private void StoppedWatching(object sender, StoppedWatchingEventArgs args)
        {
            _restarting = true;
        }

        public void ExecuteNewBuilds(IList<BuildStatus> newBuildStatuses)
        {
            try
            {
                ApplyUserMappings(newBuildStatuses);
                SendCiServerConnectedEvents();
                TryToGetAndSendNewSosOnlineAlerts();
                var allBuildStatuses = BuildStatusUtil.Merge(_previousBuildStatuses, newBuildStatuses);
                var changedBuildStatuses = GetChangedBuildStatuses(allBuildStatuses);
                if (!changedBuildStatuses.Any())
                {
                    if (_restarting)
                        InvokeRefreshStatus(allBuildStatuses);
                    return;
                }
                InvokeSetTrayIcon(changedBuildStatuses);
                InvokeRefreshStatusIfAnythingChanged(allBuildStatuses, changedBuildStatuses);
                AddAnyNewPeopleToSettings(changedBuildStatuses);
                UpdateBuildNamesInSettingsIfAnyChanged(changedBuildStatuses);
                var changedBuildStatusesAndTheirPreviousState = GetChangedBuildStatusesAndTheirPreviousState(changedBuildStatuses);
                FireApplicableRulesEngineEvents(changedBuildStatusesAndTheirPreviousState);
                WriteNewBuildsToSosDb(changedBuildStatusesAndTheirPreviousState);
                NotifyIfNewAchievements(changedBuildStatuses);
                InvokeStatsChanged(changedBuildStatuses);
                SyncNewBuildsToSos(changedBuildStatuses);
                InvokeNewNewsItemIfAny(changedBuildStatusesAndTheirPreviousState);
                CacheBuildStatuses(changedBuildStatuses);
            }
            finally
            {
                _restarting = false;
            }
        }

        private void ApplyUserMappings(IList<BuildStatus> buildStatuses)
        {
            foreach (var buildStatus in buildStatuses)
            {
                string requestedBy = buildStatus.RequestedBy;
                var userMapping = _settings.UserMappings.FirstOrDefault(i => i.WhenISee == requestedBy);
                bool userMappingExistsForThisUser = userMapping != null;
                if (userMappingExistsForThisUser)
                {
                    buildStatus.RequestedBy = userMapping.PretendItsActually;
                }
            }
        }

        private void InvokeNewNewsItemIfAny(IEnumerable<ChangedBuildStatusesAndTheirPreviousState> changedBuildStatuses)
        {
            changedBuildStatuses
                .Where(i => i.PreviousWorkingOrBrokenBuildStatus != null && !string.IsNullOrEmpty(i.ChangedBuildStatus.RequestedBy))
// ReSharper disable PossibleInvalidOperationException
                .Select(i => i.ChangedBuildStatus.AsNewsItemEventArgs(i.PreviousWorkingOrBrokenBuildStatus.Value, _settings))
// ReSharper restore PossibleInvalidOperationException
                .ToList()
                .ForEach(i => InvokeNewNewsItem(i, newsIsBothLocalAndNew: true));
        }

        private void WriteNewBuildsToSosDb(IEnumerable<ChangedBuildStatusesAndTheirPreviousState> changedBuildStatusesAndTheirPreviousState)
        {
            var previouslyWorkingOrBrokenBuilds = changedBuildStatusesAndTheirPreviousState
                .Where(i => i.ChangedBuildStatus.IsWorkingOrBroken() && i.PreviousWorkingOrBrokenBuildStatus != null)
                .ToList();
            previouslyWorkingOrBrokenBuilds.ForEach(i => SosDb.Write(i.ChangedBuildStatus, _settings, DisableWritingToSosDb));
        }

        private void FireApplicableRulesEngineEvents(IEnumerable<ChangedBuildStatusesAndTheirPreviousState> changedBuildStatusesAndTheirPreviousState)
        {
            foreach (var buildStatus in changedBuildStatusesAndTheirPreviousState)
            {
                buildStatus.ChangedBuildStatus.FireApplicableRulesEngineEvents(buildStatus.PreviousWorkingOrBrokenBuildStatus,
                                                                               buildStatus.PreviousBuildStatus,
                                                                               this,
                                                                               _settings.Rules);
            }
        }

        private List<ChangedBuildStatusesAndTheirPreviousState> GetChangedBuildStatusesAndTheirPreviousState(IEnumerable<BuildStatus> changedBuildStatuses)
        {
            var result = changedBuildStatuses
                .Select(changedBuildStatus => new ChangedBuildStatusesAndTheirPreviousState
                {
                    PreviousWorkingOrBrokenBuildStatus = TryGetBuildStatus(changedBuildStatus, PreviousWorkingOrBrokenBuildStatus),
                    PreviousBuildStatus = TryGetBuildStatus(changedBuildStatus, PreviousBuildStatus),
                    ChangedBuildStatus = changedBuildStatus,
                });
            return result.ToList();
        }

        /// <summary>
        /// We cache the build statuses primarily so we can tell the rules engine whether a build
        /// changed from Broken->InProgress->Working or Broken->InProgress, etc
        /// </summary>
        private void CacheBuildStatuses(IEnumerable<BuildStatus> changedBuildStatuses)
        {
            foreach (var changedBuildStatus in changedBuildStatuses)
            {
                SetValue(changedBuildStatus, PreviousBuildStatus);
                if (changedBuildStatus.IsWorkingOrBroken())
                {
                    SetValue(changedBuildStatus, PreviousWorkingOrBrokenBuildStatus);
                }
            }
        }

        private void UpdateBuildNamesInSettingsIfAnyChanged(IEnumerable<BuildStatus> changedBuildStatuses)
        {
            foreach (var build in changedBuildStatuses)
            {
                _settings.UpdateNameIfChanged(build);
            }
        }

        private void SendCiServerConnectedEvents()
        {
            if (_serverPreviouslyUnavailable)
            {
                InvokeTrayNotify(ToolTipIcon.Info, "Reconnected", "Reconnected to server.");
            }
            _serverPreviouslyUnavailable = false;
            InvokeUpdateStatusBar("Connected");
        }

        // e.g. if a build exists in newStatus but doesn't exit in oldStatus, return it.  If a build exists in
        //  oldStatus and in newStatus and the BuildStatusEnum is different then return it.
        private IList<BuildStatus> GetChangedBuildStatuses(BuildStatus[] allBuildStatuses)
        {
            var oldBuildStatus = _previousBuildStatuses;
            _previousBuildStatuses = allBuildStatuses;
            var changedBuildStatuses = from newStatus in allBuildStatuses
                                       from oldStatus in oldBuildStatus.Where(s => s.BuildDefinitionId == newStatus.BuildDefinitionId).DefaultIfEmpty()
                                       where DidBuildStatusChange(oldStatus, newStatus)
                                       select newStatus;
            
            Debug.Assert(changedBuildStatuses != null, "changedBuildStatuses should not be null");
            Debug.Assert(PreviousWorkingOrBrokenBuildStatus != null, "PreviousWorkingOrBrokenBuildStatus should never be null");
            Debug.Assert(PreviousBuildStatus != null, "PreviousBuildStatus should never be null");

            return changedBuildStatuses.ToList();
        }

        private static bool DidBuildStatusChange(BuildStatus oldStatus, BuildStatus newStatus)
        {
            if (oldStatus == null) return true;

            bool startTimesUnequal = oldStatus.StartedTime != newStatus.StartedTime;
            bool buildStatusesUnequal = oldStatus.BuildStatusEnum != newStatus.BuildStatusEnum;
           bool buildChanged = 
                startTimesUnequal || buildStatusesUnequal;
            
            if (buildChanged)
            {
                string message = string.Format(
                    "Detected a build status change. BuildDefinitionId: {0}; OldStartTime: {1}; NewStartTime: {2}; OldStatus: {3}; NewStatus: {4}; BuildId: {5}; RequestedBy: {6}", 
                    newStatus.BuildDefinitionId, 
                    oldStatus.StartedTime, 
                    newStatus.StartedTime, 
                    oldStatus.BuildStatusEnum, 
                    newStatus.BuildStatusEnum,
                    newStatus.BuildId,
                    newStatus.RequestedBy
                    );
                _log.Debug(message);
            }

            return buildChanged;
        }

        private void TryToGetAndSendNewSosOnlineAlerts()
        {
            if (DisableSosOnline) return;
            SosOnlineService.TryToGetAndSendNewSosOnlineAlerts(_settings, Now, InvokeNewAlert);
        }

        protected virtual DateTime Now
        {
            get { return DateTime.Now; }
        }

        private void InvokeStatsChanged(IList<BuildStatus> changedBuildStatuses)
        {
            if (!changedBuildStatuses.Any(i => i.IsWorkingOrBroken())) return;
            var statsChanged = StatsChanged;
            if (statsChanged == null) return;
            statsChanged(this, new StatsChangedEventArgs { ChangedBuildStatuses = changedBuildStatuses });
        }

        private void InvokeRefreshStatus(IEnumerable<BuildStatus> buildStatuses)
        {
            IList<BuildStatusDto> buildStatusListViewItems = buildStatuses
                .Select(bs => bs.AsBuildStatusDto(DateTime.Now, PreviousWorkingOrBrokenBuildStatus, _settings))
                .ToList();

            var refreshStatus = RefreshStatus;
            if (refreshStatus == null) return;
            refreshStatus(this, new RefreshStatusEventArgs { BuildStatusDtos = buildStatusListViewItems });
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (_previousBuildStatuses.Any(bs => bs.BuildStatusEnum == BuildStatusEnum.InProgress))
            {
                InvokeRefreshStatus(_previousBuildStatuses);
            }
        }

        private void InvokeRefreshStatusIfAnythingChanged(IEnumerable<BuildStatus> allBuildStatuses, IEnumerable<BuildStatus> changedBuildStatuses)
        {
            if (changedBuildStatuses.Any())
            {
                _log.Debug("InvokeRefreshStatus: Some build status changed");
                InvokeRefreshStatus(allBuildStatuses);
            }
        }

        protected virtual SosOnlineService SosOnlineService
        {
            get { return _sosOnlineService; }
        }

        private void SyncNewBuildsToSos(IList<BuildStatus> changedBuildStatuses)
        {
            if (!_settings.SosOnlineAlwaysSync) return;
            var noUsername = string.IsNullOrEmpty(_settings.SosOnlineUsername);
            if (noUsername) return;

            TrySynchronizeBuildStatuses(changedBuildStatuses);
            TrySynchronizeMyPointsAndAchievements(changedBuildStatuses);
        }

        private void TrySynchronizeBuildStatuses(IList<BuildStatus> changedBuildStatuses)
        {
            if (DisableSosOnline) return;
            if (_settings.SosOnlineWhatToSync != WhatToSyncEnum.BuildStatuses) return;
            var requestedByPeople = _settings.GetUsersContainedInBuildsAsDto(changedBuildStatuses);
            SosOnlineService.BuildStatusChanged(_settings, changedBuildStatuses, requestedByPeople);
        }

        private void TrySynchronizeMyPointsAndAchievements(IList<BuildStatus> changedBuildStatuses)
        {
            if (DisableSosOnline) return;
            if (!changedBuildStatuses.Any(i => i.IsWorkingOrBroken())) return;
            var anyBuildsAreMine = changedBuildStatuses.Any(i => i.RequestedBy == _settings.MyRawName && i.IsWorkingOrBroken());
            if (!anyBuildsAreMine) return;
            var exportedBuilds = SosDb.ExportNewBuilds(_settings);
            var noBuildsToExport = exportedBuilds == null;
            if (noBuildsToExport)
            {
                _log.Error("No builds were found to export from sosDb to sos online even though one was changed");
                return;
            }
            _log.Debug("Uploading the following builds to sos online: " + exportedBuilds);
            string exportedAchievements = _settings.ExportNewAchievements();
            SosOnlineService.Synchronize(_settings, exportedBuilds, exportedAchievements, OnAddBuildsSuccess, OnAddBuildsFail);
        }

        private void OnAddBuildsFail(string userTargedErrorMessage, Exception ex)
        {
            _log.Error("Failed to connect to SoS online", ex);
            InvokeUpdateStatusBar(userTargedErrorMessage, ex);
        }

        private void OnAddBuildsSuccess(DateTime newHighWaterMark)
        {
            _log.Debug("Successfully uploaded to sos online. New high water mark: " + newHighWaterMark);
            _settings.SosOnlineHighWaterMark = newHighWaterMark.Ticks;
        }

        private void NotifyIfNewAchievements(IList<BuildStatus> changedBuildStatuses)
        {
            if (!changedBuildStatuses.Any(i => i.IsWorkingOrBroken())) return;
            var visiblePeopleWithNewChanges = from changedBuildStatus in changedBuildStatuses
                                             join person in _settings.VisiblePeople on changedBuildStatus.RequestedBy equals person.RawName
                                             where changedBuildStatus.IsWorkingOrBroken()
                                             select new
                                             {
                                                 Person = person,
                                                 Build = changedBuildStatus
                                             };
            
            foreach (var personWithNewChange in visiblePeopleWithNewChanges)
            {
                var newAchievements = personWithNewChange.Person.CalculateNewAchievements(_settings, personWithNewChange.Build);
                List<AchievementLookup> achievements = newAchievements.ToList();
                if (achievements.Any())
                {
                    personWithNewChange.Person.AddAchievements(achievements);
                    InvokeNewAchievement(personWithNewChange.Person, achievements);
                }
            }
            // this is required because achievements often write to settings e.g. cumulative build time
            _settings.Save();
        }

        private static BuildStatusEnum? TryGetBuildStatus(BuildStatus changedBuildStatus, IDictionary<string, BuildStatus> dictionary)
        {
            BuildStatus previousWorkingOrBrokenBuildStatus;
            dictionary.TryGetValue(changedBuildStatus.BuildDefinitionId, out previousWorkingOrBrokenBuildStatus);

            return previousWorkingOrBrokenBuildStatus == null ? (BuildStatusEnum?)null : previousWorkingOrBrokenBuildStatus.BuildStatusEnum;
        }

        private static void SetValue(BuildStatus changedBuildStatus, IDictionary<string, BuildStatus> dictionary)
        {
            try
            {
                if (!dictionary.ContainsKey(changedBuildStatus.BuildDefinitionId))
                    dictionary.Add(changedBuildStatus.BuildDefinitionId, changedBuildStatus);
                else
                    dictionary[changedBuildStatus.BuildDefinitionId] = changedBuildStatus;
            }
            catch (IndexOutOfRangeException)
            {
                _log.Error("Tried to update the cache from the thread '" + Thread.CurrentThread.Name + "' but failed because the cache was previously accessed from a different thread. This could cause errors in determining whether a build changed.");
            }
        }

        private void InvokeSetTrayIcon(IEnumerable<BuildStatus> buildStatuses)
        {
            var buildStatusesAndSettings = from buildStatus in buildStatuses
                                           join setting in _settings.CiEntryPointSettings.SelectMany(i => i.BuildDefinitionSettings) on buildStatus.BuildDefinitionId equals setting.Id
                                           select new { buildStatus, setting };
            bool anyBuildBroken = buildStatusesAndSettings
                .Any(bs => bs.setting.AffectsTrayIcon && (
                    bs.buildStatus.BuildStatusEnum == BuildStatusEnum.Broken));
            TrayIcon trayIcon = anyBuildBroken ? TrayIcon.Red : TrayIcon.Green;
            InvokeSetTrayIcon(trayIcon);
        }

        private void AddAnyNewPeopleToSettings(IEnumerable<BuildStatus> changedBuildStatuses)
        {
            var buildStatusesWithNewPeople = from buildStatus in changedBuildStatuses
                                             join setting in _settings.CiEntryPointSettings.SelectMany(i => i.BuildDefinitionSettings) on buildStatus.BuildDefinitionId equals setting.Id
                                             where !setting.ContainsPerson(buildStatus)
                                                && !string.IsNullOrEmpty(buildStatus.RequestedBy)
                                             select new { buildStatus, setting };

            var buildStatusesWithNewPeopleList = buildStatusesWithNewPeople
                .GroupBy(i => i.buildStatus.RequestedBy)
                .Select(i => i.First())
                .ToList();
            if (!buildStatusesWithNewPeopleList.Any()) return;

            var allExistingPeople = _settings.CiEntryPointSettings
                .SelectMany(eps => eps.BuildDefinitionSettings)
                .SelectMany(bds => bds.People)
                .Distinct().ToList();
            var newPeople = buildStatusesWithNewPeopleList
                .Select(s => s.buildStatus.RequestedBy)
                .Where(p => allExistingPeople.All(p1 => p1 != p)).ToList();

            buildStatusesWithNewPeopleList
                .ForEach(bss => bss.setting.People.Add(bss.buildStatus.RequestedBy));
            _settings.Save();

            newPeople.ForEach(InvokeNewUser);
        }

        internal void InvokeModalDialog(string dialogText, string okText)
        {
            var modalDialog = ModalDialog;
            if (modalDialog == null) return;
            modalDialog(this, new ModalDialogEventArgs { DialogText = dialogText, OkText = okText });
        }

        public void InvokeSetAudio(AudioPattern audioPattern, int? duration)
        {
            if (_settings.Mute) return;
            
            var startSiren = SetAudio;
            if (startSiren == null) return;
            startSiren(this, new SetAudioEventArgs { AudioPattern = audioPattern, Duration = duration });
        }

        public void InvokeSetLights(LedPattern ledPattern, int? duration)
        {
            var stopSiren = SetLights;
            if (stopSiren == null) return;
            stopSiren(this, new SetLightsEventArgs { LedPattern = ledPattern, Duration = duration });
        }

        public void Start(bool initialStart)
        {
            var ciEntryPointSettings = _settings.CiEntryPointSettings
                .Where(s => !string.IsNullOrEmpty(s.Url))
                .ToList();

            _watchers.Clear();
            foreach (var ciEntryPointSetting in ciEntryPointSettings)
            {
                WatcherBase watcher = ciEntryPointSetting.GetWatcher(_settings);
                _watchers.Add(watcher);
                watcher.StatusChecked += BuildWatcherStatusChecked;
                watcher.StoppedWatching += StoppedWatching;
                watcher.ServerUnavailable += BuildWatcherServerUnavailable;
                watcher.BuildDefinitionNotFound += BuildDefinitionNotFound;
                watcher.Settings = _settings;
                watcher.CiEntryPointSetting = ciEntryPointSetting;
                // todo: It looks like we are overwriting preceding watcher threads with subsequent ones which will cause problems when we try to Stop() them
                _watcherThread = new Thread(watcher.StartWatching) { IsBackground = true, Name = "CiWatcher" };
                _watcherThread.Start();
            }

            if (ciEntryPointSettings.Any())
            {
                if (initialStart)
                {
                    InvokeUpdateStatusBar("Attempting to connect to server");
                    SetStatusUnknown();
                }

                _timer.Start();
            }
            else
            {
                InvokeUpdateStatusBar("");
                InvokeRefreshStatus(Enumerable.Empty<BuildStatus>());
            }
        }

        private void BuildDefinitionNotFound(object sender, BuildDefinitionNotFoundArgs args)
        {
            if (args.BuildDefinitionSetting == null)
            {
                _log.Warn("BuildDefinitionNotFound, yet no BuildDefinition provided.");
                return;
            }
            args.BuildDefinitionSetting.Active = false;
            _settings.Save();
            InvokeTrayNotify(ToolTipIcon.Error, "Can't Find " + args.BuildDefinitionSetting.Name, "This build will be removed from the list of watched builds.\nYou may add it back from the 'Configure CI Server' button.");
        }

        private void SetStatusUnknown()
        {
            InvokeSetTrayIcon(TrayIcon.Question);
            IEnumerable<BuildStatus> buildStatuses = _settings.CiEntryPointSettings
                .SelectMany(i => i.BuildDefinitionSettings)
                .Where(bd => bd.Active)
                .Select(bd => bd.AsUnknownBuildStatus(SosDb));
            InvokeRefreshStatus(buildStatuses);
        }

        public void RefreshAll()
        {
            Stop();
            Start(initialStart: false);
        }

        public void Stop()
        {
            _timer.Stop();
            _watcherThread?.Abort();
        }

        public void SyncAllBuildStatuses()
        {
            if (DisableSosOnline) return;
            if (_settings.SosOnlineWhatToSync == WhatToSyncEnum.BuildStatuses)
            {
                _sosOnlineService.BuildStatusChanged(
                    _settings,
                    PreviousWorkingOrBrokenBuildStatus.Select(i => i.Value).ToList(),
                    _settings.People.Select(i => new InstanceUserDto(i)).ToList()
                    );
            }
        }
    }
}
