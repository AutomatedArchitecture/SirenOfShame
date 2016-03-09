using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TfsRestServices
{
    public class CommentsCache
    {
        private readonly Dictionary<int, Tuple<int, string>> _commentsCache = new Dictionary<int, Tuple<int, string>>();

        public async Task FetchNewComments(List<TfsJsonBuild> projects, TfsConnectionDetails connection, Func<TfsJsonBuild, TfsConnectionDetails, Task<string>> getCommentFunc)
        {
            foreach (var tfsJsonBuild in projects)
            {
                var buildDefinitionId = tfsJsonBuild.Definition.Id;
                var buildId = tfsJsonBuild.Id;

                if (!_commentsCache.ContainsKey(buildDefinitionId))
                {
                    await FetchAndSaveComment(connection, getCommentFunc, tfsJsonBuild);
                }
                else
                {
                    var oldBuildId = _commentsCache[buildDefinitionId].Item1;
                    if (oldBuildId != buildId)
                    {
                        await FetchAndSaveComment(connection, getCommentFunc, tfsJsonBuild);
                    }
                }
            }
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