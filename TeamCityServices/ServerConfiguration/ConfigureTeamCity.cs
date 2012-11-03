using System;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using log4net;

namespace TeamCityServices.ServerConfiguration
{
    public partial class ConfigureTeamCity : ConfigureServerBase
    {
        private const string PLACEHODER_TEXT = "Loading...";
        private readonly TeamCityCiEntryPoint _teamCityCiEntryPoint;
        private readonly TeamCityService _service = new TeamCityService();
        private readonly CiEntryPointSetting _ciEntryPointSetting;
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(ConfigureTeamCity));

        public ConfigureTeamCity() { }

        public ConfigureTeamCity(SirenOfShameSettings sosSettings, TeamCityCiEntryPoint teamCityCiEntryPoint, CiEntryPointSetting ciEntryPointSetting)
            : base(sosSettings)
        {
            _teamCityCiEntryPoint = teamCityCiEntryPoint;
            InitializeComponent();
            _ciEntryPointSetting = ciEntryPointSetting;
            _url.Text = _ciEntryPointSetting.Url;
            _userName.Text = _ciEntryPointSetting.UserName;
            _password.Text = _ciEntryPointSetting.GetPassword();
            if (!string.IsNullOrEmpty(_url.Text))
            {
                ReloadProjects();
            }
        }

        private void ConnectClick(object sender, EventArgs e)
        {
            ReloadProjects();
        }

        private void ClearProjectNodes()
        {
            ClearProjectNodes(_projects.Nodes);
        }

        private void ClearProjectNodes(TreeNodeCollection nodes)
        {
            var treeNodes = nodes.Cast<TreeNode>().ToArray();
            foreach (var treeNode in treeNodes)
            {
                treeNode.Tag = null;
                _projects.Nodes.Remove(treeNode);
            }
        }

        private void ReloadProjects()
        {
            try
            {
                ClearProjectNodes();
                _projects.Nodes.Add("Loading...");
                _service.GetProjects(_url.Text, _userName.Text, _password.Text, GetProjectsComplete, GetProjectsError);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetProjectsError(Exception ex)
        {
            _log.Error("Error connecting to server", ex);
            ClearProjectNodes();

            string message = ex.Message;
            if (ex.InnerException != null)
            {
                message += "\r\n" + ex.InnerException.Message;
            }
            MessageBox.Show("Error connecting to server: " + message);
        }

        private void GetProjectsComplete(TeamCityProject[] projects)
        {
            _ciEntryPointSetting.Url = _url.Text;
            _ciEntryPointSetting.UserName = _userName.Text;
            _ciEntryPointSetting.SetPassword(_password.Text);
            Settings.Save();

            ClearProjectNodes();
            var teamCityProjects = projects.OrderBy(i => i.Name);
            foreach (TeamCityProject project in teamCityProjects)
            {
                bool exists = Settings.BuildExistsAndIsActive(_teamCityCiEntryPoint.Name, project.Name);

                ThreeStateTreeNode node = new ThreeStateTreeNode(project.Name)
                {
                    Tag = project,
                    State = exists ? CheckBoxState.Checked : CheckBoxState.Unchecked
                };
                node.State = CheckBoxState.Indeterminate;
                node.Nodes.Add(PLACEHODER_TEXT);
                _projects.Nodes.Add(node);
                LoadBuildDefinitions(node);
            }
        }

        private void ProjectsBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            LoadBuildDefinitions(e.Node);
        }

        private void LoadBuildDefinitions(TreeNode node)
        {
            if (node.Tag is TeamCityProject && node.Nodes.Count == 1 && node.Nodes[0].Text == PLACEHODER_TEXT)
            {
                _service.GetBuildDefinitions((TeamCityProject)node.Tag, _userName.Text, _password.Text, buildDefinitions =>
                {
                    ClearProjectNodes(node.Nodes);
                    var activeBuildDefinitionSettings = _ciEntryPointSetting.BuildDefinitionSettings.Where(bd => bd.Active);
                    foreach (TeamCityBuildDefinition buildDefinition in buildDefinitions)
                    {
                        TeamCityBuildDefinition definition = buildDefinition;
                        ThreeStateTreeNode buildDefinitionNode = new ThreeStateTreeNode(buildDefinition.Name)
                        {
                            Tag = buildDefinition
                        };
                        var buildDefSettings = activeBuildDefinitionSettings.FirstOrDefault(bd => bd.Id == definition.Id);
                        if (buildDefSettings != null)
                        {
                            buildDefinitionNode.State = buildDefSettings.Active ? CheckBoxState.Checked : CheckBoxState.Unchecked;
                        }
                        node.Nodes.Add(buildDefinitionNode);
                        buildDefinitionNode.UpdateStateOfRelatedNodes();
                    }
                });
            }
        }

        private void ProjectsAfterCheck(object sender, TreeViewEventArgs e)
        {
            TeamCityBuildDefinition buildDefinition = e.Node.Tag as TeamCityBuildDefinition;
            if (buildDefinition != null)
            {
                var buildDefSetting = _ciEntryPointSetting.FindAddBuildDefinition(buildDefinition, _teamCityCiEntryPoint.Name);
                buildDefSetting.Active = e.Node.Checked;
                Settings.Save();
            }
            var threeStateTreeNode = e.Node as ThreeStateTreeNode;
            if (threeStateTreeNode != null)
            {
                threeStateTreeNode.UpdateStateOfRelatedNodes();
            }
        }
    }
}
