using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using log4net;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Framework.Client;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Watcher;
using BuildStatus = SirenOfShame.Lib.Watcher.BuildStatus;

namespace TfsServices.Configuration
{
    public class MyBuildServer
    {
        private static readonly ILog Log = MyLogManager.GetLogger(typeof(MyBuildServer));
        private readonly IBuildServer _buildServer;

        public MyBuildServer(IBuildServer buildServer)
        {
            _buildServer = buildServer;
        }

        public IEnumerable<BuildStatus> GetBuildStatuses(IEnumerable<MyTfsBuildDefinition> buildDefinitions)
        {
            try
            {
                var buildDefinitionUris = buildDefinitions.Select(bd => bd.Uri);
                IBuildDetailSpec buildDetailSpec = _buildServer.CreateBuildDetailSpec(buildDefinitionUris);
                buildDetailSpec.MaxBuildsPerDefinition = 1;
                buildDetailSpec.QueryOrder = BuildQueryOrder.FinishTimeDescending;

                IBuildQueryResult buildQueryResults = _buildServer.QueryBuilds(buildDetailSpec);
                var latestChangesets = buildDefinitions.Select(bd => bd.GetLatestChangeset());
                var successfulChangesets = latestChangesets.Where(c => c != null);

                var buildStatusResultsJoined = from buildQueryResult in buildQueryResults.Builds
                                               from changeset in successfulChangesets.Where(sc => sc.BuildDefinitionId == buildQueryResult.BuildDefinition.Name).DefaultIfEmpty()
                                               select CreateBuildStatus(buildQueryResult, changeset);

                return buildStatusResultsJoined;
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (DatabaseConnectionException ex)
            {
                throw new ServerUnavailableException(ex.Message, ex);
            }
            catch (TeamFoundationServiceUnavailableException ex)
            {
                throw new ServerUnavailableException(ex.Message, ex);
            }
            catch (BuildServerException ex)
            {
                Log.Error("Error connecting to server", ex);
                throw new ServerUnavailableException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Error("Error connecting to server", ex);
                throw new ServerUnavailableException(ex.Message, ex);
            }
        }

        private static BuildStatusEnum GetBuildStatusEnum(Microsoft.TeamFoundation.Build.Client.BuildStatus status)
        {
            switch (status)
            {
                case (Microsoft.TeamFoundation.Build.Client.BuildStatus.Failed):
                    return BuildStatusEnum.Broken;
                case (Microsoft.TeamFoundation.Build.Client.BuildStatus.Succeeded):
                    return BuildStatusEnum.Working;
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

        public static BuildStatus CreateBuildStatus(IBuildDetail buildDetail, MyChangeset changeset)
        {
            return new BuildStatus
            {
                Id = buildDetail.BuildDefinition.Name,
                Name = buildDetail.BuildDefinition.Name,
                BuildStatusEnum = GetBuildStatusEnum(buildDetail.Status),
                RequestedBy = buildDetail.RequestedFor,
                StartedTime = buildDetail.StartTime,
                FinishedTime = buildDetail.FinishTime,
                Comment = changeset.Comment
            };
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

            if (!typeof(MyBuildServer).IsAssignableFrom(obj.GetType())) return false;
            var theirUri = ((MyBuildServer)obj).BuildServerUri;
            return myUri.Equals(theirUri);
        }

        public Uri BuildServerUri
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
