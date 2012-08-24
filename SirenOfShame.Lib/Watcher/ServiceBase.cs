using System.Xml.Linq;

namespace SirenOfShame.Lib.Watcher
{
    public abstract class ServiceBase
    {
        protected virtual XDocument DownloadXml(string url, string userName, string password, string cookie = null)
        {
            WebClientXml webClientXml = new WebClientXml();
            return webClientXml.DownloadXml(url, userName, password, cookie);
        }
    }
}
