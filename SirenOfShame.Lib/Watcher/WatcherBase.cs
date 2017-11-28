using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
            ServerUnavailable?.Invoke(this, new ServerUnavailableEventArgs(args));
        }
        
        protected void InvokeBuildDefinitionNotFound(BuildDefinitionSetting buildDefinitionSetting)
        {
            BuildDefinitionNotFound?.Invoke(this, new BuildDefinitionNotFoundArgs(buildDefinitionSetting));
        }

        protected void InvokeInvalidCredentials()
        {
            InvalidCredentials?.Invoke(this, new EventArgs());
        }

        protected void InvokeStatusChecked(IList<BuildStatus> args)
        {
            StatusChecked?.Invoke(this, new StatusCheckedEventArgsArgs
            {
                BuildStatuses = args
            });
        }

        private void GetBuildStatusAndFireEvents()
        {
            try
            {
                var newBuildStatus = GetBuildStatus();
                if (newBuildStatus.Count != 0)
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

        protected abstract IList<BuildStatus> GetBuildStatus();

        public virtual void StartWatching()
        {
            try
            {
                _log.Debug(string.Format("Started watching build status, poling interval: {0} seconds",
                    Settings.PollInterval));
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
                OnStoppedWatching();
            }
            catch (TaskCanceledException)
            {
                _log.Debug("Stopped watching build status (TaskCanceledException)");
                StopWatching();
                OnStoppedWatching();
            }
            catch (InvalidCredentialsException)
            {
                _log.Warn("Caught an InvalidCredentialsException, stopping watching so as not to lock out account");
                InvokeInvalidCredentials();
                StopWatching();
                OnStoppedWatching();
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
        public event EventHandler InvalidCredentials;
        public event StoppedWatchingEvent StoppedWatching;
        public SirenOfShameSettings Settings { private get; set; }
        public CiEntryPointSetting CiEntryPointSetting { protected get; set; }

        public abstract void StopWatching();

        public abstract void Dispose();

        protected void OnStoppedWatching()
        {
            StoppedWatching?.Invoke(this, new StoppedWatchingEventArgs());
        }
    }

    public delegate void StoppedWatchingEvent(object sender, StoppedWatchingEventArgs args);

    public class StoppedWatchingEventArgs
    {
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
