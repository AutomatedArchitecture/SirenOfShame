using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Lib;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Helpers;

namespace TfsServices.Configuration
{
    public partial class ConfigureTfs : ConfigureServerBase
    {
        private List<MyTfsProjectCollection> _projectCollections = new List<MyTfsProjectCollection>();
        private readonly TfsCiEntryPoint _tfsCiEntryPoint;
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(ConfigureTfs));
        private readonly CiEntryPointSetting _ciEntryPointSetting;
        private bool _disableCheckEvents = false;

        public ConfigureTfs(SirenOfShameSettings settings, TfsCiEntryPoint tfsCiEntryPoint, CiEntryPointSetting ciEntryPointSetting)
            : base(settings)
        {
            _tfsCiEntryPoint = tfsCiEntryPoint;
            InitializeComponent();
            _ciEntryPointSetting = ciEntryPointSetting;
            _url.Text = _ciEntryPointSetting.Url;
            _applyBuildQuality.Checked = _ciEntryPointSetting.ApplyBuildQuality;
            someoneElse.Checked = !string.IsNullOrEmpty(_ciEntryPointSetting.UserName);
            username.Text = _ciEntryPointSetting.UserName;
            password.Text = _ciEntryPointSetting.GetPassword();
            DataBindAsync();
            RefreshCredentials();
        }

        private void DataBindAsync()
        {
            if (_ciEntryPointSetting.Url == null)
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
                using (var tfs = new MyTfsServer(_ciEntryPointSetting))
                {
                    _projectCollections = tfs.ProjectCollections.OrderBy(i => i.Name).ToList();
                    Invoke(ApplyFilter);
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
            if (MyTfsServer.CheckIfIsHostedTfs(_url.Text) && windowsCredentials.Checked)
            {
                MessageBox.Show("You will need to 'Be Someone Else' when connecting to hosted TFS");
                someoneElse.Checked = true;
                return;
            }

            _ciEntryPointSetting.Url = _url.Text;
            _ciEntryPointSetting.UserName = windowsCredentials.Checked ? null : username.Text;
            if (someoneElse.Checked)
            {
                _ciEntryPointSetting.SetPassword(password.Text);
            }
            Settings.Save();
            DataBindAsync();
        }

        private void BuildConfigurationsAfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_disableCheckEvents) return;
            var isBuildDefinition = e.Node.Tag != null;
            if (isBuildDefinition)
            {
                SetBuildDefinitionActive(e.Node);
                RefreshCheckednessOfParentNodes();
            }
            else
            {
                SelectAllChildren(e.Node);
            }
        }

        private void SelectAllChildren(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = node.Checked;
            }
        }

        private void SetBuildDefinitionActive(TreeNode node)
        {
            var buildDefinitionId = (string) node.Tag;
            _ciEntryPointSetting.GetBuildDefinition(buildDefinitionId).Active = node.Checked;
            Settings.Save();
        }

        private void UrlTextChanged(object sender, EventArgs e)
        {
            _ciEntryPointSetting.Url = _url.Text;
            Settings.Save();
        }

        private void RefreshCredentials()
        {
            username.Visible = someoneElse.Checked;
            password.Visible = someoneElse.Checked;
            usernameLabel.Visible = someoneElse.Checked;
            passwordLabel.Visible = someoneElse.Checked;
            passwordHintLabel.Visible = someoneElse.Checked;
        }
        
        private void SomeoneElseCheckedChanged(object sender, EventArgs e)
        {
            RefreshCredentials();
        }

        private void WindowsCredentialsCheckedChanged(object sender, EventArgs e)
        {
            RefreshCredentials();
        }

        private void _applyBuildQuality_CheckedChanged(object sender, EventArgs e)
        {
            _ciEntryPointSetting.ApplyBuildQuality = _applyBuildQuality.Checked;
            Settings.Save();
        }

        private void Filter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void RefreshCheckednessOfParentNodes()
        {
            _disableCheckEvents = true;
            try
            {
                foreach (TreeNode node in _buildConfigurations.Nodes)
                {
                    RefreshCheckednessOfParentNodes(node);
                }
            }
            finally
            {
                _disableCheckEvents = false;
            }
        }
        
        private void RefreshCheckednessOfParentNodes(TreeNode node)
        {
            var childNodes = node.Nodes.Cast<TreeNode>().ToList();
            foreach (TreeNode child in childNodes)
            {
                RefreshCheckednessOfParentNodes(child);
            }
            if (childNodes.Any())
            {
                node.Checked = childNodes.All(i => i.Checked);
            }
        }
        
        private void ApplyFilter()
        {
            _buildConfigurations.Nodes.Clear();

            foreach (var teamProjectCollection in _projectCollections)
            {
                TreeNode projectCollectionNode = new TreeNode(teamProjectCollection.Name);
                var myTfsProjects = teamProjectCollection.Projects.OrderBy(i => i.Name);
                foreach (var project in myTfsProjects)
                {
                    var projectNode = new TreeNode(project.Name);
                    var myTfsBuildDefinitions = project.BuildDefinitions.OrderBy(i => i.Name);
                    foreach (var buildDefinition in myTfsBuildDefinitions)
                    {
                        var shouldBeVisible = string.IsNullOrEmpty(_filter.Text) || buildDefinition.Name.Contains(_filter.Text, StringComparison.CurrentCultureIgnoreCase);
                        if (!shouldBeVisible) continue;
                        var buildDefinitionSetting = _ciEntryPointSetting.FindAddBuildDefinition(buildDefinition, _tfsCiEntryPoint.Name);
                        projectNode.Nodes.Add(buildDefinition.GetAsNode(buildDefinitionSetting.Active));
                    }
                    AddIfContainsChildren(projectCollectionNode.Nodes, projectNode);
                }
                AddIfContainsChildren(_buildConfigurations.Nodes, projectCollectionNode);
            }

            RefreshCheckednessOfParentNodes();
        }

        private static void AddIfContainsChildren(TreeNodeCollection nodes, TreeNode child)
        {
            if (child.Nodes.Count == 0) return;
            nodes.Add(child);
            child.Expand();
        }
    }
}
