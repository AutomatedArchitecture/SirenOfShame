using System;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;

namespace TeamCityServices.ServerConfiguration
{
    public partial class ConfigureTeamCity : ConfigureServerBase
    {
        private const string PlacehoderText = "Loading...";
        private readonly TeamCityCiEntryPoint _teamCityCiEntryPoint;
        private readonly TeamCityService _service = new TeamCityService();
        private bool _updatingTree;

        public ConfigureTeamCity() { }

        public ConfigureTeamCity(SirenOfShameSettings sosSettings, TeamCityCiEntryPoint teamCityCiEntryPoint)
            : base(sosSettings)
        {
            _teamCityCiEntryPoint = teamCityCiEntryPoint;
            InitializeComponent();
            CiEntryPointSettings settings = Settings.FindAddSettings(_teamCityCiEntryPoint.Name);
            _url.Text = settings.Url;
            _userName.Text = settings.UserName;
            _password.Text = settings.Password;
            if (!string.IsNullOrEmpty(_url.Text))
            {
                ReloadProjects();
            }
        }

        private void _connect_Click(object sender, EventArgs e)
        {
            ReloadProjects();
        }

        private void ReloadProjects()
        {
            _projects.Nodes.Clear();
            _projects.Nodes.Add("Loading...");
            _service.GetProjects(_url.Text, _userName.Text, _password.Text, GetProjectsComplete, GetProjectsError);
        }

        private void GetProjectsError(Exception ex)
        {
            _projects.Nodes.Clear();
            MessageBox.Show("Error connecting to server: " + ex.Message);
        }

        private void GetProjectsComplete(TeamCityProject[] projects)
        {
            CiEntryPointSettings settings = Settings.FindAddSettings(_teamCityCiEntryPoint.Name);
            settings.Url = _url.Text;
            settings.UserName = _userName.Text;
            settings.Password = _password.Text;
            Settings.Save();

            _projects.Nodes.Clear();
            foreach (TeamCityProject project in projects)
            {
                ThreeStateTreeNode node = new ThreeStateTreeNode(project.Name)
                {
                    Tag = project
                };
                node.State = CheckBoxState.Indeterminate;
                node.Nodes.Add(PlacehoderText);
                _projects.Nodes.Add(node);
                LoadBuildDefinitions(node);
            }
        }

        private void _projects_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            LoadBuildDefinitions(e.Node);
        }

        private void LoadBuildDefinitions(TreeNode node)
        {
            if (node.Tag is TeamCityProject && node.Nodes.Count == 1 && node.Nodes[0].Text == PlacehoderText)
            {
                _service.GetBuildDefinitions((TeamCityProject)node.Tag, _userName.Text, _password.Text, buildDefinitions =>
                {
                    node.Nodes.Clear();
                    var activeBuildDefinitionSettings = Settings.BuildDefinitionSettings.Where(bd => bd.Active);
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

        private void _projects_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is TeamCityBuildDefinition)
            {
                var buildDefinition = (TeamCityBuildDefinition)e.Node.Tag;
                var buildDefSetting = Settings.FindAddBuildDefinition(buildDefinition, _teamCityCiEntryPoint.Name);
                buildDefSetting.Active = e.Node.Checked;
                Settings.Save();
            }
            ((ThreeStateTreeNode)e.Node).UpdateStateOfRelatedNodes();
        }
    }
}
