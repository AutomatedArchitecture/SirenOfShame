using Microsoft.TeamFoundation.VersionControl.Client;

namespace TfsServices.Configuration
{
    public class MyChangeset
    {
        private readonly Changeset _changeset;
        private readonly string _buildDefinitionId;

        public MyChangeset(Changeset changeset, string buildDefinitionId)
        {
            _changeset = changeset;
            _buildDefinitionId = buildDefinitionId;
        }


        public string BuildDefinitionId
        {
            get { return _buildDefinitionId; }
        }

        public string Comment
        {
            get { return _changeset.Comment; }
        }
    }
}