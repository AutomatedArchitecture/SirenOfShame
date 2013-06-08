using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using log4net;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Framework.Client;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Watcher;
using BuildStatus = SirenOfShame.Lib.Watcher.BuildStatus;

namespace TfsServices.Configuration
{
    public class CachedCommentsRetriever
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

        private static readonly Dictionary<string, CommentAndHash> CachedCommentsByBuildDefinition = new Dictionary<string, CommentAndHash>();

        public BuildStatus GetCommentsIntoBuildStatus(MyTfsBuildDefinition buildDefinition, BuildStatus buildStatus)
        {
            MyChangeset changeset = GetCommentsForBuild(buildDefinition, buildStatus);
            return AddCommentsToBuildStatus(buildStatus, changeset);
        }

        private static BuildStatus AddCommentsToBuildStatus(BuildStatus buildStatus, MyChangeset changeset)
        {
            if (changeset == null) return null;
            buildStatus.Comment = changeset.Comment;
            buildStatus.BuildId = changeset.ChangesetId.ToString(CultureInfo.InvariantCulture);
            return buildStatus;
        }

        private MyChangeset GetCommentsForBuild(MyTfsBuildDefinition buildDefinition, BuildStatus buildStatus)
        {
            var newBuildHash = buildStatus.GetBuildDataAsHash();
            CommentAndHash cachedChangeset;
            bool haveEverGottenCommentsForThisBuildDef = CachedCommentsByBuildDefinition.TryGetValue(buildDefinition.Name, out cachedChangeset);
            bool areCacheCommentsStale = false;
            if (haveEverGottenCommentsForThisBuildDef)
            {
                string oldBuildHash = cachedChangeset.BuildStatusHash;
                areCacheCommentsStale = oldBuildHash != newBuildHash;
            }
            if (!haveEverGottenCommentsForThisBuildDef || areCacheCommentsStale)
            {
                MyChangeset latestChangeset = buildDefinition.GetLatestChangeset();
                CachedCommentsByBuildDefinition[buildDefinition.Name] = new CommentAndHash(newBuildHash, latestChangeset);
            }
            return CachedCommentsByBuildDefinition[buildDefinition.Name].Changeset;
        }
    }

    public class MyBuildServer
    {
        private static readonly ILog Log = MyLogManager.GetLogger(typeof(MyBuildServer));
        private readonly IBuildServer _buildServer;

        public MyBuildServer(IBuildServer buildServer, MyTfsProject myTfsProject)
        {
            _buildServer = buildServer;
        }

        public IEnumerable<BuildStatus> GetBuildStatuses(IEnumerable<MyTfsBuildDefinition> buildDefinitionsQuery, bool applyBuildQuality)
        {
            List<MyTfsBuildDefinition> buildDefinitions = buildDefinitionsQuery.ToList();
            IEnumerable<IBuildDetail> buildDetails = GetBuildDetailsFromServer(buildDefinitions);

            var buildDetailsAndTheirBuildStatuses = from buildDefinition in buildDefinitions
                                                    join buildDetail in buildDetails on buildDefinition.Id equals buildDetail.BuildDefinition.Name
                                                    select new { buildDefinition, buildDetail };

            var cachedCommentsRetriever = new CachedCommentsRetriever();

            var buildStatusWithComments = buildDetailsAndTheirBuildStatuses.Select(i => cachedCommentsRetriever
                .GetCommentsIntoBuildStatus(i.buildDefinition, CreateBuildStatus(i.buildDetail, i.buildDefinition, applyBuildQuality)))
                .ToList();
            return buildStatusWithComments;
        }

        private IEnumerable<IBuildDetail> GetBuildDetailsFromServer(IEnumerable<MyTfsBuildDefinition> buildDefinitions)
        {
            var buildDefinitionUris = buildDefinitions.Select(bd => bd.Uri);
            IBuildDetailSpec buildDetailSpec = _buildServer.CreateBuildDetailSpec(buildDefinitionUris);
            buildDetailSpec.MaxBuildsPerDefinition = 1;
            buildDetailSpec.QueryOrder = BuildQueryOrder.FinishTimeDescending;

            IBuildQueryResult buildQueryResults;
            try
            {
                buildQueryResults = _buildServer.QueryBuilds(buildDetailSpec);
            }
            catch (DatabaseOperationTimeoutException ex)
            {
                Log.Debug(ex);
                throw new ServerUnavailableException();
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving build details", ex);
                throw new ServerUnavailableException("Error retrieving build details", ex);
            }
            var latestBuilds = from build in buildQueryResults.Builds
                               group build by build.BuildDefinition.Id
                                   into g
                                   select g.OrderByDescending(b => b.StartTime).First();
            return latestBuilds.ToList();
        }

        private static BuildStatusEnum GetBuildStatusEnum(Microsoft.TeamFoundation.Build.Client.BuildStatus status)
        {
            switch (status)
            {
                case (Microsoft.TeamFoundation.Build.Client.BuildStatus.Failed):
                    return BuildStatusEnum.Broken;
                case (Microsoft.TeamFoundation.Build.Client.BuildStatus.Succeeded):
                    return BuildStatusEnum.Working;
                case (Microsoft.TeamFoundation.Build.Client.BuildStatus.PartiallySucceeded):
                    return BuildStatusEnum.Broken;
                case (Microsoft.TeamFoundation.Build.Client.BuildStatus.InProgress):
                    return BuildStatusEnum.InProgress;
                case (Microsoft.TeamFoundation.Build.Client.BuildStatus.NotStarted):
                    // it briefly goes to NotStarted after new successes or fails for some reason
                    return BuildStatusEnum.InProgress;
                default:
                    Log.Debug("Unhandeled build status returned from server: " + status);
                    return BuildStatusEnum.Unknown;
            }
        }

        private static BuildStatusEnum GetBuildStatusEnum(String quality)
        {
            switch (quality)
            {
                case "Initial Test Passed":
                case "Lab Test Passed":
                case "Ready for Deployment":
                case "Released":
                case "UAT Passed":
                    return BuildStatusEnum.Working;
                case "Under Investigation":
                    return BuildStatusEnum.InProgress;
                case "Rejected":
                    return BuildStatusEnum.Broken;
// ReSharper disable RedundantCaseLabel
                case "Ready for Initial Test":
                case "Unexamined":
// ReSharper restore RedundantCaseLabel
                default:
                    return BuildStatusEnum.Unknown;
            }
        }

        private static BuildStatus CreateBuildStatus(IBuildDetail buildDetail, MyTfsBuildDefinition myTfsBuildDefinition, bool applyBuildQuality)
        {
            return new BuildStatus
            {
                BuildDefinitionId = buildDetail.BuildDefinition.Name,
                Name = buildDetail.BuildDefinition.Name,
                BuildStatusEnum = GetBuildStatusEnum(buildDetail, applyBuildQuality),
                RequestedBy = buildDetail.RequestedFor,
                StartedTime = buildDetail.StartTime == DateTime.MinValue ? (DateTime?)null : buildDetail.StartTime,
                FinishedTime = buildDetail.FinishTime == DateTime.MinValue ? (DateTime?)null : buildDetail.FinishTime,
                Url = myTfsBuildDefinition.ConvertTfsUriToUrl(buildDetail.Uri)
            };
        }

        private static BuildStatusEnum GetBuildStatusEnum(IBuildDetail buildDetail, bool applyBuildQuality)
        {
            if (applyBuildQuality)
            {
                var qualityBasedBuildStatus = GetBuildStatusEnum(buildDetail.Quality);
                if (qualityBasedBuildStatus != BuildStatusEnum.Unknown) 
                    return qualityBasedBuildStatus;
            }
            return GetBuildStatusEnum(buildDetail.Status);
        }

        private static Uri GetUriFromBuildServer(IBuildServer buildServer)
        {
            if (buildServer == null) return null;
            if (buildServer.TeamProjectCollection == null) return null;
            if (buildServer.TeamProjectCollection.Uri == null) return null;
            return buildServer.TeamProjectCollection.Uri;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var myUri = BuildServerUri;
            if (myUri == null) return false;

            if (!(obj is MyBuildServer)) return false;
            var theirUri = ((MyBuildServer)obj).BuildServerUri;
            return myUri.Equals(theirUri);
        }

        private Uri BuildServerUri
        {
            get { return GetUriFromBuildServer(_buildServer); }
        }

        public override int GetHashCode()
        {
            var uri = BuildServerUri;
            if (uri == null) return 0;
            return uri.GetHashCode();
        }
    }
}
