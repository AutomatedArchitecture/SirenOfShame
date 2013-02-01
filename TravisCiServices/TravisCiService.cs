using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace TravisCiServices
{
    public class TravisCiService : ServiceBase
    {
        public void GetProject(string ownerName, string projectName, Action<TravisCiBuildDefinition> getProjectComplete, Action<Exception> getProjectError)
        {
            WebClient webClient = new WebClient();
            var projectUrl = new Uri(GetUrl(ownerName, projectName));
            webClient.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error!=null)
                {
                    getProjectError(e.Error);
                    return;
                }
                try
                {
                    getProjectComplete(TravisCiBuildDefinition.FromJson(e.Result));
                }
                catch (Exception ex)
                {
                    getProjectError(ex);
                }
            };
            webClient.DownloadStringAsync(projectUrl);
        }

        private string GetUrl(string ownerName, string projectName)
        {
            return "http://travis-ci.org/" + ownerName + "/" + projectName + ".json";
        }

        public IList<TravisCiBuildStatus> GetBuildsStatuses(BuildDefinitionSetting[] watchedBuildDefinitions)
        {
            var parallelResult = from buildDefinitionSetting in watchedBuildDefinitions
                                 select GetBuildStatus(buildDefinitionSetting);
            return parallelResult.AsParallel().ToList();
        }

        private TravisCiBuildStatus GetBuildStatus(BuildDefinitionSetting buildDefinitionSetting)
        {
            var webClient = new WebClient();
            var travisBuildDef = TravisCiBuildDefinition.FromIdString(buildDefinitionSetting.Id);
            var buildDefinitionUrl = "https://api.travis-ci.org/repositories/" + travisBuildDef.OwnerName + "/" + travisBuildDef.ProjectName + ".json";

            try
            {
                var json = webClient.DownloadString(buildDefinitionUrl);
                var lastBuildId = GetJsonValue(json, "last_build_id");
                var buildUrl = "https://api.travis-ci.org/builds/" + lastBuildId + ".json";
                json = webClient.DownloadString(buildUrl);
                return new TravisCiBuildStatus(travisBuildDef, json, buildDefinitionSetting);
            }
            catch (WebException webException)
            {
                throw WebClientXml.ToServerUnavailableException(buildDefinitionUrl, webException);
            }
        }

        public static DateTime? GetJsonDate(string json, string key)
        {
            var val = GetJsonValue(json, key);
            if (string.IsNullOrWhiteSpace(val) || val == "null") return null;
            return DateTime.Parse(val);
        }

        public static string GetJsonValue(string json, string key)
        {
            var match = Regex.Match(json, "['\"]" + key + "['\"]:\\s*(.*?),");
            if (!match.Success)
            {
                throw new Exception("Could not find key: '" + key + "' in json");
            }
            var result = match.Groups[1].Value;
            result = result.Trim('"');
            return result;
        }
    }
}
