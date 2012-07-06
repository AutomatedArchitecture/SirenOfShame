using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Xml.Linq;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;

namespace BuildBotServices
{
    public class BuildBotService : ServiceBase
    {
        

        private static readonly ILog _log = MyLogManager.GetLogger(typeof(BuildBotService));

        public delegate void GetProjectsCompleteDelegate(BuildBotBuildDefinition[] buildDefinitions);

        public void GetProjects(string rootUrl, string userName, string password, GetProjectsCompleteDelegate complete, Action<Exception> onError)
        {
            WebClient webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password)
            };
            rootUrl = GetRootUrl(rootUrl);
            var projectUrl = new Uri(rootUrl + "/json/builders");
            webClient.DownloadStringCompleted += (s, e) =>
            {
                try
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Dictionary<string, BuildBotBuildersJSONQuery> builders = serializer.Deserialize<Dictionary<string, BuildBotBuildersJSONQuery>>(e.Result);
                    var builds = new List<BuildBotBuildDefinition>();
                    foreach (KeyValuePair<string, BuildBotBuildersJSONQuery> kvp in builders)
                    {
                        var buildurl = new Uri(rootUrl + "/json/builders/" + kvp.Key );
                        builds.Add( new BuildBotBuildDefinition(buildurl.ToString(), kvp.Key ) );
                    }
                    complete(builds.ToArray());
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

        public IList<BuildBotBuildStatus> GetBuildsStatuses(string rootUrl, string userName, string password, BuildDefinitionSetting[] watchedBuildDefinitions)
        {
            var results = new List<BuildBotBuildStatus>();
            rootUrl = GetRootUrl(rootUrl);
            var webClient = new WebClient
            {
                Credentials = new NetworkCredential(userName, password),
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore),
            };

            foreach (var watchedBuildDefinition in watchedBuildDefinitions)
            {
                var projectUrl = new Uri(rootUrl + "/json/builders/" + watchedBuildDefinition.Name + "/builds?select=-1");
                
                try
                {
                    var resultString = webClient.DownloadString(projectUrl);
                    results.Add(new BuildBotBuildStatus(resultString, rootUrl));
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

                                    string message = "Error connecting to server with the following url: " + projectUrl.ToString() + "\n\n" + errorResult;
                                    _log.Error(message, webException);
                                    throw new SosException(message, webException);
                                }
                            }
                        }
                    }
                    if (webException.Status == WebExceptionStatus.Timeout)
                    {
                        throw new ServerUnavailableException();
                    }
                    if (webException.Status == WebExceptionStatus.NameResolutionFailure)
                    {
                        throw new ServerUnavailableException();
                    }
                    if (webException.Status == WebExceptionStatus.ConnectFailure)
                    {
                        throw new ServerUnavailableException();
                    }

                    _log.Error("Error connecting to " + projectUrl.ToString() + ". WebException.Status = " + webException.Status);
                    throw;
                }
            }
            return results;
        }
    }
}
