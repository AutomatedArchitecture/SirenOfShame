using System;
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
            TypeChanged();
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

        private void Add_Click(object sender, EventArgs e)
        {
            _service.GetProject(_travisUrl.Text, _authToken.Text, _ownerName.Text, _projectName.Text, GetProjectComplete, GetProjectError);
        }

        private void GetProjectError(Exception ex)
        {
            _log.Error(ex);
            MessageBox.Show("Error connecting to server: " + ex.Message);
        }

        private void GetProjectComplete(TravisCiBuildDefinition buildDefinition)
        {
            _ciEntryPointSetting.Url = _travisUrl.Text;
            if (!string.IsNullOrEmpty(_authToken.Text))
            {
                _ciEntryPointSetting.SetPassword(_authToken.Text);
            }
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

        private void _typePublic_CheckedChanged(object sender, EventArgs e)
        {
            TypeChanged();
        }

        private void _typePrivate_CheckedChanged(object sender, EventArgs e)
        {
            TypeChanged();
        }

        private void _typeEnterprise_CheckedChanged(object sender, EventArgs e)
        {
            TypeChanged();
        }

        private void TypeChanged()
        {
            var repoType = GetRepoType();
            _travisUrl.Text = GetUrlFromType(repoType);
            _travisUrl.Enabled = repoType == TravisRepoType.Enterprise;
            _authToken.Enabled = repoType != TravisRepoType.Public;
            authTokenLabel.Enabled = repoType != TravisRepoType.Public;
            _generateAuthToken.Enabled = repoType != TravisRepoType.Public;
            if (repoType == TravisRepoType.Public)
            {
                _authToken.Text = "";
            }
        }

        private TravisRepoType GetRepoType()
        {
            if (_typePublic.Checked) return TravisRepoType.Public;
            if (_typePrivate.Checked) return TravisRepoType.Private;
            return TravisRepoType.Enterprise;
        }

        private string GetUrlFromType(TravisRepoType repoType)
        {
            if (repoType == TravisRepoType.Public) return "https://api.travis-ci.org/";
            if (repoType == TravisRepoType.Private) return "https://api.travis-ci.com/";
            return "";
        }
    }

    public enum TravisRepoType
    {
        Public = 1,
        Private = 2,
        Enterprise = 3
    }
}
