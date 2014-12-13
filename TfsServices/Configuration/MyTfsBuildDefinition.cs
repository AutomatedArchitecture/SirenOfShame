using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.SourceControl.WebApi;
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
            // todo: Figure out if this is git or not
            bool isGit = true;
            if (isGit)
            {
                return GetLatestGitChangeset();
            }
            else
            {
                return GetLatestNonGitChangeset();
            }
        }
        
        public CheckinInfo GetLatestNonGitChangeset()
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

        public string ConvertTfsUriToUrl(Uri uri)
        {
            return _myTfsProject.ConvertTfsUriToUrl(uri);
        }

        public CheckinInfo GetLatestGitChangeset()
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
                foreach (var workspaceMappingServerUrl in workspaceMappingServerUrls)
                {
                    var client = _myTfsProject.ProjectCollection.GetGitHttpClient();
                    var repositories = await client.GetRepositoriesAsync();
                    var repository = GetRepositoryId(repositories, workspaceMappingServerUrl);
                    if (repository == null)
                    {
                        _log.Warn("Unable to find a repository for workspace mapping: " + workspaceMappingServerUrl);
                        continue;
                    }
                    var repositoryId = repository.Id;
                    var branches = await client.GetBranchRefsAsync(repositoryId);

                    // Doing a web query for each branch could get expensive fast, but I can't find a better way to get the most recent check-in across all branches with a single query
                    List<GitCommitRef> latestCommitForEachBranch = branches.Select(branchRef => GetMostRecentCheckinFromBranchRef(client, repositoryId, branchRef).Result).ToList();
                    var lastCheckinAcrossAllBranches = latestCommitForEachBranch.Aggregate((i, j) => i.Author.Date > j.Author.Date ? i : j);
                    return new CheckinInfo(lastCheckinAcrossAllBranches);
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Error("Unable to retrieve comments or author for git repository");
                return null;
            }
        }

        private static async Task<GitCommitRef> GetMostRecentCheckinFromBranchRef(GitHttpClient client, Guid repositoryId, GitRef branchRef)
        {

            var gitBranchStats = await client.GetBranchStatisticsAsync(repositoryId.ToString(), "master");
            return gitBranchStats.Commit;
        }

        private GitRepository GetRepositoryId(IEnumerable<GitRepository> repositories, string workspaceMappingServerUrl)
        {
            return repositories.FirstOrDefault(i => workspaceMappingServerUrl.EndsWith(i.Name));
        }

        private MyTfsServer MyTfsServer
        {
            get { return _myTfsProject.MyTfsServer; }
        }
    }
}
