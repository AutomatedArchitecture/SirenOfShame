using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Services;
using log4net;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Network;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Util;
using Timer = System.Windows.Forms.Timer;

namespace SirenOfShame.Lib.Watcher
{
    public class RulesEngine
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(RulesEngine));
        protected IDictionary<string, BuildStatus> PreviousWorkingOrBrokenBuildStatus { get; set; }
        protected IDictionary<string, BuildStatus> PreviousBuildStatus { get; set; }

        private readonly SirenOfShameSettings _settings;
        private readonly IList<WatcherBase> _watchers = new List<WatcherBase>();
        public SosDb SosDb = new SosDb();

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

        public void InvokeNewAchievement(PersonSetting person, List<AchievementLookup> achievements)
        {
            var newAchievement = NewAchievement;
            if (newAchievement != null) newAchievement(this, new NewAchievementEventArgs { Person = person, Achievements = achievements });
        }

        public void InvokePlayWindowsAudio(string location)
        {
            if (_settings.Mute) return;

            PlayWindowsAudioEvent playWindowsAudio = PlayWindowsAudio;
            if (playWindowsAudio != null) playWindowsAudio(this, new PlayWindowsAudioEventArgs { Location = location });
        }

        public void InvokeSetTrayIcon(TrayIcon trayIcon)
        {
            SetTrayIconEvent setTrayIcon = SetTrayIcon;
            if (setTrayIcon != null) setTrayIcon(this, new SetTrayIconEventArgs { TrayIcon = trayIcon });
        }

        public void InvokeNewAlert(NewAlertEventArgs args)
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
            _buildStatus = new BuildStatus[] { };
        }

        private BuildStatus[] _buildStatus = new BuildStatus[] { };

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
            if (_serverPreviouslyUnavailable)
            {
                InvokeTrayNotify(ToolTipIcon.Info, "Reconnected", "Reconnected to server.");
            }
            _serverPreviouslyUnavailable = false;

            GetAlertAsyncIfNewDay();

            BuildStatus[] newBuildStatus = BuildStatusUtil.Merge(_buildStatus, args.BuildStatuses);
            var oldBuildStatus = _buildStatus;
            _buildStatus = newBuildStatus;

            InvokeUpdateStatusBar("Connected");

            // e.g. if a build exists in newStatus but doesn't exit in oldStatus, return it.  If a build exists in
            //  oldStatus and in newStatus and the BuildStatusEnum is different then return it.
            var changedBuildStatuses = from newStatus in newBuildStatus
                                       from oldStatus in oldBuildStatus.Where(s => s.BuildDefinitionId == newStatus.BuildDefinitionId).DefaultIfEmpty()
                                       where oldStatus == null || (oldStatus.StartedTime != newStatus.StartedTime) || oldStatus.BuildStatusEnum != newStatus.BuildStatusEnum
                                       select newStatus;
            changedBuildStatuses = changedBuildStatuses.ToList();

            BuildWatcherStatusChanged(newBuildStatus, changedBuildStatuses.ToList());
        }

        private void GetAlertAsyncIfNewDay()
        {
            // if someone doesn't want to check for the lastest software, they probably are on a private network and don't want to check for alerts either
            if (_settings.UpdateLocation != UpdateLocation.Auto) return;

            bool weHaveAlreadyCheckedForAlertsToday = _settings.LastCheckedForAlert != null && (Now - _settings.LastCheckedForAlert.Value).TotalHours < 24;
            if (weHaveAlreadyCheckedForAlertsToday) return;
            
            _settings.LastCheckedForAlert = DateTime.Now;
            _settings.Save();
            SosWebClient webClient = GetWebClient();
            webClient.DownloadStringCompleted += (s, e) =>
            {
                try
                {
                    if (e.Error != null)
                    {
                        _log.Error("Error retrieving alert", e.Error);
                        return;
                    }
                    NewAlertEventArgs args = new NewAlertEventArgs();
                    var successParsing = args.Instantiate(e.Result);
                    if (successParsing)
                    {
                        if (_settings.SoftwareInstanceId == null)
                        {
                            _settings.SoftwareInstanceId = args.SoftwareInstanceId;
                            _settings.Save();
                        }
                        if (_settings.AlertClosed == null || args.AlertDate > _settings.AlertClosed)
                        {
                            InvokeNewAlert(args);
                        }
                    }
                } 
                catch (Exception ex)
                {
                    _log.Error("Error retrieving alert", ex);
                }
            };
            string url = string.Format("http://sirenofshame.com/GetAlert?SirenEverConnected={0}&SoftwareInstanceId={1}&ServerType={2}&Version={3}",
                _settings.SirenEverConnected,
                _settings.SoftwareInstanceId,
                string.Join(",", _settings.CiEntryPointSettings.Select(cip => cip.Name)),
                Application.ProductVersion
                );
            webClient.DownloadStringAsync(new Uri(url));
        }

        protected virtual DateTime Now
        {
            get { return DateTime.Now; }
        }

        protected virtual SosWebClient GetWebClient()
        {
            return new SosWebClient();
        }

        private void InvokeStatsChanged(IList<BuildStatus> changedBuildStatuses)
        {
            var statsChanged = StatsChanged;
            if (statsChanged == null) return;
            statsChanged(this, new StatsChangedEventArgs { ChangedBuildStatuses = changedBuildStatuses });
        }

        private void InvokeRefreshStatus(IEnumerable<BuildStatus> buildStatuses)
        {
            IEnumerable<BuildStatusListViewItem> buildStatusListViewItems = buildStatuses
                .OrderBy(s => s.Name)
                .Select(bs => bs.AsBuildStatusListViewItem(DateTime.Now, PreviousWorkingOrBrokenBuildStatus, _settings));

            var refreshStatus = RefreshStatus;
            if (refreshStatus == null) return;
            refreshStatus(this, new RefreshStatusEventArgs { BuildStatusListViewItems = buildStatusListViewItems });
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (_buildStatus.Any(bs => bs.BuildStatusEnum == BuildStatusEnum.InProgress))
            {
                InvokeRefreshStatus(_buildStatus);
            }
        }

        private void BuildWatcherStatusChanged(IList<BuildStatus> allBuildStatuses, IList<BuildStatus> changedBuildStatuses)
        {
            Debug.Assert(changedBuildStatuses != null, "changedBuildStatuses should not be null");
            Debug.Assert(PreviousWorkingOrBrokenBuildStatus != null, "PreviousWorkingOrBrokenBuildStatus should never be null");
            Debug.Assert(PreviousBuildStatus != null, "PreviousBuildStatus should never be null");

            if (changedBuildStatuses.Any())
            {
                _log.Debug("InvokeRefreshStatus: Some build status changed");
                InvokeRefreshStatus(allBuildStatuses);
            }

            InvokeSetTrayIconForChangedBuildStatuses(allBuildStatuses);
            AddRequestedByPersonToBuildStatusSettings(changedBuildStatuses);

            foreach (var changedBuildStatus in changedBuildStatuses)
            {
                BuildStatusEnum? previousWorkingOrBrokenStatus = TryGetBuildStatus(changedBuildStatus, PreviousWorkingOrBrokenBuildStatus);
                BuildStatusEnum? previousStatus = TryGetBuildStatus(changedBuildStatus, PreviousBuildStatus);
                changedBuildStatus.Changed(previousWorkingOrBrokenStatus, previousStatus, this, _settings.Rules);

                _settings.UpdateNameIfChanged(changedBuildStatus);

                SetValue(changedBuildStatus, PreviousBuildStatus);
                if (changedBuildStatus.IsWorkingOrBroken())
                {
                    if (previousWorkingOrBrokenStatus != null)
                        SosDb.Write(changedBuildStatus, _settings);

                    SetValue(changedBuildStatus, PreviousWorkingOrBrokenBuildStatus);
                }
            }

            if (changedBuildStatuses.Any(i => i.IsWorkingOrBroken()))
            {
                NotifyIfNewAchievements(changedBuildStatuses);
                InvokeStatsChanged(changedBuildStatuses);
                SyncNewBuildsToSos(changedBuildStatuses);
            }
        }

        private void SyncNewBuildsToSos(IList<BuildStatus> changedBuildStatuses)
        {
            if (!_settings.SosOnlineAlwaysSync) return;
            var noUsername = string.IsNullOrEmpty(_settings.SosOnlineUsername);
            if (noUsername) return;
            var anyBuildsAreMine = changedBuildStatuses.Any(i => i.RequestedBy == _settings.MyRawName && i.IsWorkingOrBroken());
            if (!anyBuildsAreMine) return;
            var sosOnlineService = new SosOnlineService();
            var sosDb = new SosDb();
            var exportedBuilds = sosDb.ExportNewBuilds(_settings);
            var noBuildsToExport = exportedBuilds == null;
            if (noBuildsToExport)
            {
                _log.Error("No builds were found to export from sosDb to sos online even though one was changed");
                return;
            }
            _log.Debug("Uploading the following builds to sos online: " + exportedBuilds);
            string exportedAchievements = _settings.ExportNewAchievements();
            sosOnlineService.Synchronize(_settings, exportedBuilds, exportedAchievements, OnAddBuildsSuccess, OnAddBuildsFail);
        }

        private void OnAddBuildsFail(string userTargedErrorMessage, ServerUnavailableException ex)
        {
            _log.Error("Failed to connect to SoS online", ex);
            InvokeUpdateStatusBar(userTargedErrorMessage, ex);
        }

        private void OnAddBuildsSuccess(DateTime newHighWaterMark)
        {
            _log.Debug("Successfully uploaded to sos online. New high water mark: " + newHighWaterMark);
            _settings.SosOnlineHighWaterMark = newHighWaterMark.Ticks;
        }

        private void NotifyIfNewAchievements(IEnumerable<BuildStatus> changedBuildStatuses)
        {
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
            if (!dictionary.ContainsKey(changedBuildStatus.BuildDefinitionId))
                dictionary.Add(changedBuildStatus.BuildDefinitionId, changedBuildStatus);
            else
                dictionary[changedBuildStatus.BuildDefinitionId] = changedBuildStatus;
        }

        private void InvokeSetTrayIconForChangedBuildStatuses(IEnumerable<BuildStatus> allBuildStatuses)
        {
            var buildStatusesAndSettings = from buildStatus in allBuildStatuses
                                           join setting in _settings.CiEntryPointSettings.SelectMany(i => i.BuildDefinitionSettings) on buildStatus.BuildDefinitionId equals setting.Id
                                           select new { buildStatus, setting };
            bool anyBuildBroken = buildStatusesAndSettings
                .Any(bs => bs.setting.AffectsTrayIcon && bs.buildStatus.BuildStatusEnum == BuildStatusEnum.Broken);
            TrayIcon trayIcon = anyBuildBroken ? TrayIcon.Red : TrayIcon.Green;
            InvokeSetTrayIcon(trayIcon);
        }

        private void AddRequestedByPersonToBuildStatusSettings(IEnumerable<BuildStatus> changedBuildStatuses)
        {
            var buildStatusesWithNewPeople = from buildStatus in changedBuildStatuses
                                             join setting in _settings.CiEntryPointSettings.SelectMany(i => i.BuildDefinitionSettings) on buildStatus.BuildDefinitionId equals setting.Id
                                             where !setting.ContainsPerson(buildStatus)
                                             select new { buildStatus, setting };

            var buildsWithoutRequestedByPerson = buildStatusesWithNewPeople.ToList();
            buildsWithoutRequestedByPerson
                .Where(bss => !string.IsNullOrEmpty(bss.buildStatus.RequestedBy))
                .ToList()
                .ForEach(bss => bss.setting.People.Add(bss.buildStatus.RequestedBy));
            if (buildsWithoutRequestedByPerson.Any())
                _settings.Save();
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

        private Thread _watcherThread;

        readonly Timer _timer = new Timer();

        public void Start(bool initialStart = true)
        {
            var ciEntryPointSettings = _settings.CiEntryPointSettings.Where(s => !string.IsNullOrEmpty(s.Url));

            _watchers.Clear();
            foreach (var ciEntryPointSetting in ciEntryPointSettings)
            {
                WatcherBase watcher = ciEntryPointSetting.GetWatcher(_settings);
                _watchers.Add(watcher);
                watcher.StatusChecked += BuildWatcherStatusChecked;
                watcher.ServerUnavailable += BuildWatcherServerUnavailable;
                watcher.BuildDefinitionNotFound += BuildDefinitionNotFound;
                watcher.Settings = _settings;
                watcher.CiEntryPointSetting = ciEntryPointSetting;
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
                .Select(bd => bd.AsUnknownBuildStatus());
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
            if (_watcherThread != null)
                _watcherThread.Abort();
        }
    }
}
