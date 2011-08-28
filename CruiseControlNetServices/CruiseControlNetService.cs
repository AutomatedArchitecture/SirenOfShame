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
using log4net;

namespace CruiseControlNetServices
{
    public class CruiseControlNetService
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(CruiseControlNetService));

        public delegate void GetProjectsCompleteDelegate(CruiseControlNetBuildDefinition[] buildDefinitions);

        public void GetProjects(string rootUrl, string userName, string password, GetProjectsCompleteDelegate complete, Action<Exception> onError)
        {
            WebClient webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password)
            };
            rootUrl = GetRootUrl(rootUrl);
            var projectUrl = new Uri(rootUrl + "/XmlStatusReport.aspx");
            webClient.DownloadStringCompleted += (s, e) =>
            {
                try
                {
                    XDocument doc = XDocument.Parse(e.Result);
                    if (doc.Root == null) throw new Exception("Could not get project list");
                    var projectElems = doc.Root.Elements("Project");
                    CruiseControlNetBuildDefinition[] buildDefinitions = projectElems
                        .Select(planXml => new CruiseControlNetBuildDefinition(rootUrl, planXml))
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

        public IList<CruiseControlNetBuildStatus> GetBuildsStatuses(string rootUrl, string userName, string password, BuildDefinitionSetting[] watchedBuildDefinitions)
        {
            var buildStatuses = GetBuildStatuses(rootUrl, userName, password).ToList();
            var results = new List<CruiseControlNetBuildStatus>();
            foreach (var watchedBuildDefinition in watchedBuildDefinitions)
            {
                var buildStatus = buildStatuses.FirstOrDefault(s => s.BuildDefinitionId == watchedBuildDefinition.Id);
                if (buildStatus == null)
                {
                    throw new BuildDefinitionNotFoundException(watchedBuildDefinition);
                }
                results.Add(buildStatus);
            }
            return results;
        }

        private IEnumerable<CruiseControlNetBuildStatus> GetBuildStatuses(string rootUrl, string userName, string password)
        {
            rootUrl = GetRootUrl(rootUrl);
            var url = new Uri(rootUrl + "/XmlStatusReport.aspx");
            var doc = DownloadXml(url.ToString(), userName, password);
            if (doc.Root == null) throw new Exception("Could not get project list");
            var projectElems = doc.Root.Elements("Project");
            var buildStatuses = projectElems.Select(projectElem => new CruiseControlNetBuildStatus(projectElem));
            return buildStatuses;
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
