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

namespace BambooServices
{
    public class BambooService : ServiceBase
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
            BambooBuildDefinition[] buildDefinitions = new BambooBuildDefinition[] { };
            //This loop may be run multiple times
            webClient.DownloadStringCompleted += (s, e) =>
            {
                try
                {
                    XDocument doc = XDocument.Parse(e.Result);
                    if (doc.Root == null) throw new Exception("Could not get project list");
                    var plansElem = doc.Root.Element("plans");
                    if (plansElem == null) throw new Exception("Could not get plans element");
                    var plansElements = doc.Descendants("plans")
                        .Where(i => i.Attribute("size") != null)
                        .ToArray();
                    if (plansElements.Length != 1) throw new Exception("Retrieved " + plansElements.Length + " plans when 1 was expected");
                    XElement firstPlan = plansElements[0];
                    int startIndex = firstPlan.AttributeValueAsInt("start-index");
                    int size = firstPlan.AttributeValueAsInt("size");
                    int maxResults = firstPlan.AttributeValueAsInt("max-result");

                    BambooBuildDefinition[] buildPlans = plansElem
                        .Elements("plan")
                        .Select(planXml => new BambooBuildDefinition(rootUrl, planXml))
                        .ToArray();

                    buildDefinitions = buildDefinitions.Concat(buildPlans).ToArray();
                    bool moreResultsInBatch = (startIndex + maxResults) < size;
                    if (moreResultsInBatch)
                        webClient.DownloadStringAsync(new Uri(projectUrl.ToString() + "&start-index=" + (startIndex + maxResults)));
                    else
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
                    string buildUrl = rootUrl + "/rest/api/latest/result/" + buildDefinitionSetting.Id + "-latest?expand=changes.change&os_authType=basic";
                    buildDoc = DownloadXml(buildUrl, userName, password);
                }
                catch (Exception)
                {
                    string buildUrl = rootUrl + "/rest/api/latest/build/" + buildDefinitionSetting.Id + "-latest?expand=changes.change&os_authType=basic";
                    buildDoc = DownloadXml(buildUrl, userName, password);
                }
                if (buildDoc.Root == null) throw new Exception("Could not get project status");
                return BambooBuildStatus.CreateBuildResult(buildDoc, buildDefinitionSetting, rootUrl);
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
    }
}
