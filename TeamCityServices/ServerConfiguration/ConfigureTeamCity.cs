using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private const string PLACEHOLDER_TEXT = "Loading...";
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
            Load += OnLoad;
        }

        private async void OnLoad(object sender, EventArgs eventArgs)
        {
            if (!string.IsNullOrEmpty(_url.Text))
            {
                await ReloadProjects();
            }
        }

        private async void ConnectClick(object sender, EventArgs e)
        {
            await ReloadProjects();
        }

        private void ClearProjectNodes()
        {
            _projects.Nodes.Clear();
        }

        private async Task ReloadProjects()
        {
            try
            {
                ClearProjectNodes();
                _projects.Nodes.Add(PLACEHOLDER_TEXT);
                try
                {
                    var projectsResult = await Task.Run(async () =>
                    {
                        var projects = await _service.GetProjects(_url.Text, _userName.Text, _password.Text);
                        return projects;
                    });
                    GetProjectsComplete(projectsResult);
                }
                catch (Exception ex)
                {
                    GetProjectsError(ex);
                }
            }
            catch (Exception ex)
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
            SaveCredentials();
            ClearProjectNodes();
            AddRootProjects(projects);
        }

        private void AddRootProjects(TeamCityProject[] rootProjects)
        {
            var activeBuildDefinitionSettings = _ciEntryPointSetting.BuildDefinitionSettings.Where(bd => bd.Active).ToList();
            foreach (TeamCityProject project in rootProjects)
            {
                AddSubProjectsAndBuildDefinitions(project, activeBuildDefinitionSettings, _projects.Nodes);
            }
        }

        private bool AddSubProjectsAndBuildDefinitions(TeamCityProject project, List<BuildDefinitionSetting> activeBuildDefinitionSettings, TreeNodeCollection treeNodeCollection)
        {
            ThreeStateTreeNode node = new ThreeStateTreeNode(project.Name);
            bool allChildrenChecked = true;

            foreach (var buildDefinition in project.BuildDefinitions)
            {
                var isChecked = AddBuildDefinition(buildDefinition, node, activeBuildDefinitionSettings);
                allChildrenChecked = allChildrenChecked && isChecked;
            }

            foreach (var subProject in project.SubProjects)
            {
                var isChecked = AddSubProjectsAndBuildDefinitions(subProject, activeBuildDefinitionSettings, node.Nodes);
                allChildrenChecked = allChildrenChecked && isChecked;
            }

            node.Checked = allChildrenChecked;

            treeNodeCollection.Add(node);

            return node.Checked;
        }

        private void SaveCredentials()
        {
            _ciEntryPointSetting.Url = _url.Text;
            _ciEntryPointSetting.UserName = _userName.Text;
            _ciEntryPointSetting.SetPassword(_password.Text);
            Settings.Save();
        }

        private bool AddBuildDefinition(TeamCityBuildDefinition buildDefinition, ThreeStateTreeNode parentProjectNode, IEnumerable<BuildDefinitionSetting> activeBuildDefinitionSettings)
        {
            ThreeStateTreeNode buildDefinitionNode = new ThreeStateTreeNode(buildDefinition.Name)
            {
                Tag = buildDefinition
            };
            var buildDefSettings = activeBuildDefinitionSettings.FirstOrDefault(bd => bd.Id == buildDefinition.Id);
            if (buildDefSettings != null)
            {
                buildDefinitionNode.State = buildDefSettings.Active ? CheckBoxState.Checked : CheckBoxState.Unchecked;
            }
            parentProjectNode.Nodes.Add(buildDefinitionNode);
            buildDefinitionNode.UpdateStateOfRelatedNodes();
            return buildDefinitionNode.Checked;
        }

        private void Projects_AfterCheck(object sender, TreeViewEventArgs e)
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
