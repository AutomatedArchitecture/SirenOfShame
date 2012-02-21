using System;
using System.Linq;
using System.Windows.Forms;
using log4net;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Settings;

namespace TfsServices.Configuration
{
    public class MyTfsBuildDefinition : MyBuildDefinition
    {
        private readonly IBuildDefinition _buildDefinition;
        private readonly MyTfsProject _myTfsProject;
        private static readonly ILog Log = MyLogManager.GetLogger(typeof(MyTfsBuildDefinition));

        public MyTfsBuildDefinition(IBuildDefinition buildDefinition, MyTfsProject myTfsProject)
        {
            _buildDefinition = buildDefinition;
            _myTfsProject = myTfsProject;
        }

        public override string Name
        {
            get { return _buildDefinition.Name; }
        }

        public override string Id
        {
            get { return _buildDefinition.Name; }
        }

        public MyBuildServer BuildServer
        {
            get { return new MyBuildServer(_buildDefinition.BuildServer); }
        }

        public Uri Uri
        {
            get { return _buildDefinition.Uri; }
        }

        public TreeNode GetAsNode(bool active)
        {
            return new TreeNode(Name) { Tag = Id, Checked = active };
        }

        public MyChangeset GetLatestChangeset()
        {
            // Exclude cloaked mappings - they can't have changesets.
            var serverUrls = _buildDefinition.Workspace.Mappings
                                    .Where(m => m.MappingType != WorkspaceMappingType.Cloak)
                                    .Select(m => m.ServerItem);
            if (!serverUrls.Any())
            {
                Log.Warn(string.Format("Build definition {0} does not have any workspace mappings so can't retrieve comments", Id));
                return null;
            }

            Changeset maxChangeset = null;
            var vc = _myTfsProject.ProjectCollection.VersionControlServer;
            const int deletionId = 0;
            foreach (var workspaceMapping in serverUrls)
            {
                var changesets = vc.QueryHistory(workspaceMapping,
                                                         VersionSpec.Latest,
                                                         deletionId,
                                                         RecursionType.Full,
                                                         null,
                                                         null,
                                                         VersionSpec.Latest,
                                                         1,
                                                         true,
                                                         false,
                                                         true);
                if (changesets == null)  // mapping may not be on the server yet.
                    continue;
                Changeset changeset = changesets.Cast<Changeset>().FirstOrDefault();
                if (maxChangeset == null || changeset.ChangesetId > maxChangeset.ChangesetId)
                {
                    maxChangeset = changeset;
                }
            }
            
            return new MyChangeset(maxChangeset, Id, this);
        }

        public string ConvertTfsUriToUrl(Uri uri)
        {
            return _myTfsProject.ConvertTfsUriToUrl(uri);
        }
    }
}
