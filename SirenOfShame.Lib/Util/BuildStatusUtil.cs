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
        /// <param name="buildStatuses"></param>
        /// <returns></returns>
        public static BuildStatus[] Merge(IEnumerable<BuildStatus> oldBuildStatuses, IEnumerable<BuildStatus> newBuildStatuses)
        {
            BuildStatus[] result = new BuildStatus[newBuildStatuses.Count()];
            int i = 0;
            foreach (var newBuildStatus in newBuildStatuses)
            {
                var matchingOldBuildStatus = oldBuildStatuses
                    .FirstOrDefault(bs => bs.Id == newBuildStatus.Id && bs.BuildStatusEnum == newBuildStatus.BuildStatusEnum && bs.StartedTime == newBuildStatus.StartedTime);

                result[i] = matchingOldBuildStatus ?? newBuildStatus;
                i++;
            }
            return result;
        }
    }
}
