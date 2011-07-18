using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Lib;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;

namespace TfsServices.Configuration
{
    public partial class ConfigureTfs : ConfigureServerBase
    {
        private readonly TfsCiEntryPoint _tfsCiEntryPoint;
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(ConfigureTfs));
        
        public ConfigureTfs(SirenOfShameSettings settings, TfsCiEntryPoint tfsCiEntryPoint)
            : base(settings)
        {
            _tfsCiEntryPoint = tfsCiEntryPoint;
            InitializeComponent();
            _url.Text = settings.FindAddSettings(_tfsCiEntryPoint.Name).Url;
            DataBindAsync();
        }

        private void DataBindAsync()
        {
            if (Settings.FindAddSettings(_tfsCiEntryPoint.Name).Url == null)
            {
                // ToDo: dynamically try to find the url
                return;
            }

            _buildConfigurations.Nodes.Clear();
            _buildConfigurations.Nodes.Add("Contacting server...");

            Thread t = new Thread(DataBind) {IsBackground = true};
            t.Start();
        }

        private void DataBind()
        {
            try
            {
                using (var tfs = new MyTfsServer(Settings.FindAddSettings(_tfsCiEntryPoint.Name).Url))
                {
                    IList<TreeNode> projectCollectionNodes = new List<TreeNode>();

                    foreach (var teamProjectCollection in tfs.ProjectCollections)
                    {
                        TreeNode projectCollectionNode = new TreeNode(teamProjectCollection.Name);
                        foreach (var project in teamProjectCollection.Projects)
                        {
                            var projectNode = projectCollectionNode.Nodes.Add(project.Name);
                            foreach (var buildDefinition in project.BuildDefinitions)
                            {
                                var buildDefinitionSetting = Settings.FindAddBuildDefinition(buildDefinition, _tfsCiEntryPoint.Name);
                                projectNode.Nodes.Add(buildDefinition.GetAsNode(buildDefinitionSetting.Active));
                            }
                        }
                        projectCollectionNodes.Add(projectCollectionNode);
                    }

                    Invoke(() => _buildConfigurations.Nodes.Clear());
                    Invoke(() => _buildConfigurations.Nodes.AddRange(projectCollectionNodes.ToArray()));

                }
                Settings.Save();
            }
            catch (Exception ex)
            {
                Invoke(() => _buildConfigurations.Nodes.Clear());
                _log.Error("Failed to connect to server", ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void GoClick(object sender, EventArgs e)
        {
            Settings.FindAddSettings(_tfsCiEntryPoint.Name).Url = _url.Text;
            Settings.Save();
            DataBindAsync();
        }

        private void BuildConfigurationsAfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag == null) return;
            var buildDefinitionId = (string)e.Node.Tag;
            Settings.GetBuildDefinition(buildDefinitionId).Active = e.Node.Checked;
            Settings.Save();
        }

        private void UrlTextChanged(object sender, EventArgs e)
        {
            Settings.FindAddSettings(_tfsCiEntryPoint.Name).Url = _url.Text;
            Settings.Save();
        }
    }
}
