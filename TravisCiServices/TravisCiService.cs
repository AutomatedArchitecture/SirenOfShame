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
        public void GetProject(string baseUrl, string ownerName, string projectName, Action<TravisCiBuildDefinition> getProjectComplete, Action<Exception> getProjectError)
        {
            WebClient webClient = new WebClient();
            var travisUrl = GetUrl(baseUrl, ownerName, projectName);
            var projectUrl = new Uri(travisUrl);
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
            AddTravisHeaders(webClient);
            webClient.DownloadStringAsync(projectUrl);
        }

        private string GetUrl(string baseUrl, string ownerName, string projectName)
        {
            return string.Format("{0}repos/{1}/{2}", baseUrl, ownerName, projectName);
        }

        public IEnumerable<TravisCiBuildStatus> GetBuildsStatuses(CiEntryPointSetting ciEntryPointSetting, IEnumerable<BuildDefinitionSetting> watchedBuildDefinitions)
        {
            var parallelResult = watchedBuildDefinitions.Select(buildDefinitionSetting => GetBuildStatus(ciEntryPointSetting, buildDefinitionSetting));
            return parallelResult.AsParallel().ToList();
        }

        private void AddTravisHeaders(WebClient webClient)
        {
            webClient.Headers.Add("User-Agent", "SirenOfShame/1.0.0");
            webClient.Headers.Add("Accept", "application/vnd.travis-ci.2+json");
            // todo: help generate a token for users
            webClient.Headers.Add("Authorization", "token \"mytoken\"");
        }

        private TravisCiBuildStatus GetBuildStatus(CiEntryPointSetting ciEntryPointSetting, BuildDefinitionSetting buildDefinitionSetting)
        {
            var webClient = new WebClient();
            AddTravisHeaders(webClient);
            var travisBuildDef = TravisCiBuildDefinition.FromIdString(buildDefinitionSetting.Id);
            var buildDefinitionUrl = GetUrl(ciEntryPointSetting.Url, travisBuildDef.OwnerName, travisBuildDef.ProjectName);

            try
            {
                var json = webClient.DownloadString(buildDefinitionUrl);
                var lastBuildId = GetJsonValue(json, "last_build_id");
                var buildUrl = string.Format("{0}builds/{1}", ciEntryPointSetting.Url, lastBuildId);
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
