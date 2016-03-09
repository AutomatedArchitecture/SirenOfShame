using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SirenOfShame.Lib.Settings;

namespace TfsRestServices
{
    public class TfsRestService
    {
        public async Task<List<TfsRestBuildDefinition>> GetBuildDefinitions(string url, string username, string password)
        {
            var projects = await GetFromTfs<TfsJsonBuildDefinition>(url, "_apis/build/definitions", username, password, new Dictionary<string, string>());
            return projects.Select(i => new TfsRestBuildDefinition(i)).ToList();
        }

        private async Task<List<T>> GetFromTfs<T>(string baseUrl, string api, string username, string password, Dictionary<string, string> queryParams)
        {
            HttpClient httpClient = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);
            var betterUrl = baseUrl + (baseUrl.EndsWith("/") ? "" : "/");
            httpClient.BaseAddress = new Uri(betterUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var queryParamsAsString = string.Concat(queryParams.Select(i => "&" + i.Key + "=" + i.Value));
            var buildDefinitionsStr = await httpClient.GetStringAsync(api + "?api-version=2.0" + queryParamsAsString);
            var jsonWrapper = JsonConvert.DeserializeObject<TfsJsonWrapper<T>>(buildDefinitionsStr);
            return jsonWrapper.Value;
        }

        private static Dictionary<string, string> GetBuildQueryParams(BuildDefinitionSetting[] watchedBuildDefinitions)
        {
            var buildIds = watchedBuildDefinitions.Select(i => i.Id);
            var result = new Dictionary<string, string>
            {
                { "maxBuildsPerDefinition", "1" },
                { "statusFilter", "inProgress,completed" },
                { "definitions", string.Join(",", buildIds) },
            };
            return result;
        }

        public async Task<IEnumerable<TfsRestBuildStatus>> GetBuildsStatuses(CiEntryPointSetting ciEntryPointSetting, BuildDefinitionSetting[] watchedBuildDefinitions)
        {
            var queryParams = GetBuildQueryParams(watchedBuildDefinitions);
            var projects = await GetFromTfs<TfsJsonBuild>(ciEntryPointSetting.Url, "_apis/build/builds", ciEntryPointSetting.UserName, ciEntryPointSetting.GetPassword(), queryParams);
            return projects.Select(i => new TfsRestBuildStatus(i));
        }
    }
}