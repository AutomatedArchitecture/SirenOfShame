using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using log4net;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Watcher
{
    public abstract class WatcherBase : IDisposable
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(WatcherBase));

        protected WatcherBase(SirenOfShameSettings settings)
        {
            Settings = settings;
        }

        protected void InvokeServerUnavailable(ServerUnavailableException args)
        {
            var e = ServerUnavailable;
            if (e != null) e(this, new ServerUnavailableEventArgs(args));
        }
        
        protected void InvokeBuildDefinitionNotFound(BuildDefinitionSetting buildDefinitionSetting)
        {
            var e = BuildDefinitionNotFound;
            if (e != null) e(this, new BuildDefinitionNotFoundArgs(buildDefinitionSetting));
        }

        protected void InvokeStatusChecked(BuildStatus[] args)
        {
            var e = StatusChecked;
            if (e != null) e(this, new StatusCheckedEventArgsArgs
            {
                BuildStatuses = args
            });
        }

        protected void GetBuildStatusAndFireEvents()
        {
            try
            {
                BuildStatus[] newBuildStatus = GetBuildStatus().ToArray();
                if (newBuildStatus.Length != 0)
                    InvokeStatusChecked(newBuildStatus);
            }
            catch (ServerUnavailableException ex)
            {
                InvokeServerUnavailable(ex);
            }
            catch (BuildDefinitionNotFoundException ex)
            {
                InvokeBuildDefinitionNotFound(ex.BuildDefinitionSetting);
            }
        }

        protected abstract IEnumerable<BuildStatus> GetBuildStatus();

        public virtual void StartWatching()
        {
            try
            {
                _log.Debug(string.Format("Started watching build status, poling interval: {0} seconds", Settings.PollInterval));
                while (true)
                {
                    GetBuildStatusAndFireEvents();
                    Thread.Sleep(Settings.PollInterval * 1000);
                }
            }
            catch (ThreadAbortException)
            {
                _log.Debug("Stopped watching build status (ThreadAbortException)");
                StopWatching();
            }
            catch (Exception ex)
            {
                _log.Error("uncaught exception in watcher", ex);
                StopWatching();
                ExceptionMessageBox.Show(null, "Drat", "Error connecting to server", ex);
            }
        }

        public event StatusCheckedEvent StatusChecked;
        public event ServerUnavailableEvent ServerUnavailable;
        public event BuildDefinitionNotFoundEvent BuildDefinitionNotFound;
        public SirenOfShameSettings Settings { get; set; }
        public abstract void StopWatching();

        public abstract void Dispose();
    }

    public delegate void BuildDefinitionNotFoundEvent(object sender, BuildDefinitionNotFoundArgs args);

    public class BuildDefinitionNotFoundArgs : EventArgs
    {
        public BuildDefinitionSetting BuildDefinitionSetting { get; set; }

        public BuildDefinitionNotFoundArgs(BuildDefinitionSetting buildDefinitionSetting)
        {
            BuildDefinitionSetting = buildDefinitionSetting;
        }
    }

    public delegate void ServerUnavailableEvent(object sender, ServerUnavailableEventArgs args);

    public class ServerUnavailableEventArgs
    {
        public ServerUnavailableEventArgs(ServerUnavailableException ex)
        {
            Exception = ex;
        }
        public ServerUnavailableException Exception { get; set; }
    }
}
