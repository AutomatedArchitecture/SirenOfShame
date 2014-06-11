using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Lib.Helpers;
using log4net;

namespace CruiseControlNetServices
{
    public class CruiseControlNetService : ServiceBase
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
            IEnumerable<XElement> projectElems = doc.Root.Elements("Project");

            List<CruiseControlNetBuildStatus> results = new List<CruiseControlNetBuildStatus>();
            if (projectElems != null)
            {
                foreach (XElement projectElem in projectElems)
                {
                    try
                    {
                        var name = projectElem.AttributeValue("name");
                        var lastBuildTimeStr = projectElem.AttributeValueOrDefault("lastBuildTime");
                        string parsedBuildTime = CruiseControlNetBuildStatus.ParseCruiseControlDateToId(lastBuildTimeStr);
                        string lastBuildNumber = projectElem.AttributeValueOrDefault("lastBuildLabel");
                        string lastBuildStatus = projectElem.AttributeValueOrDefault("lastBuildStatus");

                        var XMLUrl = lastBuildStatus != null && lastBuildStatus.Equals("Success") ?
                            string.Format("{0}/server/local/project/{1}/build/log{2}Lbuild.{3}.xml/XmlBuildLog.aspx", rootUrl, name, parsedBuildTime, lastBuildNumber) :
                            string.Format("{0}/server/local/project/{1}/build/log{2}.xml/XmlBuildLog.aspx", rootUrl, name, parsedBuildTime);

                        var buildLog = DownloadXml(XMLUrl.ToString(), userName, password);
                        XElement buildLogModificationElems = buildLog.Root == null ? null : buildLog.Root.Element("modifications");

                        results.Add(new CruiseControlNetBuildStatus(projectElem, buildLogModificationElems));
                    }
                    catch (Exception ex)
                    {
                        // Swallow
                    }
                }
            }

            return results;
        }
    }
}
