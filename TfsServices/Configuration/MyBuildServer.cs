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
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(MyBuildServer));
        private readonly IBuildServer _buildServer;
        private readonly MyTfsProject _tfsProject;
        private bool _firstRequest = true;
        readonly Dictionary<String, String> _uriToName = new Dictionary<String, String>();

        public MyBuildServer(IBuildServer buildServer, MyTfsProject myTfsProject)
        {
            _buildServer = buildServer;
            _tfsProject = myTfsProject;
        }

        public IEnumerable<BuildStatus> GetBuildStatuses(IEnumerable<MyTfsBuildDefinition> buildDefinitionsQuery, bool applyBuildQuality)
        {
            var buildDefinitionUris = buildDefinitionsQuery.Select(bd => bd.Uri).ToArray();

            IBuildDetailSpec[] buildDetailSpec = new IBuildDetailSpec[2];

            buildDetailSpec[0] = _buildServer.CreateBuildDetailSpec(buildDefinitionUris);
            buildDetailSpec[0].Status = Microsoft.TeamFoundation.Build.Client.BuildStatus.InProgress;
            buildDetailSpec[0].QueryOrder = BuildQueryOrder.FinishTimeDescending;
            buildDetailSpec[0].InformationTypes = new[] { "AssociatedChangeset" };
            buildDetailSpec[0].QueryOptions = _firstRequest ? QueryOptions.Process : QueryOptions.None;

            buildDetailSpec[1] = _buildServer.CreateBuildDetailSpec(buildDefinitionUris);
            buildDetailSpec[1].MaxBuildsPerDefinition = 1;
            buildDetailSpec[1].Status = Microsoft.TeamFoundation.Build.Client.BuildStatus.All;
            buildDetailSpec[1].QueryOrder = BuildQueryOrder.FinishTimeDescending;
            buildDetailSpec[1].InformationTypes = new[] { "AssociatedChangeset" };
            buildDetailSpec[1].QueryOptions = _firstRequest ? QueryOptions.Process : QueryOptions.None;

            _firstRequest = false;

            IBuildQueryResult[] buildQueryResult = _buildServer.QueryBuilds(buildDetailSpec);

            Dictionary<String, BuildStatus> buildStatuses = new Dictionary<String, BuildStatus>();
            Dictionary<String, IBuildDetail> buildDetail = new Dictionary<String, IBuildDetail>();

            // Get last completed for each def
            foreach (var build in buildQueryResult[1].Builds)
                buildDetail[GetBuildDefIdFromBuildDefUri(build.BuildDefinitionUri)] = build;

            // Get current build (if any) and overwrite last completed
            foreach (var build in buildQueryResult[0].Builds)
                buildDetail[GetBuildDefIdFromBuildDefUri(build.BuildDefinitionUri)] = build;

            foreach (var build in buildDetail)
            {
                if (!buildStatuses.ContainsKey(build.Key))
                    buildStatuses[build.Key] = CreateBuildStatus(build.Value, applyBuildQuality);
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

        private BuildStatus CreateBuildStatus(IBuildDetail buildDetail, bool applyBuildQuality)
        {
            BuildStatusEnum status = GetBuildStatusEnum(buildDetail.Status);
            if (applyBuildQuality && status == BuildStatusEnum.Working)
                status = GetBuildStatusEnum(buildDetail.Quality);
            
            var result = new BuildStatus
            {
                BuildDefinitionId = buildDetail.BuildDefinitionUri.Segments[buildDetail.BuildDefinitionUri.Segments.Length - 1],
                BuildStatusEnum = status,
                RequestedBy = buildDetail.RequestedBy ?? (buildDetail.RequestedFor ?? buildDetail.LastChangedBy),
                StartedTime = buildDetail.StartTime == DateTime.MinValue ? (DateTime?)null : buildDetail.StartTime,
                FinishedTime = buildDetail.FinishTime == DateTime.MinValue ? (DateTime?)null : buildDetail.FinishTime,
            };

            if (buildDetail.BuildDefinition != null)
            {
                _uriToName[buildDetail.BuildDefinitionUri.ToString()] = buildDetail.BuildDefinition.Name;
            }

            result.Name = _uriToName[buildDetail.BuildDefinitionUri.ToString()];
            result.BuildId = buildDetail.Uri.Segments[buildDetail.Uri.Segments.Length - 1];

            var changesets = buildDetail.Information.GetNodesByType("AssociatedChangeset");

            if (changesets.Any())
            {
                result.RequestedBy = GetRequestedBy(changesets);

                if (changesets.Count() > 1)
                    result.Comment = "(Multiple Changesets)";
                else
                    result.Comment = changesets.First().Fields["Comment"];
            }
            else 
            {
                result.Comment = buildDetail.Reason.ToString();
            }
            
            if (applyBuildQuality &&
                GetBuildStatusEnum(buildDetail.Quality) == BuildStatusEnum.Broken)
            {
                result.Comment =
                    "Build deployment or test failure. Please see test server or test results for details.\n" +
                    result.Comment;
            }

            result.Url = _tfsProject.ConvertTfsUriToUrl(buildDetail.Uri);

            return result;
        }

        private static string GetRequestedBy(IEnumerable<IBuildInformationNode> changesets)
        {
            HashSet<String> users = new HashSet<String>();
            foreach (var changeset in changesets)
                users.Add(changeset.Fields["CheckedInBy"]);

            var count = users.Count();
            if (count > 1)
                return "(Multiple Users)";
            return users.First();
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
