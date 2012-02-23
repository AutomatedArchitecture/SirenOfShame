using SirenOfShame.Lib.Network;

namespace SirenOfShame.Test.Unit.Watcher
{
    internal class FakeWebClient : SosWebClient
    {
        public override void DownloadStringAsync(System.Uri uri)
        {
            // do nothing
        }

        public void InvokeDownloadStringCompleted(string result)
        {
            InvokeDownloadStringCompleted(this, new SosDownloadStringCompletedEventArgs { Result = result });
        }
    }
}