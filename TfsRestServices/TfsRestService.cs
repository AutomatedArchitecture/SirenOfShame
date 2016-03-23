using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Settings;

namespace TfsRestServices
{
    public class TfsRestService
    {
        private static readonly CommentsCache _commentsCache = new CommentsCache();
        private readonly ILog _log = MyLogManager.GetLogger(typeof (TfsRestService));
        private readonly TfsJsonService _tfsJsonService = new TfsJsonService();

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
            var tfsJsonBuildDefinitions = await _tfsJsonService.GetBuildDefinitions(url, username, password);
            return tfsJsonBuildDefinitions.Select(i => new TfsRestBuildDefinition(i)).ToList();
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
            var connection = new TfsConnectionDetails(ciEntryPointSetting);
            var buildQueryParams = GetBuildQueryParams(watchedBuildDefinitions);
            var projects = await _tfsJsonService.GetBuildsStatuses(connection, buildQueryParams);
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
            var comments = await _tfsJsonService.GetComments(tfsJsonBuild, connection);
            var firstComment = comments.FirstOrDefault();
            var message = firstComment?.Message;
            return message;
        }
    }
}