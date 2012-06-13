using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Xml.Linq;
using SirenOfShame.Lib.Exceptions;
using log4net;

namespace SirenOfShame.Lib.Watcher
{
    public abstract class ServiceBase
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(ServiceBase));

        protected virtual bool IsServerUnavailable(string errorResult)
        {
            if (errorResult.Contains("HTTP Status 404")) return true;
            if (errorResult.Contains("Connection timed out")) return true;
            return false;
        }
        
        protected XDocument DownloadXml(string url, string userName, string password, string cookie = null)
        {
            var webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password),
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore),
            };

            if (cookie != null)
                webClient.Headers.Add("Cookie", cookie);

            try
            {
                var resultString = webClient.DownloadString(url);
                try
                {
                    return XDocument.Parse(resultString);
                }
                catch (Exception ex)
                {
                    string message = "Couldn't parse XML when trying to connect to " + url + ":\n" + resultString;
                    _log.Error(message, ex);
                    throw new SosException(message, ex);
                }
            }
            catch (WebException webException)
            {
                if (webException.Response != null)
                {
                    var response = webException.Response;
                    using (Stream s1 = response.GetResponseStream())
                    {
                        if (s1 != null)
                        {
                            using (StreamReader sr = new StreamReader(s1))
                            {
                                var errorResult = sr.ReadToEnd();

                                if (IsServerUnavailable(errorResult))
                                {
                                    throw new ServerUnavailableException();
                                }
                                
                                string message = "Error connecting to server with the following url: " + url + "\n\n" + errorResult;
                                _log.Error(message, webException);
                                throw new SosException(message, webException);
                            }
                        }
                    }
                }
                if (webException.Status == WebExceptionStatus.Timeout)
                {
                    throw new ServerUnavailableException();
                }
                if (webException.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    throw new ServerUnavailableException();
                }
                if (webException.Status == WebExceptionStatus.ConnectFailure)
                {
                    throw new ServerUnavailableException();
                }
                
                _log.Error("Error connecting to " + url + ". WebException.Status = " + webException.Status);
                throw;
            }
        }
    }
}
