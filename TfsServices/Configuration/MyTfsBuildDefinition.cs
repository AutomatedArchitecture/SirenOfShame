using System;
using System.Collections;
using System.Collections.Generic;
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

        private const int DeletionId = 0;

        public MyChangeset GetLatestChangeset()
        {
            try
            {
                // generally there is only one workspace mapping per build definition, but there could be >1
                IEnumerable<string> workspaceMappingServerUrls = GetNonCloakedWorkspaceMappingServerUrls();
                var noWorkspaceMappings = !workspaceMappingServerUrls.Any();
                if (noWorkspaceMappings)
                {
                    Log.Warn(string.Format("Build definition {0} does not have any workspace mappings so can't retrieve comments", Id));
                    return null;
                }

                Changeset maxChangeset = null;
                var versionControlServer = _myTfsProject.GetVersionControlServer();
                foreach (var workspaceMappingServerUrl in workspaceMappingServerUrls)
                {
                    IEnumerable changesets = GetChangesetsFromServer(versionControlServer, workspaceMappingServerUrl);
                    Changeset changeset = GetMostRecentChangeset(changesets);
                    var thisIsTheFirstWorkspaceMapping = maxChangeset == null;
                    var theCurrentChangesetIsMoreRecentThanTheMax = thisIsTheFirstWorkspaceMapping || changeset.ChangesetId > maxChangeset.ChangesetId;
                    if (theCurrentChangesetIsMoreRecentThanTheMax)
                    {
                        maxChangeset = changeset;
                    }
                }

                return maxChangeset == null ? null : new MyChangeset(maxChangeset, Id, this);
            } 
            catch (Exception ex)
            {
                Log.Error("Unable to retrieve comments for build definition " + Id, ex);
                return null; // errors in getting comments 
            }
        }

        private static Changeset GetMostRecentChangeset(IEnumerable changesets)
        {
            return changesets == null ? null : changesets.Cast<Changeset>().FirstOrDefault();
        }

        private static IEnumerable GetChangesetsFromServer(VersionControlServer versionControlServer, string workspaceMappingServerUrl)
        {
            return versionControlServer.QueryHistory(workspaceMappingServerUrl,
                                                     VersionSpec.Latest,
                                                     DeletionId,
                                                     RecursionType.Full,
                                                     null,
                                                     null,
                                                     VersionSpec.Latest,
                                                     1,
                                                     true,
                                                     false,
                                                     true);
        }

        // Exclude cloaked mappings - they can't have changesets.
        private IEnumerable<string> GetNonCloakedWorkspaceMappingServerUrls()
        {
            return _buildDefinition.Workspace.Mappings
                .Where(m => m.MappingType != WorkspaceMappingType.Cloak)
                .Select(m => m.ServerItem);
        }

        public string ConvertTfsUriToUrl(Uri uri)
        {
            return _myTfsProject.ConvertTfsUriToUrl(uri);
        }
    }
}
