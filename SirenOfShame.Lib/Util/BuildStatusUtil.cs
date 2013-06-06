using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Util
{

    public static class BuildStatusUtil
    {
        /// <summary>
        /// Will return a build status list that looks identical to the new build status list except
        /// any build statuses with the same Id and BuildStatus will not be overwritten.
        /// </summary>
        public static BuildStatus[] Merge(IList<BuildStatus> oldBuildStatuses, IList<BuildStatus> newBuildStatuses)
        {
            var buildStatusComparer = new BuildStatusComparer();
            var oldBuildStatusesToRetain = oldBuildStatuses.Except(newBuildStatuses, buildStatusComparer);
            var newBuildStatusesToAdd = newBuildStatuses.Except(oldBuildStatuses, buildStatusComparer);
            var unchangedBuildStatuses = from oldStatus in oldBuildStatuses
                                         join newStatus in newBuildStatuses on oldStatus.BuildDefinitionId equals newStatus.BuildDefinitionId
                                         where newStatus.BuildStatusEnum == oldStatus.BuildStatusEnum &&
                                            newStatus.StartedTime == oldStatus.StartedTime
                                         select oldStatus;
            var changedBuildStatuses = from oldStatus in oldBuildStatuses
                                       join newStatus in newBuildStatuses on oldStatus.BuildDefinitionId equals newStatus.BuildDefinitionId
                                       where newStatus.BuildStatusEnum != oldStatus.BuildStatusEnum ||
                                            newStatus.StartedTime != oldStatus.StartedTime
                                       select newStatus;
            return oldBuildStatusesToRetain.Union(newBuildStatusesToAdd).Union(unchangedBuildStatuses).Union(changedBuildStatuses).ToArray();
        }
    }

    public class BuildStatusComparer : IEqualityComparer<BuildStatus>
    {
        public bool Equals(BuildStatus x, BuildStatus y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return x.BuildDefinitionId == y.BuildDefinitionId;
        }

        public int GetHashCode(BuildStatus obj)
        {
            return 0;
        }
    }
}
