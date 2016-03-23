using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Settings;

namespace TfsRestServices
{
    public class TfsRestService
    {
        private static readonly CommentsCache _commentsCache = new CommentsCache();
        private readonly ILog _log = MyLogManager.GetLogger(typeof (TfsRestService));

        public async Task<List<TfsRestProjectCollection>>  GetBuildDefinitionsGrouped(string url, string username, string password)
        {
            var buildDefinitions = await GetBuildDefinitions(url, username, password);
            return new List<TfsRestProjectCollection>
            {
                new TfsRestProjectCollection
                {
                    Name = "pcname",
                    Id = "pcid",
                    Url = "pcurl",
                    Projects = new List<TfsRestProject>
                    {
                        new TfsRestProject
                        {
                            Name = "pname",
                            Id = "pid",
                            Url = "purl",
                            BuildDefinitions = buildDefinitions
                        }
                    }
                }
            };

            //List<TfsRestProjectCollection> result = new List<TfsRestProjectCollection>();
            //var projectCollections = await GetProjectCollections(url, username, password);
            //foreach (var projectCollection in projectCollections)
            //{
            //    var projects = await GetProjects(url, username, password, projectCollection);
            //    var tfsRestProjectCollection = new TfsRestProjectCollection(projectCollection, projects);
            //    tfsRestProjectCollection.Projects = new List<TfsRestProject>();
            //    tfsRestProjectCollection.Projects.Add();
            //    result.Add(tfsRestProjectCollection);
            //}
            //return result;
        }

        private async Task<List<TfsJsonProject>>  GetProjects(string url, string username, string password, TfsJsonProjectCollection projectCollection)
        {
            // todo: implement
            await Task.Yield();
            return new List<TfsJsonProject>
            {
                new TfsJsonProject
                {
                    Id = "pid",
                    Name = "pname",
                    Url = "url"
                }
            };
        }

        public async Task<List<TfsJsonProjectCollection>> GetProjectCollections(string url, string username, string password)
        {
            await Task.Yield();
            // todo: implement
            return new List<TfsJsonProjectCollection>
            {
                new TfsJsonProjectCollection
                {
                    Id = "id",
                    Name = "name",
                    Url = "url"
                }
            };
        }

        public async Task<List<TfsRestBuildDefinition>> GetBuildDefinitions(string url, string username, string password)
        {
            var connection = new TfsConnectionDetails(url, username, password);
            var projects = await GetFromTfs<TfsJsonBuildDefinition>(connection, "_apis/build/definitions", new Dictionary<string, string>());
            return projects.Select(i => new TfsRestBuildDefinition(i)).ToList();
        }

        private async Task<List<T>> GetFromTfs<T>(TfsConnectionDetails connection, string api, Dictionary<string, string> queryParams)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                Credentials = connection.AsNetworkConnection()
            };

            HttpClient httpClient = new HttpClient(handler)
            {
                BaseAddress = connection.GetBaseAddress()
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            var credentialsBase64Encoded = connection.Base64EncodeCredentials();
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
            var message = await GetCommentOnce(tfsJsonBuild, connection);
            if (tfsJsonBuild.Definition.Type == "xaml" && message == null)
            {
                // old style xaml builds don't get associated with a commit immediately for some annoying reason, so keep trying for 2 minutes
                const int maxRetries = 12;
                for (int i = 0; i < maxRetries && message == null; i++)
                {
                    _log.Debug("Comment was null for a xaml build definition, so delaying and checking for a comment again in 10 seconds");
                    await Task.Delay(10000);
                    message = await GetCommentOnce(tfsJsonBuild, connection);
                }
            }
            return message?.Trim();
        }

        private async Task<string> GetCommentOnce(TfsJsonBuild tfsJsonBuild, TfsConnectionDetails connection)
        {
            var comments =
                await
                    GetFromTfs<TfsJsonComment>(connection, "_apis/build/builds/" + tfsJsonBuild.Id + "/changes",
                        new Dictionary<string, string>());
            var firstComment = comments.FirstOrDefault();
            var message = firstComment?.Message;
            return message;
        }
    }
}