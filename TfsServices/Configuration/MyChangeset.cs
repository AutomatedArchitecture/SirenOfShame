using System;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace TfsServices.Configuration
{
    /// <summary>
    /// Represents a check-in to TFS (like with check-in comments and stuff)
    /// </summary>
    public class MyChangeset
    {
        private readonly Changeset _changeset;
        private readonly string _buildDefinitionId;
        private readonly MyTfsBuildDefinition _myTfsBuildDefinition;

        public MyChangeset(Changeset changeset, string buildDefinitionId, MyTfsBuildDefinition myTfsBuildDefinition)
        {
            _changeset = changeset;
            _buildDefinitionId = buildDefinitionId;
            _myTfsBuildDefinition = myTfsBuildDefinition;
        }

        public int ChangesetId
        {
            get { return _changeset.ChangesetId; }
        }

        public string BuildDefinitionId
        {
            get { return _buildDefinitionId; }
        }

        public string Comment
        {
            get { return _changeset.Comment; }
        }

        public string ConvertTfsUriToUrl(Uri uri)
        {
            return _myTfsBuildDefinition.ConvertTfsUriToUrl(uri);
        }
    }
}