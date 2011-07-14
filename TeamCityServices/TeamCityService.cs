using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using SirenOfShame.Lib.Exceptions;
using log4net;
using SirenOfShame.Lib;

namespace TeamCityServices
{
    // http://confluence.jetbrains.net/display/TW/REST+API+Plugin
    public class TeamCityService
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(TeamCityService));

        public delegate void GetProjectsCompleteDelegate(TeamCityProject[] projects);

        public delegate void GetBuildDefinitionsCompleteDelegate(TeamCityBuildDefinition[] buildDefinitions);

        public delegate void GetBuildStatusCompleteDelegate(TeamCityBuildStatus buildStatus);

        public void GetProjects(string rootUrl, string userName, string password, GetProjectsCompleteDelegate complete, Action<Exception> onError){
            WebClient webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password)
            };
            rootUrl = GetRootUrl(rootUrl);
            var projectUrl = new Uri(rootUrl + "/httpAuth/app/rest/projects");
            webClient.DownloadStringCompleted += (s, e) =>
                {
                    try
                    {
                        XDocument doc = XDocument.Parse(e.Result);
                        if (doc.Root == null)
                        {
                            throw new Exception("Could not get project list");
                        }
                        TeamCityProject[] projects = doc.Root
                            .Elements("project")
                            .Select(projectXml => new TeamCityProject(rootUrl, projectXml))
                            .ToArray();
                        complete(projects);
                    } catch (Exception ex)
                    {
                        _log.Error("Error connecting to server", ex);
                        onError(ex);
                    }
                };
            webClient.DownloadStringAsync(projectUrl);
        }

        private string GetRootUrl(string rootUrl)
        {
            rootUrl = rootUrl.TrimEnd('/');
            return rootUrl;
        }

        public void GetBuildDefinitions(TeamCityProject project, string userName, string password, GetBuildDefinitionsCompleteDelegate complete)
        {
            WebClient webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password)
            };
            var projectDetailsUrl = new Uri(project.RootUrl + project.Href);
            webClient.DownloadStringCompleted += (s, e) =>
            {
                XDocument doc = XDocument.Parse(e.Result);
                if (doc.Root == null)
                {
                    throw new Exception("Could not get project build definitions");
                }
                XElement buildTypes = doc.Root.Element("buildTypes");
                if (buildTypes == null)
                {
                    throw new Exception("Could not get project build definitions");
                }
                TeamCityBuildDefinition[] projects = buildTypes
                    .Elements("buildType")
                    .Select(buildTypeXml => new TeamCityBuildDefinition(project.RootUrl, buildTypeXml))
                    .ToArray();
                complete(projects);
            };
            webClient.DownloadStringAsync(projectDetailsUrl);
        }

        public void GetBuildStatus(string rootUrl, string buildDefinitionId, string userName, string password, GetBuildStatusCompleteDelegate complete, Action<Exception> onError)
        {
            WebClient webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password)
            };

            rootUrl = GetRootUrl(rootUrl);
            var buildStatusUrl = new Uri(rootUrl + "/httpAuth/app/rest/builds/buildType:" + buildDefinitionId);

            webClient.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    WebException webException = e.Error as WebException;
                    if (webException == null || webException.Response == null)
                    {
                        _log.Error("Error connecting to server", e.Error);
                        onError(e.Error);
                    } 
                    else 
                    {
                        var response = webException.Response;
                        using (Stream s1 = response.GetResponseStream())
                        using (StreamReader sr = new StreamReader(s1))
                        {
                            var result = sr.ReadToEnd();
                            _log.Error(result, webException);
                            onError(new SosException(result, e.Error));
                        }
                    }
                    return;
                }

                try
                {
                    XDocument doc = XDocument.Parse(e.Result);
                    if (doc.Root == null)
                    {
                        throw new Exception("Could not get project build status");
                    }
                    complete(new TeamCityBuildStatus(buildDefinitionId, doc));
                } catch (Exception ex)
                {
                    _log.Error("Error connecting to team city.", ex);
                    onError(ex);
                }
            };
            webClient.DownloadStringAsync(buildStatusUrl);
        }
    }
}
