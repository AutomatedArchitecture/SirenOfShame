﻿using log4net;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamCityServices
{
    // http://confluence.jetbrains.net/display/TW/REST+API+Plugin
    public class TeamCityService : ServiceBase
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(TeamCityService));

        public async Task<TeamCityProject[]> GetProjects(string rootUrl, string userName, string password)
        {
            WebClient webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password)
            };
            rootUrl = GetRootUrl(rootUrl);
            var projectUrl = new Uri(rootUrl + "/httpAuth/app/rest/projects");
            var projectListAsString = await webClient.DownloadStringTaskAsync(projectUrl);
            XDocument projectsDoc = XDocument.Parse(projectListAsString);
            if (projectsDoc.Root == null)
            {
                throw new Exception("Could not get project list");
            }
            TeamCityProject[] projects = projectsDoc.Root
                .Elements("project")
                .Select(projectXml => new TeamCityProject(rootUrl, projectXml))
                .ToArray();

            foreach (var project in projects)
            {
                await LoadProjectInfoFromApi(project, webClient);
            }

            var parentOnlyNodes = RecreateTreeOfProjects(projects);

            return parentOnlyNodes;
        }

        private TeamCityProject[] RecreateTreeOfProjects(TeamCityProject[] allProjects)
        {
            var roots = allProjects.Where(i => i.ParentProjectId == null).ToArray();
            foreach (var project in roots)
            {
                RecreateTreeOfProjects(project, allProjects);
            }
            return roots;
        }

        private void RecreateTreeOfProjects(TeamCityProject project, TeamCityProject[] allProjects)
        {
            project.SubProjects = allProjects.Where(i => i.ParentProjectId == project.Id).ToList();
            foreach (var subProject in project.SubProjects)
            {
                RecreateTreeOfProjects(subProject, allProjects);
            }
        }

        private static async Task LoadProjectInfoFromApi(TeamCityProject project, WebClient webClient)
        {
            try
            {
                var projectDetailsUrl = new Uri(project.RootUrl + project.Href);
                _log.Debug("Retrieving project info for " + project.Name + " at " + projectDetailsUrl);
                var projectDetailsStr = await webClient.DownloadStringTaskAsync(projectDetailsUrl);
                XDocument projectDoc = XDocument.Parse(projectDetailsStr);
                if (projectDoc.Root == null)
                {
                    throw new Exception("Could not get project build definitions");
                }

                var parentProjectElement = projectDoc.Root.Element("parentProject");
                if (parentProjectElement != null)
                {
                    project.ParentProjectId = parentProjectElement.Attribute("id").Value;
                }

                XElement buildTypes = projectDoc.Root.Element("buildTypes");
                if (buildTypes == null)
                    throw new ArgumentException(string.Format("buildTypes was null for {0}, this shouldn't happen", project.Name));
                project.BuildDefinitions = buildTypes
                    .Elements("buildType")
                    .Select(buildTypeXml => new TeamCityBuildDefinition(project.RootUrl, buildTypeXml))
                    .ToList();
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Error parsing project info for project {0} ({1})", project.Name, project.Id), ex);
                throw;
            }
        }

        private string GetRootUrl(string rootUrl)
        {
            if (string.IsNullOrEmpty(rootUrl)) return null;
            rootUrl = rootUrl.TrimEnd('/');
            return rootUrl;
        }

        private static bool _supportsGetLatestBuildByBuildTypeId = true;

        private static readonly Dictionary<string, string> _cookies = new Dictionary<string, string>();

        private string GetCookie(string rootUrl)
        {
            string cookie;
            _cookies.TryGetValue(rootUrl, out cookie);
            return cookie;
        }

        public IEnumerable<TeamCityBuildStatus> GetBuildsStatuses(string rootUrl, string userName, string password, IEnumerable<BuildDefinitionSetting> watchedBuildDefinitions)
        {
            rootUrl = GetRootUrl(rootUrl);

            XDocument document = DownloadXml(rootUrl + "/httpAuth/app/rest/builds?locator=running:true", userName, password, GetCookie(rootUrl));

            var inProgressBuilds = document.Descendants("builds").First().Descendants("build").Select(b => new
            {
                buildTypeId = b.AttributeValueOrDefault("buildTypeId"),
                buildId = b.AttributeValueOrDefault("id")
            }).ToArray();

            var parallelResult = from buildDefinitionSetting in watchedBuildDefinitions
                                 from inProgressBuild in inProgressBuilds.Where(b => b.buildTypeId == buildDefinitionSetting.Id).DefaultIfEmpty()
                                 select inProgressBuild != null ?
                                    GetBuildStatusByBuildId(rootUrl, userName, password, buildDefinitionSetting, inProgressBuild.buildId)
                                    : GetBuildStatus(rootUrl, buildDefinitionSetting, userName, password);
            return parallelResult.AsParallel().ToList();
        }

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetGetCookieEx(
            string url,
            string cookieName,
            StringBuilder cookieData,
            ref int size,
            Int32 dwFlags,
            IntPtr lpReserved);

        private const Int32 INTERNET_COOKIE_HTTPONLY = 0x2000;
        /// <summary>
        /// Gets the URI cookie container.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        private static string GetUriCookieContainer(Uri uri)
        {
            // Determine the size of the cookie
            int datasize = 8192 * 16;
            StringBuilder cookieData = new StringBuilder(datasize);
            if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref datasize, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero))
            {
                if (datasize < 0)
                    return null;
                // Allocate stringbuilder large enough to hold the cookie
                cookieData = new StringBuilder(datasize);
                if (!InternetGetCookieEx(
                    uri.ToString(),
                    null, cookieData,
                    ref datasize,
                    INTERNET_COOKIE_HTTPONLY,
                    IntPtr.Zero))
                    return null;
            }
            return cookieData.ToString();
        }

        private TeamCityBuildStatus GetBuildStatus(string rootUrl, BuildDefinitionSetting buildDefinitionSetting, string userName, string password)
        {
            rootUrl = GetRootUrl(rootUrl);

            // older versions of team city don't support this format: httpAuth/app/rest/builds/buildType:bt2
            //  but that format requires far fewer http requests
            if (!_supportsGetLatestBuildByBuildTypeId)
            {
                // TeamCity 5.X
                return GetLatestBuildByBuildId(rootUrl, userName, password, buildDefinitionSetting);
            }
            // TeamCity 6.X
            return GetLatestBuildByBuildTypeId(rootUrl, userName, password, buildDefinitionSetting);
        }

        private TeamCityBuildStatus GetLatestBuildByBuildId(string rootUrl, string userName, string password, BuildDefinitionSetting buildDefinitionSetting)
        {
            string getLatestBuildIdByBuildTypeUrl = rootUrl + "/httpAuth/app/rest/buildTypes/" + buildDefinitionSetting.Id + "/builds?count=1";
            try
            {
                XDocument latestBuildIdXDoc = DownloadXml(getLatestBuildIdByBuildTypeUrl, userName, password);
                var id = latestBuildIdXDoc.Descendants("build").Attributes("id").First().Value;
                return GetBuildStatusByBuildId(rootUrl, userName, password, buildDefinitionSetting, id);
            }
            catch (SosException ex)
            {
                if (ex.Message.Contains("No build type is found by id "))
                {
                    throw new BuildDefinitionNotFoundException(buildDefinitionSetting);
                }
                throw;
            }
        }

        private TeamCityBuildStatus GetBuildStatusByBuildId(
            string rootUrl,
            string userName,
            string password,
            BuildDefinitionSetting buildDefinitionSetting,
            string buildId)
        {

            var getBuildByBuildIdIdUrl = rootUrl + "/httpAuth/app/rest/builds/id:" + buildId + "?fields=$long,changes(count,href)";
            XDocument buildResultXDoc = DownloadXml(getBuildByBuildIdIdUrl, userName, password);
            return GetBuildStatusAndCommentsFromXDocument(rootUrl, userName, password, buildDefinitionSetting, buildResultXDoc);
        }

        protected TeamCityBuildStatus GetBuildStatusAndCommentsFromXDocument(
            string rootUrl,
            string userName,
            string password,
            BuildDefinitionSetting buildDefinitionSetting,
            XDocument buildResultXDoc)
        {
            XDocument changeResultXDoc = null;
            try
            {
                if (buildResultXDoc.Root == null) throw new Exception("Could not get project build status");
                var changesNode = buildResultXDoc.Descendants("changes").FirstOrDefault();
                if (changesNode == null)
                {
                    var title = buildResultXDoc.Descendants("title").FirstOrDefault();
                    if (title != null && title.Value.StartsWith("Cleanup in progress"))
                        throw new ServerUnavailableException("Cleanup in progress");
                    _log.Error("There was no changes element in the following XML: " + buildResultXDoc);
                    return new TeamCityBuildStatus(buildDefinitionSetting) { BuildStatusEnum = BuildStatusEnum.Unknown };
                }
                var count = changesNode.AttributeValueOrDefault("count");
                bool commentsExist = !string.IsNullOrEmpty(count) && count != "0";
                if (commentsExist)
                {
                    var changesHref = changesNode.AttributeValueOrDefault("href");
                    var changesUrl = rootUrl + changesHref;
                    XDocument changesResultXDoc = DownloadXml(changesUrl, userName, password);
                    var changeNode = changesResultXDoc.Descendants("change").FirstOrDefault();
                    if (changeNode == null)
                    {
                        _log.Debug("No change node found");
                    }
                    else
                    {
                        var changeHref = changeNode.AttributeValueOrDefault("href");
                        var changeUrl = rootUrl + changeHref;
                        changeResultXDoc = DownloadXml(changeUrl, userName, password);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error parsing xml. BuildResultXDoc: " + buildResultXDoc + "\r\n\r\n ChangeResultXDoc: " + changeResultXDoc, ex);
                throw;
            }
            return new TeamCityBuildStatus(buildDefinitionSetting, buildResultXDoc, changeResultXDoc);
        }

        private TeamCityBuildStatus GetLatestBuildByBuildTypeId(string rootUrl, string userName, string password,
            BuildDefinitionSetting buildDefinitionSetting)
        {
            var url = rootUrl + "/httpAuth/app/rest/builds/buildType:" + buildDefinitionSetting.Id + "?fields=$long,changes(count,href)";
            try
            {
                XDocument doc = DownloadXml(url, userName, password);
                if (doc == null)
                {
                    _log.ErrorFormat("Could not get project build status for {0}", url);
                    return new TeamCityBuildStatus(buildDefinitionSetting);
                }
                if (doc.Root == null)
                {
                    throw new Exception("Could not get project build status");
                }

                return GetBuildStatusAndCommentsFromXDocument(
                    rootUrl,
                    userName,
                    password,
                    buildDefinitionSetting,
                    doc);
            }
            catch (BuildDefinitionNotFoundException)
            {
                return new TeamCityBuildStatus(buildDefinitionSetting)
                {
                    BuildStatusEnum = BuildStatusEnum.Unknown,
                    Comment = "[unable to connect to " + buildDefinitionSetting.Id + "]",
                };
            }
            catch (SosException ex)
            {
                if (ex.Message.Contains("BadRequestException: Cannot find build by other locator then 'id' without build type specified."))
                {
                    _log.Debug("_supportsGetLatestBuildByBuildTypeId = false");
                    _supportsGetLatestBuildByBuildTypeId = false;
                    return GetLatestBuildByBuildId(rootUrl, userName, password, buildDefinitionSetting);
                }
                if (ex.Message.Contains("No build type is found by id "))
                {
                    throw new BuildDefinitionNotFoundException(buildDefinitionSetting);
                }
                throw;
            }
        }
    }
}
