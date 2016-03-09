using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SirenOfShame.Lib.Settings;

namespace TfsRestServices
{
    public class TfsRestService
    {
        private static readonly CommentsCache _commentsCache = new CommentsCache();

        public async Task<List<TfsRestBuildDefinition>> GetBuildDefinitions(string url, string username, string password)
        {
            var connection = new TfsConnectionDetails(url, username, password);
            var projects = await GetFromTfs<TfsJsonBuildDefinition>(connection, "_apis/build/definitions", new Dictionary<string, string>());
            return projects.Select(i => new TfsRestBuildDefinition(i)).ToList();
        }

        private async Task<List<T>> GetFromTfs<T>(TfsConnectionDetails connection, string api, Dictionary<string, string> queryParams)
        {
            var credentialsBase64Encoded = connection.Base64EncodeCredentials();
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = connection.GetBaseAddress()
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentialsBase64Encoded);
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
            var connection = new TfsConnectionDetails(ciEntryPointSetting);
            var projects = await GetFromTfs<TfsJsonBuild>(connection, "_apis/build/builds", queryParams);
            await _commentsCache.FetchNewComments(projects, connection, GetComment);
            return projects.Select(i => new TfsRestBuildStatus(i, _commentsCache));
        }

        private async Task<string> GetComment(TfsJsonBuild tfsJsonBuild, TfsConnectionDetails connection)
        {
            var comments = await GetFromTfs<TfsJsonComment>(connection, "_apis/build/builds/" + tfsJsonBuild.Id + "/changes", new Dictionary<string, string>());
            var firstComment = comments.FirstOrDefault();
            return firstComment?.Message;
        }
    }
}