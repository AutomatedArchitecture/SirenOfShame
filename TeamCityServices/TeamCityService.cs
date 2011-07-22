using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Xml.Linq;
using SirenOfShame.Lib.Exceptions;
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

        public void GetInProgressBuilds(string rootUrl, string userName, string password, Action<int> onGetOutstandingInProgressBuildCount, Action<TeamCityBuildStatus> complete, Action<Exception> onError)
        {
            rootUrl = GetRootUrl(rootUrl);
            string url = rootUrl + "/ajax.html?getRunningBuilds=1";
            MakeAsyncWebRequest(url, userName, password, onError, result =>
            {
                //var xDocResult = XDocument.Parse(result);
                //_log.Debug(result);
                onGetOutstandingInProgressBuildCount(0);
            }, post: true);
        }

        public void GetBuildStatus(string rootUrl, BuildDefinitionSetting buildDefinitionSetting, string userName, string password, Action<TeamCityBuildStatus> complete, Action<Exception> onError)
        {
            rootUrl = GetRootUrl(rootUrl);

            // older versions of team city don't support this format: httpAuth/app/rest/builds/buildType:bt2
            //  but that format requires far fewer http requests
            if (_supportsGetLatestBuildByBuildTypeId)
            {
                GetLatestBuildByBuildTypeId(rootUrl, userName, password, buildDefinitionSetting, complete, onError);
            } else
            {
                GetLatestBuildByBuildId(rootUrl, userName, password, buildDefinitionSetting, complete, onError);
            }
        }

        private static void GetLatestBuildByBuildId(string rootUrl, string userName, string password, BuildDefinitionSetting buildDefinitionSetting, Action<TeamCityBuildStatus> complete, Action<Exception> onError)
        {
            string getLatestBuildIdByBuildTypeUrl = rootUrl + "/httpAuth/app/rest/buildTypes/" + buildDefinitionSetting.Id + "/builds?count=1";
            MakeAsyncWebRequest(getLatestBuildIdByBuildTypeUrl, userName, password, onError, latestBuildIdResult =>
            {
                XDocument latestBuildIdXDoc = XDocument.Parse(latestBuildIdResult);
                var id = latestBuildIdXDoc.Descendants("build").Attributes("id").First().Value;
                string getBuildByBuildIdIdUrl = rootUrl + "/httpAuth/app/rest/builds/id:" + id;
                MakeAsyncWebRequest(getBuildByBuildIdIdUrl, userName, password, onError, buildResult =>
                {
                    XDocument buildResultXDoc = XDocument.Parse(buildResult);
                    if (buildResultXDoc.Root == null) throw new Exception("Could not get project build status");
                    var teamCityBuildStatus = new TeamCityBuildStatus(buildDefinitionSetting, buildResultXDoc);
                    complete(teamCityBuildStatus);
                });
            });
        }

        private static void GetLatestBuildByBuildTypeId(string rootUrl, string userName, string password, BuildDefinitionSetting buildDefinitionSetting, Action<TeamCityBuildStatus> complete, Action<Exception> onError)
        {
            string url = rootUrl + "/httpAuth/app/rest/builds/buildType:" + buildDefinitionSetting.Id;
            MakeAsyncWebRequest(url, userName, password, onError, result =>
            {
                XDocument doc = XDocument.Parse(result);
                if (doc.Root == null) throw new Exception("Could not get project build status");
                var teamCityBuildStatus = new TeamCityBuildStatus(buildDefinitionSetting, doc);
                complete(teamCityBuildStatus);
            }, errorMessage =>
            {
                if (errorMessage.Contains("BadRequestException: Cannot find build by other locator then 'id' without build type specified."))
                {
                    _log.Debug("_supportsGetLatestBuildByBuildTypeId = false");
                    _supportsGetLatestBuildByBuildTypeId = false;
                    GetLatestBuildByBuildId(rootUrl, userName, password, buildDefinitionSetting, complete, onError);
                    return true;
                }
                return false;
            });
        }

        private static void MakeAsyncWebRequest(string url, string userName, string password, Action<Exception> onError, Action<string> onSuccess, Func<string, bool> customOnError = null, bool post = false)
        {
            WebClient webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password),
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore),
            };

            if (post)
            {
                // todo: do forms authentication + cookies, yuck
                webClient.Headers.Add("Content-type", "application/x-www-form-urlencoded");
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadStringCompleted += (s, e) => OnRequestComplete(e.Error, () => e.Result, url, onError, onSuccess, customOnError);
                webClient.UploadStringAsync(new Uri(url), "POST", "getRunningBuilds:1");
            } else
            {
                webClient.DownloadStringCompleted += (s, e) => OnRequestComplete(e.Error, () => e.Result, url, onError, onSuccess, customOnError);
                webClient.DownloadStringAsync(new Uri(url));
            }
        }

        private static void OnRequestComplete(Exception error, Func<string> result, string url, Action<Exception> onError, Action<string> onSuccess, Func<string, bool> customOnError)
        {
            if (error != null)
            {
                WebException webException = error as WebException;
                if (webException != null && webException.Response != null)
                {
                    var response = webException.Response;
                    using (Stream s1 = response.GetResponseStream())
                    {
                        if (s1 != null)
                        {
                            using (StreamReader sr = new StreamReader(s1))
                            {
                                var errorResult = sr.ReadToEnd();
                                if (customOnError != null && customOnError(errorResult))
                                    return;

                                _log.Error("Error connecting to server with the following url: " + url + "\n\n" + errorResult, webException);
                                onError(new SosException(errorResult, error));
                                return;
                            }
                        }
                    }
                }

                _log.Error("Error connecting to server with the following url: " + url, error);
                onError(error);

                return;
            }

            try
            {
                onSuccess(result());
            }
            catch (Exception ex)
            {
                _log.Error("Error connecting to team city with the following url: " + url, ex);
                onError(ex);
            }
        }
    }
}
