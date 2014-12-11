using Microsoft.TeamFoundation.VersionControl.Client;

namespace TfsServices.Configuration
{
    /// <summary>
    /// Represents a check-in to TFS (like with check-in comments and stuff)
    /// </summary>
    public class MyChangeset
    {
        public MyChangeset(Changeset changeset)
        {
            ChangesetId = changeset.ChangesetId;
            Comment = changeset.Comment;
            Committer = changeset.Committer;
            CommitterDisplayName = changeset.CommitterDisplayName;
        }

        public string CommitterDisplayName { get; set; }

        public string Committer { get; set; }

        public int ChangesetId { get; private set; }

        public string Comment { get; private set; }
    }
}