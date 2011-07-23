using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
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

        private static bool _supportsGetLatestBuildByBuildTypeId = true;
        private static string _cookie = null;

        public List<TeamCityBuildStatus> GetBuildsStatuses(string rootUrl, string userName, string password, BuildDefinitionSetting[] watchedBuildDefinitions)
        {
            rootUrl = GetRootUrl(rootUrl);

            if (_cookie == null)
            {
                SetCookie(rootUrl, userName, password);
            }

            WebClient webClient = new WebClient { CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) };
            webClient.Headers.Add("Cookie", _cookie);
            string result = webClient.DownloadString(new Uri(rootUrl + "/ajax.html?getRunningBuilds=1"));

            XDocument document = XDocument.Parse(result);
            var inProgressBuilds = document.Descendants("build").Select(b => new
            {
                buildTypeId = b.AttributeValueOrDefault("buildTypeId"),
                buildId = b.AttributeValueOrDefault("buildId")
            }).ToArray();

            var parallelResult = from buildDefinitionSetting in watchedBuildDefinitions
                                 from inProgressBuild in inProgressBuilds.Where(b => b.buildTypeId == buildDefinitionSetting.Id).DefaultIfEmpty()
                                 select inProgressBuild != null ?
                                    GetBuildStatusByBuildId(rootUrl, userName, password, buildDefinitionSetting, inProgressBuild.buildId)
                                    : GetBuildStatus(rootUrl, buildDefinitionSetting, userName, password);
            return parallelResult.AsParallel().ToList();
        }

        private static void SetCookie(string rootUrl, string userName, string password)
        {
            int state = 0;
            bool serverUnavailable = false;
            // WebBrowser needs to run in a single threaded apartment thread, see http://www.beansoftware.com/ASP.NET-Tutorials/Get-Web-Site-Thumbnail-Image.aspx
            Thread staThread = new Thread(() =>
            {
                // ajax.html is required to get in progress builds in TC 5.X, ajax.html requires forms auth, and TC encrypts
                //      its password on login.html, so WebBrowser is  required to get an authentication cookie
                WebBrowser webBrowser = new WebBrowser {Visible = true};
                string loginPage = rootUrl + "/login.html";
                webBrowser.DocumentCompleted += (o, evt) =>
                {
                    if (webBrowser.DocumentTitle == "Navigation Canceled")
                    {
                        serverUnavailable = true;
                        return;
                    }

                    if (state == 0)
                    {
                        webBrowser.Document.GetElementById("username").SetAttribute("value", userName);
                        webBrowser.Document.GetElementById("username").SetAttribute("value", userName);
                        webBrowser.Document.All["password"].SetAttribute("value", password);

                        var submitButton = webBrowser.Document.GetElementsByTagName("input")
                            .Cast<HtmlElement>()
                            .FirstOrDefault(e => e.GetAttribute("type") == "submit");
                        submitButton.InvokeMember("click");
                    }
                    if (state == 1)
                    {
                        _cookie = webBrowser.Document.Cookie;
                    }

                    state++;
                };
                webBrowser.Navigate(new Uri(loginPage));

                while (state <= 1 && !serverUnavailable)
                {
                    Application.DoEvents();
                }

                webBrowser.Dispose();
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();

            if (serverUnavailable)
                throw new ServerUnavailableException();
        }

        public TeamCityBuildStatus GetBuildStatus(string rootUrl, BuildDefinitionSetting buildDefinitionSetting, string userName, string password)
        {
            rootUrl = GetRootUrl(rootUrl);

            // older versions of team city don't support this format: httpAuth/app/rest/builds/buildType:bt2
            //  but that format requires far fewer http requests
            if (!_supportsGetLatestBuildByBuildTypeId)
            {
                // TeamCity 5.X
                return GetLatestBuildByBuildId(rootUrl, userName, password, buildDefinitionSetting);
            } else
            {
                // TeamCity 6.X
                return GetLatestBuildByBuildTypeId(rootUrl, userName, password, buildDefinitionSetting);
            }
        }

        private static TeamCityBuildStatus GetLatestBuildByBuildId(string rootUrl, string userName, string password, BuildDefinitionSetting buildDefinitionSetting)
        {
            string getLatestBuildIdByBuildTypeUrl = rootUrl + "/httpAuth/app/rest/buildTypes/" + buildDefinitionSetting.Id + "/builds?count=1";
            var latestBuildIdResult = DownloadString(getLatestBuildIdByBuildTypeUrl, userName, password);
            XDocument latestBuildIdXDoc = XDocument.Parse(latestBuildIdResult);
            var id = latestBuildIdXDoc.Descendants("build").Attributes("id").First().Value;
            return GetBuildStatusByBuildId(rootUrl, userName, password, buildDefinitionSetting, id);
        }

        private static TeamCityBuildStatus GetBuildStatusByBuildId(
            string rootUrl,
            string userName,
            string password,
            BuildDefinitionSetting buildDefinitionSetting,
            string buildId)
        {

            XDocument changeResultXDoc = null;
            string getBuildByBuildIdIdUrl = rootUrl + "/httpAuth/app/rest/builds/id:" + buildId;
            var buildResult = DownloadString(getBuildByBuildIdIdUrl, userName, password);
            XDocument buildResultXDoc = XDocument.Parse(buildResult);
            if (buildResultXDoc.Root == null) throw new Exception("Could not get project build status");
            var changesNode = buildResultXDoc.Descendants("changes").First();
            var count = changesNode.AttributeValueOrDefault("count");
            bool commentsExist = !string.IsNullOrEmpty(count) && count != "0";
            if (commentsExist)
            {
                var changesHref = changesNode.AttributeValueOrDefault("href");
                var changesUrl = rootUrl + changesHref;
                var changesResult = DownloadString(changesUrl, userName, password);
                XDocument changesResultXDoc = XDocument.Parse(changesResult);
                var changeNode = changesResultXDoc.Descendants("change").First();
                var changeHref = changeNode.AttributeValueOrDefault("href");
                var changeUrl = rootUrl + changeHref;
                var changeResult = DownloadString(changeUrl, userName, password);
                changeResultXDoc = XDocument.Parse(changeResult);
            }
            return new TeamCityBuildStatus(buildDefinitionSetting, buildResultXDoc, changeResultXDoc);
        }

        private static TeamCityBuildStatus GetLatestBuildByBuildTypeId(string rootUrl, string userName, string password, BuildDefinitionSetting buildDefinitionSetting)
        {
            string url = rootUrl + "/httpAuth/app/rest/builds/buildType:" + buildDefinitionSetting.Id;
            try
            {
                var result = DownloadString(url, userName, password);
                XDocument doc = XDocument.Parse(result);
                if (doc.Root == null) throw new Exception("Could not get project build status");
                // todo: get comments for TeamCity 6.X
                return new TeamCityBuildStatus(buildDefinitionSetting, doc, null);
            } 
            catch (Exception ex)
            {
                if (ex.Message.Contains("BadRequestException: Cannot find build by other locator then 'id' without build type specified."))
                {
                    _log.Debug("_supportsGetLatestBuildByBuildTypeId = false");
                    _supportsGetLatestBuildByBuildTypeId = false;
                    return GetLatestBuildByBuildId(rootUrl, userName, password, buildDefinitionSetting);
                }
                throw;
            }
        }
        
        private static string DownloadString(string url, string userName, string password)
        {
            var webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password),
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore),
            };

            try
            {
                return webClient.DownloadString(url);
            } catch (WebException webException)
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
