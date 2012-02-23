using System;
using System.Net;

namespace SirenOfShame.Lib.Network
{
    public class SosWebClient
    {
        WebClient _webClient = new WebClient();
        
        public SosWebClient()
        {
            _webClient.DownloadStringCompleted += WebClientDownloadStringCompleted;
        }

        protected void InvokeDownloadStringCompleted(object sender, SosDownloadStringCompletedEventArgs e)
        {
            if (DownloadStringCompleted != null)
                DownloadStringCompleted(sender, e);
        }
        
        private void WebClientDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            DownloadStringCompleted(sender, new SosDownloadStringCompletedEventArgs(e));
        }

        public event SosDownloadStringCompletedEventHandler DownloadStringCompleted;

        public virtual void DownloadStringAsync(Uri uri)
        {
            _webClient.DownloadStringAsync(uri);
        }
    }

    public delegate void SosDownloadStringCompletedEventHandler(object sender, SosDownloadStringCompletedEventArgs args);

    public class SosDownloadStringCompletedEventArgs
    {
        public SosDownloadStringCompletedEventArgs() { }

        public SosDownloadStringCompletedEventArgs(DownloadStringCompletedEventArgs downloadStringCompletedEventArgs)
        {
            Error = downloadStringCompletedEventArgs.Error;
            if (Error == null)
            {
                Result = downloadStringCompletedEventArgs.Result;
                Cancelled = downloadStringCompletedEventArgs.Cancelled;
                UserState = downloadStringCompletedEventArgs.UserState;
            }
        }

        public string Result { get; set; }

        public object UserState { get; set; }

        public Exception Error { get; set; }

        public bool Cancelled { get; set; }
    }
}
