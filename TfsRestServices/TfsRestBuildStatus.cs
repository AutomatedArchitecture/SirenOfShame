using System;
using System.Diagnostics;
using SirenOfShame.Lib.Watcher;

namespace TfsRestServices
{
    public class TfsRestBuildStatus : BuildStatus
    {
        public TfsRestBuildStatus(TfsJsonBuildDefinition tfsRestBuildDefinition)
        {
            BuildStatusEnum = tfsRestBuildDefinition.Result == "succeeded" ? BuildStatusEnum.Working : BuildStatusEnum.Broken;
            Name = tfsRestBuildDefinition.Definition.Name;
            BuildDefinitionId = tfsRestBuildDefinition.Definition.Id.ToString();
        }

        
    }
}