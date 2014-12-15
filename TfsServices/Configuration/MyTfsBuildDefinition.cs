using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(MyTfsBuildDefinition));
        private readonly MyBuildServer _myBuildServer;

        public MyTfsBuildDefinition(IBuildDefinition buildDefinition, MyTfsProject myTfsProject)
        {
            _buildDefinition = buildDefinition;
            _myTfsProject = myTfsProject;
            _myBuildServer = new MyBuildServer(_buildDefinition.BuildServer, _myTfsProject);
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
            get { return _myBuildServer; }
        }

        public Uri Uri
        {
            get { return _buildDefinition.Uri; }
        }

        public TreeNode GetAsNode(bool active)
        {
            return new TreeNode(Name) { Tag = Id, Checked = active };
        }

        private const int DELETION_ID = 0;

        public CheckinInfo GetLatestChangeset()
        {
            if (IsGit)
            {
                return GetLatestGitChangeset();
            }
            return GetLatestNonGitChangeset();
        }

        private bool IsGit
        {
            get { return _buildDefinition.SourceProviders.Any(i => i.Name == "TFGIT"); }
        }

        private CheckinInfo GetLatestNonGitChangeset()
        {
            try
            {
                // generally there is only one workspace mapping per build definition, but there could be >1
                IList<string> workspaceMappingServerUrls = GetNonCloakedWorkspaceMappingServerUrls().ToList();
                if (!workspaceMappingServerUrls.Any())
                {
                    _log.Warn(string.Format("Build definition {0} does not have any workspace mappings so can't retrieve comments", Id));
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

                return maxChangeset == null ? null : new CheckinInfo(maxChangeset);
            }
            catch (Exception ex)
            {
                _log.Error("Unable to retrieve comments for build definition " + Id, ex);
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
                                                     DELETION_ID,
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

        private CheckinInfo GetLatestGitChangeset()
        {
            return GetLatestGitChangesetAsync().Result;
        }

        private async Task<CheckinInfo> GetLatestGitChangesetAsync()
        {
            // generally there is only one workspace mapping per build definition, but there could be >1
            IList<string> workspaceMappingServerUrls = GetNonCloakedWorkspaceMappingServerUrls().ToList();
            if (!workspaceMappingServerUrls.Any())
            {
                _log.Warn(string.Format("Build definition {0} does not have any workspace mappings so can't retrieve comments", Id));
                return null;
            }
            try
            {
                var repositoryId = await _myTfsProject.ProjectCollection.ExecuteGetHttpClientRequest<Guid?>("/_apis/git/repositories", repositories =>
                {
                    foreach (var workspaceMappingServerUrl in workspaceMappingServerUrls)
                    {
                        foreach (var repository in repositories)
                        {
                            string repositoryName = repository.name;
                            if (workspaceMappingServerUrl.EndsWith(repositoryName))
                            {
                                return repository.id;
                            }
                        }
                    }
                    return null;
                });

                var getCommitsUrl = "/_apis/git/repositories/" + repositoryId + "/commits?top=1";
                var commit = await _myTfsProject.ProjectCollection.ExecuteGetHttpClientRequest(getCommitsUrl, commits =>
                {
                    var comment = commits[0].comment;
                    var author = commits[0].author.name;
                    return new CheckinInfo
                    {
                        Comment = comment,
                        Committer = author
                    };
                });
                return commit;
            }
            catch (Exception ex)
            {
                _log.Error("Unable to retrieve comments or author for git repository", ex);
                return null;
            }
        }
    }
}
