using System;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;

namespace BambooServices.ServerConfiguration
{
    public partial class ConfigureBamboo : ConfigureServerBase
    {
        private readonly BambooCIEntryPoint _hudsonCiEntryPoint;
        private readonly CiEntryPointSetting _ciEntryPointSetting;
        private readonly BambooService _service = new BambooService();

        public ConfigureBamboo() { }

        public ConfigureBamboo(SirenOfShameSettings sosSettings, BambooCIEntryPoint hudsonCiEntryPoint, CiEntryPointSetting ciEntryPointSetting)
            : base(sosSettings)
        {
            _hudsonCiEntryPoint = hudsonCiEntryPoint;
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

        private void _connect_Click(object sender, EventArgs e)
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
            MessageBox.Show("Error connecting to server: " + ex.Message);
        }

        private void GetProjectsComplete(BambooBuildDefinition[] buildDefinitions)
        {
            _ciEntryPointSetting.Url = _url.Text;
            _ciEntryPointSetting.UserName = _userName.Text;
            _ciEntryPointSetting.SetPassword(_password.Text);
            Settings.Save();

            _projects.Nodes.Clear();
            foreach (BambooBuildDefinition project in buildDefinitions)
            {
                ThreeStateTreeNode node = new ThreeStateTreeNode(project.Name)
                {
                    Tag = project,
                    State = CheckBoxState.Unchecked
                };
                _projects.Nodes.Add(node);
            }
        }

        private void ProjectsAfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is BambooBuildDefinition)
            {
                var buildDefinition = (BambooBuildDefinition)e.Node.Tag;
                var buildDefSetting = _ciEntryPointSetting.FindAddBuildDefinition(buildDefinition, _hudsonCiEntryPoint.Name);
                buildDefSetting.Active = e.Node.Checked;
                Settings.Save();
            }
            ((ThreeStateTreeNode)e.Node).UpdateStateOfRelatedNodes();
        }
    }
}
