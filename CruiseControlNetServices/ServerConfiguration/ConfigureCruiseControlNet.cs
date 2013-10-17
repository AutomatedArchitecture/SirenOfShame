using System;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using log4net;

namespace CruiseControlNetServices.ServerConfiguration
{
    public partial class ConfigureCruiseControlNet : ConfigureServerBase
    {
        private readonly CruiseControlNetCIEntryPoint _cruiseControlNetCiEntryPoint;
        private readonly CiEntryPointSetting _ciEntryPointSetting;
        private readonly CruiseControlNetService _service = new CruiseControlNetService();
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(ConfigureCruiseControlNet));

        public ConfigureCruiseControlNet() { }

        public ConfigureCruiseControlNet(SirenOfShameSettings sosSettings, CruiseControlNetCIEntryPoint cruiseControlNetCiEntryPoint, CiEntryPointSetting ciEntryPointSetting)
            : base(sosSettings)
        {
            _cruiseControlNetCiEntryPoint = cruiseControlNetCiEntryPoint;
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
            _log.Error(ex);
            MessageBox.Show("Error connecting to server: " + ex.Message);
        }

        private void GetProjectsComplete(CruiseControlNetBuildDefinition[] buildDefinitions)
        {
            _ciEntryPointSetting.Url = _url.Text;
            _ciEntryPointSetting.UserName = _userName.Text;
            _ciEntryPointSetting.SetPassword(_password.Text);
            Settings.Save();

            _projects.Nodes.Clear();
            var cruiseControlNetBuildDefinitions = buildDefinitions.OrderBy(i => i.Name);
            foreach (CruiseControlNetBuildDefinition project in cruiseControlNetBuildDefinitions)
            {
                bool exists = Settings.BuildExistsAndIsActive(_cruiseControlNetCiEntryPoint.Name, project.Name);

                ThreeStateTreeNode node = new ThreeStateTreeNode(project.Name)
                {
                    Tag = project,
                    State = exists ? CheckBoxState.Checked : CheckBoxState.Unchecked
                };
                _projects.Nodes.Add(node);
            }
        }

        private void ProjectsAfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is CruiseControlNetBuildDefinition)
            {
                var buildDefinition = (CruiseControlNetBuildDefinition)e.Node.Tag;
                var buildDefSetting = _ciEntryPointSetting.FindAddBuildDefinition(buildDefinition, _cruiseControlNetCiEntryPoint.Name);
                buildDefSetting.Active = e.Node.Checked;
                Settings.Save();
            }
            ((ThreeStateTreeNode)e.Node).UpdateStateOfRelatedNodes();
        }
    }
}
