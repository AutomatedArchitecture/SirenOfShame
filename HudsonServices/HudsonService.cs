using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace HudsonServices
{
    public class HudsonService : ServiceBase
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
                if (lastBuildElem == null)
                {
                    throw new ServerUnavailableException("No 'lastBuild' element found for " + buildDefinitionSetting + " is the server in maintenence mode or something?");
                }
                var buildNumber = lastBuildElem.ElementValueOrDefault("number");
                var buildUrl = rootUrl + "/job/" + buildDefinitionSetting.Id + "/" + buildNumber;
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
    }
}
