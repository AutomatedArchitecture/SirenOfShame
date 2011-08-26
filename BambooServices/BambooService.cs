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

namespace BambooServices
{
    public class BambooService
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(BambooService));

        public delegate void GetProjectsCompleteDelegate(BambooBuildDefinition[] buildDefinitions);

        public void GetProjects(string rootUrl, string userName, string password, GetProjectsCompleteDelegate complete, Action<Exception> onError)
        {
            WebClient webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password)
            };
            rootUrl = GetRootUrl(rootUrl);
            var projectUrl = new Uri(rootUrl + "/rest/api/latest/plan?os_authType=basic");
            webClient.DownloadStringCompleted += (s, e) =>
            {
                try
                {
                    XDocument doc = XDocument.Parse(e.Result);
                    if (doc.Root == null) throw new Exception("Could not get project list");
                    var plansElem = doc.Root.Element("plans");
                    if (plansElem == null) throw new Exception("Could not get plans element");
                    BambooBuildDefinition[] buildDefinitions = plansElem
                        .Elements("plan")
                        .Select(planXml => new BambooBuildDefinition(rootUrl, planXml))
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

        public IList<BambooBuildStatus> GetBuildsStatuses(string rootUrl, string userName, string password, BuildDefinitionSetting[] watchedBuildDefinitions)
        {
            rootUrl = GetRootUrl(rootUrl);

            var parallelResult = from buildDefinitionSetting in watchedBuildDefinitions
                                 select GetBuildStatus(rootUrl, buildDefinitionSetting, userName, password);
            return parallelResult.AsParallel().ToList();
        }

        private BambooBuildStatus GetBuildStatus(string rootUrl, BuildDefinitionSetting buildDefinitionSetting, string userName, string password)
        {
            try
            {
                string planUrl = rootUrl + "/rest/api/latest/plan/" + buildDefinitionSetting.Id + "?os_authType=basic";
                XDocument planDoc = DownloadXml(planUrl, userName, password);
                var isBuilding = planDoc.Root.ElementValueAsBool("isBuilding", false);
                if (isBuilding != null && isBuilding.Value)
                {
                    return BambooBuildStatus.CreateIsBuilding(buildDefinitionSetting);
                }

                XDocument buildDoc;
                try
                {
                    string buildUrl = rootUrl + "/rest/api/latest/result/" + buildDefinitionSetting.Id + "?expand=results[0].result&os_authType=basic";
                    buildDoc = DownloadXml(buildUrl, userName, password);
                }
                catch (Exception ex)
                {
                    string buildUrl = rootUrl + "/rest/api/latest/build/" + buildDefinitionSetting.Id + "?expand=builds[0].build&os_authType=basic";
                    buildDoc = DownloadXml(buildUrl, userName, password);
                }
                if (buildDoc.Root == null) throw new Exception("Could not get project status");
                return BambooBuildStatus.CreateBuildResult(buildDoc, buildDefinitionSetting);
            }
            catch (SosException ex)
            {
                if (ex.Message.Contains("not found"))
                {
                    throw new BuildDefinitionNotFoundException(buildDefinitionSetting);
                }
                throw;
            }
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
