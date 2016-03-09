using System.Collections.Generic;
using SirenOfShame.Lib.Watcher;

namespace TfsRestServices
{
    public class TfsRestBuildStatus : BuildStatus
    {
        public TfsRestBuildStatus(TfsJsonBuild tfsRestBuildDefinition, CommentsCache commentsCache)
        {
            BuildStatusEnum = GetBuildStatus(tfsRestBuildDefinition);
            Name = tfsRestBuildDefinition.Definition.Name;
            BuildDefinitionId = tfsRestBuildDefinition.Definition.Id.ToString();
            RequestedBy = tfsRestBuildDefinition.RequestedFor.DisplayName;
            BuildId = tfsRestBuildDefinition.Id.ToString();
            Url = tfsRestBuildDefinition._links.Web.Href;
            Comment = commentsCache.GetCachedCommentForBuild(tfsRestBuildDefinition);
        }

        private static readonly Dictionary<string, BuildStatusEnum> _buildStatusMapping = new Dictionary<string, BuildStatusEnum>
        {
            {"succeeded", BuildStatusEnum.Working},
            {"partiallySucceeded", BuildStatusEnum.Working},
            {"failed", BuildStatusEnum.Broken},
            {"canceled", BuildStatusEnum.Unknown},
        };

        private static BuildStatusEnum GetBuildStatus(TfsJsonBuild tfsRestBuildDefinition)
        {
            if (tfsRestBuildDefinition.Status == "inProgress") return BuildStatusEnum.InProgress;
            BuildStatusEnum buildStatusEnum;
            return _buildStatusMapping.TryGetValue(tfsRestBuildDefinition.Result, out buildStatusEnum) ? buildStatusEnum : BuildStatusEnum.Unknown;
        }
    }
}