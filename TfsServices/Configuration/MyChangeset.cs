using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace TfsServices.Configuration
{
    public class CheckinInfo
    {
        public CheckinInfo(IBuildDetail buildDetail)
        {
            Comment = buildDetail.Reason.ToString();
            Committer = buildDetail.RequestedBy ?? (buildDetail.LastChangedByDisplayName ?? buildDetail.RequestedFor);
        }

        public CheckinInfo() { }

        public CheckinInfo(Changeset changeset)
        {
            Comment = changeset.Comment;
            Committer = changeset.CommitterDisplayName;
        }

        public string Committer { get; set; }
        public string Comment { get; set; }
    }
}