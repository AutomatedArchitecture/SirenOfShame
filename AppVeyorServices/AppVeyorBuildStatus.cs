using AppVeyorServices.AppVeyor;
using ServiceStack;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace AppVeyorServices
{
    public class AppVeyorBuildStatus : BuildStatus
    {
        public AppVeyorBuildStatus(string buildUrl, Project project, ProjectBuild build, BuildDefinitionSetting buildDefinitionSetting,
            bool treatUnstableAsSuccess)
        {
            BuildDefinitionId = buildDefinitionSetting.Id;
            Name = "{0} ({1})".Fmt(project.Name, build.Version);
            BuildStatusEnum = BuildStatusEnum.Unknown;
            StartedTime = build.Started;
            FinishedTime = build.Finished;
            BuildStatusMessage = build.Status;
            Url = buildUrl;
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
                case "QUEUED":
                    return BuildStatusEnum.InProgress;
                case "SUCCESS":
                    return BuildStatusEnum.Working;
                case "FAILED":
                    return BuildStatusEnum.Broken;
                case "RUNNING":
                    return BuildStatusEnum.InProgress;
                case "CANCELLED":
                    return BuildStatusEnum.Unknown;
                case "UNSTABLE":
                    return treatUnstableAsSuccess ? BuildStatusEnum.Working : BuildStatusEnum.Broken;
                default:
                    return BuildStatusEnum.Unknown;
            }
        }
    }
}