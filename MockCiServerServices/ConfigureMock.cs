using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;

namespace MockCiServerServices
{
    public partial class ConfigureMock : ConfigureServerBase
    {
        private readonly CiEntryPointSetting _ciEntryPointSetting;
        private readonly MockCiEntryPoint _mockCiEntryPoint;

        public ConfigureMock()
        {
            InitializeComponent();
        }

        public ConfigureMock(SirenOfShameSettings settings, MockCiEntryPoint mockCiEntryPoint, CiEntryPointSetting ciEntryPointSetting)
            : base(settings)
        {
            InitializeComponent();
            _mockCiEntryPoint = mockCiEntryPoint;
            _ciEntryPointSetting = ciEntryPointSetting;
        }

        private void ConfigureMock_Load(object sender, System.EventArgs e)
        {
            _projects.Nodes.Clear();
            _projects.Nodes.Add(new ThreeStateTreeNode("Project 1") { Tag = new MockBuildDefinition("Project1", "Project 1") });
            _projects.Nodes.Add(new ThreeStateTreeNode("Project 2") { Tag = new MockBuildDefinition("Project2", "Project 2") });
            _projects.Nodes.Add(new ThreeStateTreeNode("Project 3") { Tag = new MockBuildDefinition("Project3", "Project 3") });
        }

        private void _projects_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            _ciEntryPointSetting.Url = "http://mock";
            _ciEntryPointSetting.UserName = "Mock";
            _ciEntryPointSetting.SetPassword("Password");
            Settings.Save();

            if (e.Node.Tag is MyBuildDefinition)
            {
                var buildDefinition = (MyBuildDefinition)e.Node.Tag;
                var buildDefSetting = _ciEntryPointSetting.FindAddBuildDefinition(buildDefinition, _mockCiEntryPoint.Name);
                buildDefSetting.Active = e.Node.Checked;
                Settings.Save();
            }
            ((ThreeStateTreeNode)e.Node).UpdateStateOfRelatedNodes();
        }
    }
}
