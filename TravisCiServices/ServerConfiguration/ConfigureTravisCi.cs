using System;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using log4net;

namespace TravisCiServices.ServerConfiguration
{
    public partial class ConfigureTravisCi : ConfigureServerBase
    {
        private readonly TravisCiEntryPoint _travisCiEntryPoint;
        private readonly CiEntryPointSetting _ciEntryPointSetting;
        private readonly TravisCiService _service = new TravisCiService();
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(ConfigureTravisCi));

        public ConfigureTravisCi() { }

        public ConfigureTravisCi(SirenOfShameSettings sosSettings, TravisCiEntryPoint travisCiEntryPoint, CiEntryPointSetting ciEntryPointSetting)
            : base(sosSettings)
        {
            _travisCiEntryPoint = travisCiEntryPoint;
            _ciEntryPointSetting = ciEntryPointSetting;
            InitializeComponent();
            LoadProjectList();
        }

        private void LoadProjectList()
        {
            _ciEntryPointSetting.BuildDefinitionSettings.ForEach(buildDefinition =>
            {
                var travisCiBuildDefinition = TravisCiBuildDefinition.FromIdString(buildDefinition.Id);
                ThreeStateTreeNode node = new ThreeStateTreeNode(travisCiBuildDefinition.OwnerName + "/" + travisCiBuildDefinition.ProjectName)
                {
                    Tag = buildDefinition,
                    State = CheckBoxState.Checked
                };
                _projects.Nodes.Add(node);
            });
        }

        private void _add_Click(object sender, EventArgs e)
        {
            _service.GetProject(_ownerName.Text, _projectName.Text, GetProjectComplete, GetProjectError);
        }

        private void GetProjectError(Exception ex)
        {
            _log.Error(ex);
            MessageBox.Show("Error connecting to server: " + ex.Message);
        }

        private void GetProjectComplete(TravisCiBuildDefinition buildDefinition)
        {
            _ciEntryPointSetting.Url = "https://api.travis-ci.org/";
            Settings.Save();

            bool exists = Settings.BuildExistsAndIsActive(_travisCiEntryPoint.Name, buildDefinition.Id);

            ThreeStateTreeNode node = new ThreeStateTreeNode(buildDefinition.OwnerName + "/" + buildDefinition.ProjectName)
            {
                Tag = buildDefinition,
                State = exists ? CheckBoxState.Checked : CheckBoxState.Unchecked
            };
            _projects.Nodes.Add(node);
        }

        private void ProjectsAfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is TravisCiBuildDefinition)
            {
                var buildDefinition = (TravisCiBuildDefinition)e.Node.Tag;
                var buildDefSetting = _ciEntryPointSetting.FindAddBuildDefinition(buildDefinition, _travisCiEntryPoint.Name);
                buildDefSetting.Active = e.Node.Checked;
                Settings.Save();
            }
            ((ThreeStateTreeNode)e.Node).UpdateStateOfRelatedNodes();
        }
    }
}
