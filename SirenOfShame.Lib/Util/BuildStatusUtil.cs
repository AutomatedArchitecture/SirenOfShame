﻿using System.Collections.Generic;
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
                                         join newStatus in newBuildStatuses on oldStatus.UniqueId equals newStatus.UniqueId
                                         where newStatus.BuildStatusEnum == oldStatus.BuildStatusEnum &&
                                            newStatus.StartedTime == oldStatus.StartedTime
                                         select oldStatus;
            var changedBuildStatuses = from oldStatus in oldBuildStatuses
                                       join newStatus in newBuildStatuses on oldStatus.UniqueId equals newStatus.UniqueId
                                       where newStatus.BuildStatusEnum != oldStatus.BuildStatusEnum ||
                                            newStatus.StartedTime != oldStatus.StartedTime
                                       select newStatus;
            var duplicateBuildDefIDs = oldBuildStatusesToRetain.Union(newBuildStatusesToAdd).Union(unchangedBuildStatuses).Union(changedBuildStatuses);
            var distinct = duplicateBuildDefIDs.OrderBy(x => x.StartedTime).GroupBy(x => x.BuildDefinitionId).Select(y => y.LastOrDefault());
            return distinct.ToArray();
        }
    }

    public class BuildStatusComparer : IEqualityComparer<BuildStatus>
    {
        public bool Equals(BuildStatus x, BuildStatus y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return x.UniqueId == y.UniqueId;
        }

        public int GetHashCode(BuildStatus obj)
        {
            return 0;
        }
    }
}
