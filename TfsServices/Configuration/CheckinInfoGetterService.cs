using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Build.Client;
using BuildStatus = SirenOfShame.Lib.Watcher.BuildStatus;

namespace TfsServices.Configuration
{
    public class CheckinInfo
    {
        public CheckinInfo(BuildStatus buildStatus)
        {
            Comment = buildStatus.Comment;
            RequestedBy = buildStatus.RequestedBy;
        }

        public CheckinInfo() { }

        public string RequestedBy { get; set; }
        public string Comment { get; set; }
    }

    /// <summary>
    /// Gets author and check in comments
    /// </summary>
    public class CheckinInfoGetterService
    {
        private struct CommentAndHash
        {
            public CommentAndHash(string newBuildHash, MyChangeset getLatestChangeset)
                : this()
            {
                BuildStatusHash = newBuildHash;
                Changeset = getLatestChangeset;
            }

            public MyChangeset Changeset { get; private set; }
            public string BuildStatusHash { get; private set; }
        }

        public CheckinInfo GetCheckinInfo(IBuildDetail buildDetail, BuildStatus buildStatus, MyTfsBuildDefinition buildDefinition)
        {
            var result = new CheckinInfo(buildStatus);

            var changesets = buildDetail.Information.GetNodesByType(MyBuildServer.ASSOCIATED_CHANGESET);
            var commits = buildDetail.Information.GetNodesByType(MyBuildServer.ASSOCIATED_COMMIT);

            var anyGitChangesets = changesets.Any();
            var anyTfsCommits = commits.Any();

            if (anyGitChangesets)
            {
                // This path does not not require a network call. It is used when the build is not in progress and the project uses git source control
                result = SetInfoFromAssociatedChangesets(changesets);
            }
            else if (anyTfsCommits)
            {
                // This path does not not require a network call. It is used when the build is not in progress and the project uses tfs (non-git) source control
                result = SetInfoFromAssociatedCommits(commits);
            }
            else
            {
                // this path may require a network call, however we cache requests.  It is used the first time an in-progress build is found
                var latestChangeset = QueryServerForLatestChangesetButCache(buildDefinition, buildStatus);
                if (latestChangeset != null)
                {
                    result.RequestedBy = latestChangeset.CommitterDisplayName;
                    result.Comment = latestChangeset.Comment;
                }
            }

            if (string.IsNullOrEmpty(result.Comment))
            {
                result.Comment = buildDetail.Reason.ToString();
            }

            return result;
        }

        private static readonly Dictionary<string, CommentAndHash> _cachedCommentsByBuildDefinition = new Dictionary<string, CommentAndHash>();

        private static MyChangeset QueryServerForLatestChangesetButCache(MyTfsBuildDefinition buildDefinition, BuildStatus buildStatus)
        {
            var newBuildHash = buildStatus.GetBuildDataAsHash();
            CommentAndHash cachedChangeset;
            bool haveEverGottenCommentsForThisBuildDef = _cachedCommentsByBuildDefinition.TryGetValue(buildDefinition.Name, out cachedChangeset);
            bool areCacheCommentsStale = false;
            if (haveEverGottenCommentsForThisBuildDef)
            {
                string oldBuildHash = cachedChangeset.BuildStatusHash;
                areCacheCommentsStale = oldBuildHash != newBuildHash;
            }
            if (!haveEverGottenCommentsForThisBuildDef || areCacheCommentsStale)
            {
                MyChangeset latestChangeset = buildDefinition.GetLatestChangeset();
                _cachedCommentsByBuildDefinition[buildDefinition.Name] = new CommentAndHash(newBuildHash, latestChangeset);
            }
            return _cachedCommentsByBuildDefinition[buildDefinition.Name].Changeset;
        }

        /// <summary>
        /// For TFS source control without Git
        /// </summary>
        private CheckinInfo SetInfoFromAssociatedCommits(List<IBuildInformationNode> commits)
        {
            return new CheckinInfo
            {
                RequestedBy = GetRequestedByFromCommit(commits),
                Comment = commits.Count > 1 ? "(Multiple Commits)" : commits.First().Fields["Message"]
            };
        }

        /// <summary>
        /// For TFS+Git
        /// </summary>
        private CheckinInfo SetInfoFromAssociatedChangesets(List<IBuildInformationNode> changesets)
        {
            return new CheckinInfo
            {
                RequestedBy = GetRequestedByFromChangeset(changesets),
                Comment = changesets.Count > 1 ? "(Multiple Changesets)" : changesets.First().Fields["Comment"]
            };
        }

        private static string GetRequestedByFromCommit(IEnumerable<IBuildInformationNode> commits)
        {
            var lastCommit = commits.LastOrDefault();
            if (lastCommit == null) return null;
            return lastCommit.Fields["Committer"];
        }

        private static string GetRequestedByFromChangeset(IEnumerable<IBuildInformationNode> changesets)
        {
            var users = new HashSet<String>();
            foreach (var changeset in changesets)
                users.Add(changeset.Fields["CheckedInBy"]);

            var count = users.Count();
            if (count > 1)
                return "(Multiple Users)";
            return users.First();
        }


    }
}
