using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AppVeyorServices.Properties;
using log4net;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;

namespace AppVeyorServices.ServerConfiguration
{
    public partial class ConfigureAppVeyor : ConfigureServerBase
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof (ConfigureAppVeyor));
        private readonly AppVeyorCiEntryPoint _appVeyorCiEntryPoint;
        private readonly CiEntryPointSetting _ciEntryPointSetting;
        private List<AppVeyorBuildDefinition> _buildDefinitions = new List<AppVeyorBuildDefinition>();

        public ConfigureAppVeyor()
        {
        }

        public ConfigureAppVeyor(SirenOfShameSettings sosSettings, AppVeyorCiEntryPoint appVeyorCiEntryPoint,
            CiEntryPointSetting ciEntryPointSetting)
            : base(sosSettings)
        {
            _appVeyorCiEntryPoint = appVeyorCiEntryPoint;
            InitializeComponent();
            _ciEntryPointSetting = ciEntryPointSetting;
            _url.Text = _ciEntryPointSetting.Url;
            _password.Text = _ciEntryPointSetting.GetPassword();
        }

        private void ConnectClick(object sender, EventArgs e)
        {
            ReloadProjects();
        }

        private void ReloadProjects()
        {
            try
            {
                _projects.Nodes.Clear();
                _projects.Nodes.Add("Loading...");
                AppVeyorService.GetProjects(_url.Text, _password.Text, GetProjectsComplete, GetProjectsError);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetProjectsError(Exception ex)
        {
            _projects.Nodes.Clear();
            _log.Error(ex);
            MessageBox.Show(Resources.ConfigureAppVeyor_ServerError + ex.Message);
        }

        private void GetProjectsComplete(IEnumerable<AppVeyorBuildDefinition> buildDefinitions)
        {
            _ciEntryPointSetting.Url = _url.Text;
            _ciEntryPointSetting.UserName = string.Empty;
            _ciEntryPointSetting.SetPassword(_password.Text);
            Settings.Save();

            _projects.Nodes.Clear();
            _buildDefinitions = buildDefinitions.OrderBy(i => i.Name).ToList();
            ApplyFilter();
        }

        private void ProjectsAfterCheck(object sender, TreeViewEventArgs e)
        {
            var buildDefinition = e.Node.Tag as AppVeyorBuildDefinition;
            if (buildDefinition != null)
            {
                var buildDefSetting = _ciEntryPointSetting.FindAddBuildDefinition(buildDefinition,
                    _appVeyorCiEntryPoint.Name);
                buildDefSetting.Active = e.Node.Checked;
                Settings.Save();
            }
            ((ThreeStateTreeNode) e.Node).UpdateStateOfRelatedNodes();
        }

        private void _filter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            _projects.Nodes.Clear();

            foreach (var project in _buildDefinitions)
            {
                var shouldBeVisible = string.IsNullOrEmpty(_filter.Text) ||
                                      project.Name.Contains(_filter.Text, StringComparison.CurrentCultureIgnoreCase);
                if (!shouldBeVisible) continue;
                var exists = Settings.BuildExistsAndIsActive(_appVeyorCiEntryPoint.Name, project.Name);

                var node = new ThreeStateTreeNode(project.Name)
                {
                    Tag = project,
                    State = exists ? CheckBoxState.Checked : CheckBoxState.Unchecked
                };
                _projects.Nodes.Add(node);
            }
        }

        private void _search_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void _checkAll_Click(object sender, EventArgs e)
        {
            var allChecked = _projects.Nodes.Cast<ThreeStateTreeNode>().All(i => i.Checked);
            foreach (var node in _projects.Nodes.Cast<ThreeStateTreeNode>())
            {
                node.Checked = !allChecked;
            }
        }
    }
}