using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TfsRestServices
{
    public class CommentsCache
    {
        private readonly Dictionary<int, Tuple<int, string>> _commentsCache = new Dictionary<int, Tuple<int, string>>();

        public async Task FetchNewComments(List<TfsJsonBuild> projects, TfsConnectionDetails connection, Func<TfsJsonBuild, TfsConnectionDetails, Task<string>> getCommentFunc)
        {
            var unCachedDefinitions = projects.Where(i => !_commentsCache.ContainsKey(i.Definition.Id));
            var newlyChangedBuilds = projects.Where(i => _commentsCache.ContainsKey(i.Definition.Id) && _commentsCache[i.Definition.Id].Item1 != i.Id);

            var allInvalidCaches = unCachedDefinitions.Concat(newlyChangedBuilds);
            var commentRetrievalTasks = allInvalidCaches.Select(tfsJsonBuild => FetchAndSaveComment(connection, getCommentFunc, tfsJsonBuild));
            // perform all comment retrievals in parallel
            await Task.WhenAll(commentRetrievalTasks);
        }

        private async Task FetchAndSaveComment(TfsConnectionDetails connection, Func<TfsJsonBuild, TfsConnectionDetails, Task<string>> getCommentFunc, TfsJsonBuild tfsJsonBuild)
        {
            var buildDefinitionId = tfsJsonBuild.Definition.Id;
            var buildId = tfsJsonBuild.Id;
            var comment = await getCommentFunc(tfsJsonBuild, connection);
            _commentsCache[buildDefinitionId] = new Tuple<int, string>(buildId, comment);
        }

        public string GetCachedCommentForBuild(TfsJsonBuild build)
        {
            var buildDefinition = build.Definition.Id;
            var cachedCommentForBuild = _commentsCache[buildDefinition];
            return cachedCommentForBuild.Item2;
        }
    }
}