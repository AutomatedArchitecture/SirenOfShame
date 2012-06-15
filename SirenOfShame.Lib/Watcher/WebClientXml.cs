using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Xml.Linq;
using SirenOfShame.Lib.Exceptions;
using log4net;

namespace SirenOfShame.Lib.Watcher
{
    public class WebClientXml
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(WebClientXml));
        private NameValueCollection _data = new NameValueCollection();
        
        private static readonly string[] _serverUnavailableTriggers = new[]
            {
                "HTTP Status 404",
                "Connection timed out",
                "Please wait while Jenkins is getting ready to work",
                "The remote server returned an error: (503) Server Unavailable",
            };

        protected virtual bool IsServerUnavailable(string errorResult)
        {
            return _serverUnavailableTriggers.Any(errorResult.Contains);
        }
        
        public XDocument DownloadXml(string url, string userName, string password, string cookie = null)
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
                    throw new ServerUnavailableException(message, ex);
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
                                throw new ServerUnavailableException(message, webException);
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

        public void Add(string name, string value)
        {
            _data[name] = value;
        }

        public void UploadValuesAndReturnXmlAsync(string url, Action<XDocument> action)
        {
            WebClient webClient = new WebClient();
            webClient.UploadValuesCompleted += (s, uploadEventArgs) =>
            {
                // todo: more error handeling when authenticating
                byte[] result = uploadEventArgs.Result;
                string resultAsStr = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
                XDocument doc = XDocument.Parse(resultAsStr);
                action(doc);
            };
            webClient.UploadValuesAsync(new Uri(url), "POST", _data);
        }
    }
}
