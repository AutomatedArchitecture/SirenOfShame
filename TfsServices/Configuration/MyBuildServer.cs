using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Microsoft.TeamFoundation.Build.Client;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Watcher;
using BuildStatus = SirenOfShame.Lib.Watcher.BuildStatus;

namespace TfsServices.Configuration
{
    public class MyBuildServer
    {
        public const string ASSOCIATED_CHANGESET = "AssociatedChangeset";
        public const string ASSOCIATED_COMMIT = "AssociatedCommit";
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(MyBuildServer));
        private readonly IBuildServer _buildServer;
        private readonly MyTfsProject _tfsProject;
        private bool _firstRequest = true;

        public MyBuildServer(IBuildServer buildServer, MyTfsProject myTfsProject)
        {
            _buildServer = buildServer;
            _tfsProject = myTfsProject;
        }

        public IEnumerable<BuildStatus> GetBuildStatuses(IEnumerable<MyTfsBuildDefinition> buildDefinitionsQuery, bool applyBuildQuality)
        {
            var buildDefinitionUris = buildDefinitionsQuery.Select(bd => bd.Uri).ToArray();

            var inProgressQuery = _buildServer.CreateBuildDetailSpec(buildDefinitionUris);
            inProgressQuery.Status = Microsoft.TeamFoundation.Build.Client.BuildStatus.InProgress;
            inProgressQuery.QueryOrder = BuildQueryOrder.FinishTimeDescending;
            inProgressQuery.InformationTypes = new[] { ASSOCIATED_CHANGESET, ASSOCIATED_COMMIT };
            inProgressQuery.QueryOptions = _firstRequest ? QueryOptions.Process : QueryOptions.None;

            var mostRecentQuery = _buildServer.CreateBuildDetailSpec(buildDefinitionUris);
            mostRecentQuery.MaxBuildsPerDefinition = 1;
            mostRecentQuery.Status = Microsoft.TeamFoundation.Build.Client.BuildStatus.Failed | Microsoft.TeamFoundation.Build.Client.BuildStatus.PartiallySucceeded | Microsoft.TeamFoundation.Build.Client.BuildStatus.Succeeded;
            mostRecentQuery.QueryOrder = BuildQueryOrder.FinishTimeDescending;
            mostRecentQuery.InformationTypes = new[] { ASSOCIATED_CHANGESET, ASSOCIATED_COMMIT };
            mostRecentQuery.QueryOptions = _firstRequest ? QueryOptions.Process : QueryOptions.None;

            _firstRequest = false;

            var buildQueryResult = _buildServer.QueryBuilds(new [] { inProgressQuery, mostRecentQuery });

            var buildStatuses = new Dictionary<String, BuildStatus>();
            var buildDetail = new Dictionary<String, IBuildDetail>();

            // Get last completed for each def
            foreach (var build in buildQueryResult[1].Builds)
                buildDetail[GetBuildDefIdFromBuildDefUri(build.BuildDefinitionUri)] = build;

            // Get current build (if any) and overwrite last completed
            foreach (var build in buildQueryResult[0].Builds)
                buildDetail[GetBuildDefIdFromBuildDefUri(build.BuildDefinitionUri)] = build;

            foreach (var build in buildDetail)
            {
                if (!buildStatuses.ContainsKey(build.Key))
                    buildStatuses[build.Key] = CreateBuildStatus(build.Value, buildDefinitionsQuery, applyBuildQuality);
            }

            return buildStatuses.Values.ToArray();

        }

        private String GetBuildDefIdFromBuildDefUri(Uri uri)
        {
            return uri.Segments[uri.Segments.Length - 1];
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
                    _log.Debug("Unhandeled build status returned from server: " + status);
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
                default:
                    return BuildStatusEnum.Unknown;
            }
        }

        private BuildStatus CreateBuildStatus(IBuildDetail buildDetail, IEnumerable<MyTfsBuildDefinition> allBuildDefinitions, bool applyBuildQuality)
        {
            BuildStatusEnum buildStatus = GetBuildStatusEnum(buildDetail.Status);
            var buildQuality = GetBuildStatusEnum(buildDetail.Quality);
            if (applyBuildQuality && buildStatus == BuildStatusEnum.Working)
                buildStatus = buildQuality;

            var result = new BuildStatus
            {
                CurrentBuildStatus = buildStatus,
                StartedTime = buildDetail.StartTime == DateTime.MinValue ? (DateTime?)null : buildDetail.StartTime,
                FinishedTime = buildDetail.FinishTime == DateTime.MinValue ? (DateTime?)null : buildDetail.FinishTime,
            };

            var matchedBuildDefinition = allBuildDefinitions.FirstOrDefault(i => i.Uri == buildDetail.BuildDefinitionUri);
            if (matchedBuildDefinition != null)
            {
                result.Name = matchedBuildDefinition.Name;
                result.BuildDefinitionId = matchedBuildDefinition.Id;
            }
            else
            {
                _log.Error("The server appeared to return a build definition that we are not watching: " + buildDetail.BuildDefinitionUri);
            }
            
            result.BuildId = buildDetail.Uri.Segments.LastOrDefault();

            SetCheckinInfo(buildDetail, applyBuildQuality, result, buildQuality, matchedBuildDefinition);

            result.Url = _tfsProject.ConvertTfsUriToUrl(buildDetail.Uri);

            return result;
        }

        private static void SetCheckinInfo(IBuildDetail buildDetail, bool applyBuildQuality, BuildStatus result, BuildStatusEnum buildQuality, MyTfsBuildDefinition buildDefinition)
        {
            var checkinInfoGetterService = new CheckinInfoGetterService();
            var checkinInfo = checkinInfoGetterService.GetCheckinInfo(buildDetail, result, buildDefinition);

            if (checkinInfo != null)
            {
                result.Comment = checkinInfo.Comment;
                result.RequestedBy = checkinInfo.Committer;
            }

            if (applyBuildQuality && buildQuality == BuildStatusEnum.Broken)
            {
                result.Comment = "Build deployment or test failure. Please see test server or test results for details.\n" + result.Comment;
            }
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
