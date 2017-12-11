﻿using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Xml.Linq;
using SirenOfShame.Lib.Exceptions;
using log4net;

namespace SirenOfShame.Lib.Watcher
{
    public enum AuthenticationTypeEnum
    {
        BasicAuth = 0,
        Base64EncodeInHeader = 1
    }
    
    public class WebClientXml
    {
        public WebClientXml()
        {
            AuthenticationType = AuthenticationTypeEnum.BasicAuth;
        }

        private static readonly ILog _log = MyLogManager.GetLogger(typeof(WebClientXml));
        private readonly NameValueCollection _data = new NameValueCollection();
        
        private static readonly string[] _serverUnavailableTriggers =
        {
            "HTTP Status 404",
            "Connection timed out",
            "Please wait while Jenkins is getting ready to work",
            "The remote server returned an error: (503) Server Unavailable",
        };

        private static readonly string[] _buildDefinitionUnavailableTriggers =
        {
            "No build type or template is found by id, internal id or name",
        };

        public AuthenticationTypeEnum AuthenticationType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Cookie { get; set; }

        private string Base64Encode(string data)
        {
            try
            {
                byte[] encDataByte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encDataByte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        protected static bool IsServerUnavailable(string errorResult)
        {
            return _serverUnavailableTriggers.Any(errorResult.Contains);
        }

        protected static bool IsBuildDefinitionUnavailable(string errorResult)
        {
            return _buildDefinitionUnavailableTriggers.Any(errorResult.Contains);
        }

        public void DownloadXmlAsync(string url, Action<XDocument> onSuccess = null, Action<Exception> onError = null)
        {
            var webClient = GetWebClient(UserName, Password, Cookie);
            try
            {
                webClient.DownloadStringCompleted += (sender, args) =>
                {
                    try
                    {
                        XDocument doc = XDocument.Parse(args.Result);
                        if (doc.Root == null)
                        {
                            if (onError != null) onError(new Exception("No results returned"));
                        }
                        if (onSuccess != null) onSuccess(doc);
                    } 
                    catch (Exception ex)
                    {
                        if (onError != null) onError(ex);
                    }
                };
                webClient.DownloadStringAsync(new Uri(url));
            }
            catch (WebException webException)
            {
                throw ToServerUnavailableException(url, webException);
            }
        }

        public XDocument DownloadXml(string url, string userName, string password, string cookie = null)
        {
            var webClient = GetWebClient(userName, password, cookie);

            try
            {
                var resultString = webClient.DownloadString(url);
                return TryParseXmlResult(url, resultString);
            } catch (WebException webException)
            {
                if (webException.Message.Contains("401"))
                {
                    throw new InvalidCredentialsException();
                }
                if (url.Contains("httpAuth/app/rest/builds/buildType:"))
                {
                    _log.Error(webException.Message, webException);
                    return null;
                }

                throw ToServerUnavailableException(url, webException);
            }
        }

        private WebClient GetWebClient(string userName, string password, string cookie)
        {
            var webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password),
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore),
                Encoding = Encoding.UTF8
            };

            if (AuthenticationType == AuthenticationTypeEnum.BasicAuth && !string.IsNullOrEmpty(userName))
            {
                webClient.Credentials = new NetworkCredential(userName, password);
            }
            if (AuthenticationType == AuthenticationTypeEnum.Base64EncodeInHeader && !string.IsNullOrEmpty(userName))
            {
                string base64String = Base64Encode(userName + ":" + password);
                webClient.Headers.Add("Authorization", "Basic " + base64String);
            }

            if (cookie != null)
            {
                webClient.Headers.Add("Cookie", cookie);
            }
            
            return webClient;
        }

        private static XDocument TryParseXmlResult(string url, string resultString)
        {
            try
            {
                return XDocument.Parse(resultString);
            } catch (Exception ex)
            {
                string message = "Couldn't parse XML when trying to connect to " + url + ":\n" + resultString;
                _log.Error(message, ex);
                throw new ServerUnavailableException(message, ex);
            }
        }

        public static Exception ToServerUnavailableException(string url, WebException webException)
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
                            return ToServerUnavailableException(url, webException, errorResult);
                        }
                    }
                }
            }
            if (webException.Status == WebExceptionStatus.Timeout)
            {
                return new ServerUnavailableException();
            }
            if (webException.Status == WebExceptionStatus.NameResolutionFailure)
            {
                return new ServerUnavailableException();
            }
            if (webException.Status == WebExceptionStatus.ConnectFailure)
            {
                return new ServerUnavailableException();
            }

            return new ServerUnavailableException("Server unavailable: " + webException.Status.ToString(), webException);
        }

        public static Exception ToServerUnavailableException(string url, WebException webException, string errorResult)
        {
            if (IsServerUnavailable(errorResult))
            {
                return new ServerUnavailableException();
            }

            if (IsBuildDefinitionUnavailable(errorResult))
            {
                return new BuildDefinitionNotFoundException();
            }

            string message = "Error connecting to server with the following url: " + url + "\n\n" + errorResult;
            _log.Error(message, webException);
            return new ServerUnavailableException(message, webException);
        }

        public void Add(string name, string value)
        {
            _data[name] = value;
        }

        public void UploadValuesAndReturnXmlAsync(
            string url,
            Action<XDocument> action,
            Action<Exception> onConnectionFail,
            IWebProxy proxy = null)
        {
            UploadValuesAndReturnStringAsync(url, resultAsStr =>
            {
                XDocument doc = TryParseXmlResult(url, resultAsStr);
                action(doc);
            }, onConnectionFail, proxy);
        }

        public void UploadValuesAndReturnStringAsync(string url, Action<string> action, Action<Exception> onConnectionFail, IWebProxy proxy = null)
        {
            WebClient webClient = new WebClient();
            if (proxy != null)
                webClient.Proxy = proxy;
            webClient.UploadValuesCompleted += (s, uploadEventArgs) =>
            {
                try
                {
                    // todo: more error handeling when authenticating
                    byte[] result = uploadEventArgs.Result;
                    string resultAsStr = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
                    action(resultAsStr);
                } 
                catch (WebException ex)
                {
                    var serverUnavailableException = ToServerUnavailableException(url, ex);
                    onConnectionFail(serverUnavailableException);
                } 
                catch (ServerUnavailableException ex)
                {
                    onConnectionFail(ex);
                }
                catch (Exception ex)
                {
                    WebException innerException = ex.InnerException as WebException;
                    if (innerException != null)
                    {
                        var serverUnavailableException = ToServerUnavailableException(url, innerException);
                        onConnectionFail(serverUnavailableException);
                        return;
                    }
                    _log.Error("Error in UploadValuesAndReturnXmlAsync", ex);
                    throw;
                }
            };
            webClient.UploadValuesAsync(new Uri(url), "POST", _data);
        }
    }
}
