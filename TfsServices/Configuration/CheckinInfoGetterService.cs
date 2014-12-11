using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Build.Client;
using BuildStatus = SirenOfShame.Lib.Watcher.BuildStatus;

namespace TfsServices.Configuration
{
    public class CheckinInfo
    {
        public string RequestedBy { get; set; }
        public string Comment { get; set; }
    }

    /// <summary>
    /// Gets author and check in comments
    /// </summary>
    public class CheckinInfoGetterService
    {
        public CheckinInfo GetCheckinInfo(IBuildDetail buildDetail, BuildStatus buildStatus)
        {
            var result = new CheckinInfo
            {
                Comment = buildStatus.Comment,
                RequestedBy = buildStatus.RequestedBy
            };

            var changesets = buildDetail.Information.GetNodesByType(MyBuildServer.ASSOCIATED_CHANGESET);
            var commits = buildDetail.Information.GetNodesByType(MyBuildServer.ASSOCIATED_COMMIT);

            if (changesets.Any())
            {
                result = SetInfoFromAssociatedChangesets(changesets);
            }
            else if (commits.Any())
            {
                result = SetInfoFromAssociatedCommits(commits);
            }
            else
            {
                // todo: Retrieve associated build and get build info from there
                //GetBuildAndSetInfo();
            }

            if (string.IsNullOrEmpty(result.Comment))
            {
                result.Comment = buildDetail.Reason.ToString();
            }

            return result;
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
