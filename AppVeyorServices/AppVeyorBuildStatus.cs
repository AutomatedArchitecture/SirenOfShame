using AppVeyorServices.AppVeyor;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace AppVeyorServices
{
    public class AppVeyorBuildStatus : BuildStatus
    {
        public AppVeyorBuildStatus(ProjectBuild build, BuildDefinitionSetting buildDefinitionSetting,
            bool treatUnstableAsSuccess)
        {
            BuildDefinitionId = buildDefinitionSetting.Id;
            Name = build.Version;
            BuildStatusEnum = BuildStatusEnum.Unknown;
            StartedTime = build.Started;
            FinishedTime = build.Finished;
            BuildStatusMessage = build.Status;
            Url = string.Empty;
            BuildId = build.BuildId;
            BuildStatusEnum = ToBuildStatusEnum(build.Status, treatUnstableAsSuccess);
            RequestedBy = (string.IsNullOrEmpty(build.AuthorName)) ? build.AuthorName : build.ComitterName;
            Comment = build.Message;
        }

        private static BuildStatusEnum ToBuildStatusEnum(string status, bool treatUnstableAsSuccess)
        {
            if (status == null) return BuildStatusEnum.Unknown;
            status = status.Trim().ToUpperInvariant();
            switch (status)
            {
                case "SUCCESS":
                    return BuildStatusEnum.Working;
                case "FAILED":
                    return BuildStatusEnum.Broken;
                case "RUNNING":
                    return BuildStatusEnum.InProgress;
                case "UNSTABLE":
                    return treatUnstableAsSuccess ? BuildStatusEnum.Working : BuildStatusEnum.Broken;
                default:
                    return BuildStatusEnum.Unknown;
            }
        }
    }
}