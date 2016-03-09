using System.Collections.Generic;
using SirenOfShame.Lib.Watcher;

namespace TfsRestServices
{
    public class TfsRestBuildStatus : BuildStatus
    {
        public TfsRestBuildStatus(TfsJsonBuildDefinition tfsRestBuildDefinition)
        {
            BuildStatusEnum = GetBuildStatus(tfsRestBuildDefinition);
            Name = tfsRestBuildDefinition.Definition.Name;
            BuildDefinitionId = tfsRestBuildDefinition.Definition.Id.ToString();
            RequestedBy = tfsRestBuildDefinition.RequestedFor.DisplayName;
            BuildId = tfsRestBuildDefinition.Id.ToString();
        }

        private static readonly Dictionary<string, BuildStatusEnum> _buildStatusMapping = new Dictionary<string, BuildStatusEnum>
        {
            {"succeeded", BuildStatusEnum.Working},
            {"partiallySucceeded", BuildStatusEnum.Working},
            {"failed", BuildStatusEnum.Broken},
            {"canceled", BuildStatusEnum.Unknown},
        };

        private static BuildStatusEnum GetBuildStatus(TfsJsonBuildDefinition tfsRestBuildDefinition)
        {
            BuildStatusEnum buildStatusEnum;
            return _buildStatusMapping.TryGetValue(tfsRestBuildDefinition.Result, out buildStatusEnum) ? buildStatusEnum : BuildStatusEnum.Unknown;
        }
    }
}