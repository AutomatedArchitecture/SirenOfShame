using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Util;
using Timer = System.Windows.Forms.Timer;

namespace SirenOfShame.Lib.Watcher
{
    public class RulesEngine
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(RulesEngine));
        protected IDictionary<string, BuildStatus> PreviousWorkingOrBrokenBuildStatus { get; set; }

        private readonly SirenOfShameSettings _settings;
        private readonly IList<WatcherBase> _watchers = new List<WatcherBase>();

        public event UpdateStatusBarEvent UpdateStatusBar;
        public event StatusChangedEvent RefreshStatus;
        public event TrayNotifyEvent TrayNotify;
        public event ModalDialogEvent ModalDialog;
        public event SetAudioEvent SetAudio;
        public event SetLightsEvent SetLights;
        public event SetTrayIconEvent SetTrayIcon;

        public void InvokeSetTrayIcon(TrayIcon trayIcon)
        {
            SetTrayIconEvent setTrayIcon = SetTrayIcon;
            if (setTrayIcon != null) setTrayIcon(this, new SetTrayIconEventArgs { TrayIcon = trayIcon });
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
            InvokeUpdateStatusBar("Build server unavailable, attempting to reconnect");
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
            _buildStatus = new BuildStatus[] { };
        }

        private BuildStatus[] _buildStatus = new BuildStatus[] { };

        private void InvokeUpdateStatusBar(string statusText)
        {
            string datedStatusText = null;
            if (!string.IsNullOrEmpty(statusText))
            {
                datedStatusText = string.Format("{0:G} - {1}", DateTime.Now, statusText);
            }
            var updateStatusBar = UpdateStatusBar;
            if (updateStatusBar == null) return;
            updateStatusBar(this, new UpdateStatusBarEventArgs { StatusText = datedStatusText });
        }

        private void BuildWatcherStatusChecked(object sender, StatusCheckedEventArgsArgs args)
        {
            if (_serverPreviouslyUnavailable)
            {
                InvokeTrayNotify(ToolTipIcon.Info, "Reconnected", "Reconnected to server.");
            }
            _serverPreviouslyUnavailable = false;

            var newBuildStatus = BuildStatusUtil.Merge(_buildStatus, args.BuildStatuses);
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

        private void InvokeRefreshStatus(IEnumerable<BuildStatus> buildStatuses)
        {
            IEnumerable<BuildStatusListViewItem> buildStatusListViewItems = buildStatuses
                .OrderBy(s => s.Name)
                .Select(bs => bs.AsBuildStatusListViewItem(DateTime.Now, PreviousWorkingOrBrokenBuildStatus));

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

        private void BuildWatcherStatusChanged(BuildStatus[] allBuildStatuses, IList<BuildStatus> changedBuildStatuses)
        {
            Debug.Assert(changedBuildStatuses != null, "changedBuildStatuses should not be null");
            Debug.Assert(PreviousWorkingOrBrokenBuildStatus != null, "PreviousWorkingOrBrokenBuildStatus should never be null");

            if (changedBuildStatuses.Any())
            {
                _log.Debug("InvokeRefreshStatus: Some build status changed");
                InvokeRefreshStatus(allBuildStatuses);
            }

            InvokeSetTrayIconForChangedBuildStatuses(allBuildStatuses);
            AddRequestedByPersonToBuildStatusSettings(changedBuildStatuses);

            foreach (var changedBuildStatus in changedBuildStatuses)
            {
                BuildStatus previousWorkingOrBrokenBuildStatus;
                PreviousWorkingOrBrokenBuildStatus.TryGetValue(changedBuildStatus.BuildDefinitionId, out previousWorkingOrBrokenBuildStatus);

                BuildStatusEnum? previousStatus = previousWorkingOrBrokenBuildStatus == null ? (BuildStatusEnum?)null : previousWorkingOrBrokenBuildStatus.BuildStatusEnum;
                changedBuildStatus.Changed(previousStatus, this, _settings.Rules);

                if (changedBuildStatus.IsWorkingOrBroken())
                {
                    BuildStatus status;
                    bool exists = PreviousWorkingOrBrokenBuildStatus.TryGetValue(changedBuildStatus.BuildDefinitionId, out status);
                    if (!exists)
                    {
                        PreviousWorkingOrBrokenBuildStatus.Add(changedBuildStatus.BuildDefinitionId, changedBuildStatus);
                    }
                    else
                    {
                        PreviousWorkingOrBrokenBuildStatus[changedBuildStatus.BuildDefinitionId] = changedBuildStatus;
                    }
                }
            }
        }

        private void InvokeSetTrayIconForChangedBuildStatuses(IEnumerable<BuildStatus> allBuildStatuses)
        {
            var buildStatusesAndSettings = from buildStatus in allBuildStatuses
                                           join setting in _settings.CiEntryPointSettings.SelectMany(i => i.BuildDefinitionSettings) on buildStatus.BuildDefinitionId
                                               equals setting.Id
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
                .Where(bss => bss.buildStatus.RequestedBy != null)
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
                var watcher = ciEntryPointSetting.GetWatcher(_settings);
                _watchers.Add(watcher);
                watcher.StatusChecked += BuildWatcherStatusChecked;
                watcher.ServerUnavailable += BuildWatcherServerUnavailable;
                watcher.BuildDefinitionNotFound += BuildDefinitionNotFound;
                watcher.Settings = _settings;
                watcher.CiEntryPointSetting = ciEntryPointSetting;
                _watcherThread = new Thread(watcher.StartWatching) { IsBackground = true };
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
