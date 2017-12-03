using System;
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

        public async Task<List<TfsRestProjectCollection>>  GetBuildDefinitionsGrouped(string url, string collection, string username, string password)
        {
            var resultProjectCollections = new List<TfsRestProjectCollection>();

            var connection = new TfsConnectionDetails(url, username, password);
            List<TfsJsonProjectCollection> projectCollections;
            if (string.IsNullOrEmpty(collection))
            {
                projectCollections = await _tfsJsonService.GetProjectCollections(connection);
                foreach (var projectCollection in projectCollections)
                {
                    //substitue the project name with DefaultCollection as defined in VSO REST API documenation
                    projectCollection.Name = SubstituteName(url, projectCollection.Name);
                }
            }
            else
            {
                projectCollections = new List<TfsJsonProjectCollection>(new[] {new TfsJsonProjectCollection {Name = collection}});
            }

            foreach (var projectCollection in projectCollections)
            {
                var resultProjectCollection = new TfsRestProjectCollection(projectCollection);
                var projects = await _tfsJsonService.GetProjects(connection, projectCollection.Name);
                foreach (var project in projects)
                {
                    var resultProject = new TfsRestProject(project);
                    resultProjectCollection.Projects.Add(resultProject);
                    var buildDefinitions = await GetBuildDefinitions(connection, projectCollection, project);
                    resultProject.BuildDefinitions = buildDefinitions;
                }
                resultProjectCollections.Add(resultProjectCollection);
            }

            return resultProjectCollections;
        }

        private async Task<List<TfsRestBuildDefinition>> GetBuildDefinitions(TfsConnectionDetails connection, TfsJsonProjectCollection projectCollection, TfsJsonProject project)
        {
            var tfsJsonBuildDefinitions = await _tfsJsonService.GetBuildDefinitions(connection, projectCollection.Name, project.Name);
            return tfsJsonBuildDefinitions.Select(i => new TfsRestBuildDefinition(i, project, projectCollection)).ToList();
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

        public virtual async Task<IEnumerable<TfsRestBuildStatus>> GetBuildsStatuses(CiEntryPointSetting ciEntryPointSetting, BuildDefinitionSetting[] watchedBuildDefinitions)
        {
            var connection = new TfsConnectionDetails(ciEntryPointSetting);
            var buildDefinitionsByProjectCollection = watchedBuildDefinitions.GroupBy(bd => bd.Parent);
            List<TfsRestBuildStatus> resultingBuildStatuses = new List<TfsRestBuildStatus>();
            foreach (var buildDefinitionGroup in buildDefinitionsByProjectCollection)
            {
                var projectCollection = buildDefinitionGroup.Key;
                var buildQueryParams = GetBuildQueryParams(buildDefinitionGroup.ToArray());
                var projects = await _tfsJsonService.GetBuildsStatuses(connection, buildQueryParams, projectCollection);
                await _commentsCache.FetchNewComments(projects, connection, tfsJsonBuilds => GetComment(tfsJsonBuilds, connection, projectCollection));
                var buildStatuses = projects.Select(i => new TfsRestBuildStatus(i, _commentsCache));
                resultingBuildStatuses.AddRange(buildStatuses);
            }
            return resultingBuildStatuses;
        }

        private async Task<string> GetComment(TfsJsonBuild tfsJsonBuild, TfsConnectionDetails connection, string projectCollection)
        {
            var message = await GetCommentOnce(tfsJsonBuild, connection, projectCollection);
            if (tfsJsonBuild.Definition.Type == "xaml" && message == null)
            {
                // old style xaml builds don't get associated with a commit immediately for some annoying reason, so keep trying for 2 minutes
                const int maxRetries = 12;
                for (int i = 0; i < maxRetries && message == null; i++)
                {
                    _log.Debug("Comment was null for a xaml build definition, so delaying and checking for a comment again in 10 seconds");
                    await Task.Delay(10000);
                    message = await GetCommentOnce(tfsJsonBuild, connection, projectCollection);
                }
            }
            return message?.Trim();
        }

        private async Task<string> GetCommentOnce(TfsJsonBuild tfsJsonBuild, TfsConnectionDetails connection, string projectCollection)
        {
            var comments = await _tfsJsonService.GetComments(tfsJsonBuild, connection, projectCollection);
            var firstComment = comments.FirstOrDefault();
            var message = firstComment?.Message;
            return message;
        }

        public static string SubstituteName(string url, string projectCollectionName)
        {
            var uri = new Uri(url);
            return SubstituteName(uri, projectCollectionName);
        }

        private static string SubstituteName(Uri url, string projectCollectionName)
        {
            if (url.HostNameType == UriHostNameType.Dns)
            {
                var projectNameFromUrl = url.Host.Split('.')[0];
                return string.Equals(projectNameFromUrl, projectCollectionName) ? "DefaultCollection" : projectCollectionName;
            }

            return projectCollectionName;
        }

    }
}