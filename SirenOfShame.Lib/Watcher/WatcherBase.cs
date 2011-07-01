using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
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

        protected void InvokeServerUnavailable(ServerUnavailableEventArgs args)
        {
            var e = ServerUnavailable;
            if (e != null) e(this, args);
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
            BuildStatus[] newBuildStatus;
            try
            {
                newBuildStatus = GetBuildStatus().ToArray();
            }
            catch (ServerUnavailableException ex)
            {
                if (ServerUnavailable != null)
                {
                    InvokeServerUnavailable(new ServerUnavailableEventArgs(ex));
                }
                return;
            }

            InvokeStatusChecked(newBuildStatus);
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
                StopWatching();
                _log.Debug("Stopped watching build status (ThreadAbortException)");
            }
            catch (Exception ex)
            {
                _log.Error("uncaught exception in watcher", ex);
                StopWatching();
                MessageBox.Show("Error connecting to server: " + ex.Message);
            }
        }

        public event StatusCheckedEvent StatusChecked;
        public event ServerUnavailableEvent ServerUnavailable;
        public SirenOfShameSettings Settings { get; set; }
        public abstract void StopWatching();

        public abstract void Dispose();
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
