using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Xml.Linq;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace HudsonServices
{
    public class HudsonService
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(HudsonService));

        public delegate void GetProjectsCompleteDelegate(HudsonBuildDefinition[] buildDefinitions);

        public void GetProjects(string rootUrl, string userName, string password, GetProjectsCompleteDelegate complete, Action<Exception> onError)
        {
            WebClient webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password)
            };
            rootUrl = GetRootUrl(rootUrl);
            var projectUrl = new Uri(rootUrl + "/api/xml");
            webClient.DownloadStringCompleted += (s, e) =>
            {
                try
                {
                    XDocument doc = XDocument.Parse(e.Result);
                    if (doc.Root == null)
                    {
                        throw new Exception("Could not get project list");
                    }
                    HudsonBuildDefinition[] buildDefinitions = doc.Root
                        .Elements("job")
                        .Select(projectXml => new HudsonBuildDefinition(rootUrl, projectXml))
                        .ToArray();
                    complete(buildDefinitions);
                }
                catch (Exception ex)
                {
                    _log.Error("Error connecting to server", ex);
                    onError(ex);
                }
            };
            webClient.DownloadStringAsync(projectUrl);
        }

        private string GetRootUrl(string rootUrl)
        {
            if (string.IsNullOrEmpty(rootUrl)) return null;
            rootUrl = rootUrl.TrimEnd('/');
            return rootUrl;
        }

        public IList<HudsonBuildStatus> GetBuildsStatuses(string rootUrl, string userName, string password, BuildDefinitionSetting[] watchedBuildDefinitions)
        {
            rootUrl = GetRootUrl(rootUrl);

            var parallelResult = from buildDefinitionSetting in watchedBuildDefinitions
                                 select GetBuildStatus(rootUrl, buildDefinitionSetting, userName, password);
            return parallelResult.AsParallel().ToList();
        }

        private HudsonBuildStatus GetBuildStatus(string rootUrl, BuildDefinitionSetting buildDefinitionSetting, string userName, string password)
        {
            string url = rootUrl + "/job/" + buildDefinitionSetting.Id + "/api/xml";
            try
            {
                XDocument doc = DownloadXml(url, userName, password);
                if (doc.Root == null) throw new Exception("Could not get project status");
                var lastBuildElem = doc.Root.Element("lastBuild");
                if (lastBuildElem == null) throw new Exception("No builds");
                var buildUrl = lastBuildElem.ElementValueOrDefault("url");
                if (string.IsNullOrWhiteSpace(buildUrl)) throw new Exception("Could not get build url");
                buildUrl += "/api/xml";
                doc = DownloadXml(buildUrl, userName, password);
                if (doc.Root == null) throw new Exception("Could not get project build status");
                return GetBuildStatusAndCommentsFromXDocument(rootUrl, userName, password, buildDefinitionSetting, doc);
            }
            catch (SosException ex)
            {
                if (ex.Message.Contains("NOT_FOUND"))
                {
                    throw new BuildDefinitionNotFoundException(buildDefinitionSetting);
                }
                throw;
            }
        }

        private HudsonBuildStatus GetBuildStatusAndCommentsFromXDocument(IComparable rootUrl, string userName, string password, BuildDefinitionSetting buildDefinitionSetting, XDocument doc)
        {
            var status = new HudsonBuildStatus(doc, buildDefinitionSetting);
            return status;
        }

        private static XDocument DownloadXml(string url, string userName, string password)
        {
            var webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password),
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore),
            };

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
                                if (errorResult.Contains("Please wait while Jenkins is getting ready to work"))
                                {
                                    throw new ServerUnavailableException("Jenkins is starting up");
                                }
                                string message = "Error connecting to server with the following url: " + url + "\n\n" + errorResult;
                                _log.Error(message, webException);
                                throw new SosException(message, webException);
                            }
                        }
                    }
                }
                throw;
            }
        }
    }
}
