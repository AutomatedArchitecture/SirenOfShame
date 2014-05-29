using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using log4net;

namespace HudsonServices.ServerConfiguration
{
    public partial class ConfigureHudson : ConfigureServerBase
    {
        private List<HudsonBuildDefinition> _buildDefinitions = new List<HudsonBuildDefinition>();
        private readonly HudsonCIEntryPoint _hudsonCiEntryPoint;
        private readonly CiEntryPointSetting _ciEntryPointSetting;
        private readonly HudsonService _service = new HudsonService();
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(ConfigureHudson));

        public ConfigureHudson() { }

        public ConfigureHudson(SirenOfShameSettings sosSettings, HudsonCIEntryPoint hudsonCiEntryPoint, CiEntryPointSetting ciEntryPointSetting)
            : base(sosSettings)
        {
            _hudsonCiEntryPoint = hudsonCiEntryPoint;
            InitializeComponent();
            _ciEntryPointSetting = ciEntryPointSetting;
            _url.Text = _ciEntryPointSetting.Url;
            _userName.Text = _ciEntryPointSetting.UserName;
            _password.Text = _ciEntryPointSetting.GetPassword();
            _treatUnstableAsSuccess.Checked = _ciEntryPointSetting.TreatUnstableAsSuccess;
            if (!string.IsNullOrEmpty(_url.Text))
            {
                ReloadProjects();
            }
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
                _service.GetProjects(_url.Text, _userName.Text, _password.Text, GetProjectsComplete, GetProjectsError);
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
            MessageBox.Show("Error connecting to server: " + ex.Message);
        }

        private void GetProjectsComplete(HudsonBuildDefinition[] buildDefinitions)
        {
            _ciEntryPointSetting.Url = _url.Text;
            _ciEntryPointSetting.UserName = _userName.Text;
            _ciEntryPointSetting.SetPassword(_password.Text);
            Settings.Save();

            _projects.Nodes.Clear();
            _buildDefinitions = buildDefinitions.OrderBy(i => i.Name).ToList();
            ApplyFilter();
        }

        private void ProjectsAfterCheck(object sender, TreeViewEventArgs e)
        {
            var buildDefinition = e.Node.Tag as HudsonBuildDefinition;
            if (buildDefinition != null)
            {
                var buildDefSetting = _ciEntryPointSetting.FindAddBuildDefinition(buildDefinition, _hudsonCiEntryPoint.Name);
                buildDefSetting.Active = e.Node.Checked;
                Settings.Save();
            }
            ((ThreeStateTreeNode)e.Node).UpdateStateOfRelatedNodes();
        }

        private void _treatUnstableAsSuccess_CheckedChanged(object sender, EventArgs e)
        {
            _ciEntryPointSetting.TreatUnstableAsSuccess = _treatUnstableAsSuccess.Checked;
            Settings.Save();
        }

        private void _filter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            _projects.Nodes.Clear();

            foreach (HudsonBuildDefinition project in _buildDefinitions)
            {
                var shouldBeVisible = string.IsNullOrEmpty(_filter.Text) || project.Name.Contains(_filter.Text, StringComparison.CurrentCultureIgnoreCase);
                if (!shouldBeVisible) continue;
                bool exists = Settings.BuildExistsAndIsActive(_hudsonCiEntryPoint.Name, project.Name);

                ThreeStateTreeNode node = new ThreeStateTreeNode(project.Name)
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
