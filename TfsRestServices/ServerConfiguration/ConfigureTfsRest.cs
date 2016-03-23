using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;

namespace TfsRestServices.ServerConfiguration
{
    public partial class ConfigureTfsRest : ConfigureServerBase
    {
        private readonly TfsRestCiEntryPoint _ciEntryPoint;
        private readonly CiEntryPointSetting _ciEntryPointSetting;
        private readonly TfsRestService _service = new TfsRestService();
        private List<TfsRestProjectCollection> _projectCollections;
        private bool _disableCheckEvents = false;
        public ConfigureTfsRest() { }

        public ConfigureTfsRest(SirenOfShameSettings sosSettings, TfsRestCiEntryPoint ciEntryPoint,
            CiEntryPointSetting ciEntryPointSetting)
            : base(sosSettings)
        {
            _ciEntryPoint = ciEntryPoint;
            _ciEntryPointSetting = ciEntryPointSetting;
            InitializeComponent();
            _url.Text = _ciEntryPointSetting.Url;
            _userName.Text = _ciEntryPointSetting.UserName;
            _password.Text = _ciEntryPointSetting.GetPassword();
            if (!string.IsNullOrEmpty(_url.Text))
            {
                ReloadProjects();
            }
        }

        private async void ReloadProjects()
        {
            try
            {
                _projects.Nodes.Clear();
                _projects.Nodes.Add("Loading...");
                var projectCollections = await _service.GetBuildDefinitionsGrouped(_url.Text, _userName.Text, _password.Text);
                _ciEntryPointSetting.Url = _url.Text;
                _ciEntryPointSetting.UserName = _userName.Text;
                _ciEntryPointSetting.SetPassword(_password.Text);
                Settings.Save();

                _projects.Nodes.Clear();
                _projectCollections = projectCollections.OrderBy(i => i.Name).ToList();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ApplyFilter()
        {
            _projects.Nodes.Clear();

            foreach (var projectCollection in _projectCollections)
            {
                TreeNode projectCollectionNode = new TreeNode(projectCollection.Name);
                var tfsProjects = projectCollection.Projects.OrderBy(i => i.Name);
                foreach (var project in tfsProjects)
                {
                    var projectNode = new TreeNode(project.Name);
                    var myTfsBuildDefinitions = project.BuildDefinitions.OrderBy(i => i.Name);
                    foreach (var buildDefinition in myTfsBuildDefinitions)
                    {
                        var shouldBeVisible = string.IsNullOrEmpty(_filter.Text) || buildDefinition.Name.Contains(_filter.Text, StringComparison.CurrentCultureIgnoreCase);
                        if (!shouldBeVisible) continue;
                        _ciEntryPointSetting.FindAddBuildDefinition(buildDefinition, _ciEntryPoint.Name);
                        bool exists = Settings.BuildExistsAndIsActive(_ciEntryPoint.Name, buildDefinition.Name);

                        ThreeStateTreeNode node = new ThreeStateTreeNode(buildDefinition.Name)
                        {
                            Tag = buildDefinition.Id,
                            State = exists ? CheckBoxState.Checked : CheckBoxState.Unchecked
                        };
                        projectNode.Nodes.Add(node);
                    }
                    AddIfContainsChildren(projectCollectionNode.Nodes, projectNode);
                }
                AddIfContainsChildren(_projects.Nodes, projectCollectionNode);
            }
            RefreshCheckednessOfParentNodes();
        }

        private static void AddIfContainsChildren(TreeNodeCollection nodes, TreeNode child)
        {
            if (child.Nodes.Count == 0) return;
            nodes.Add(child);
            child.Expand();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            ReloadProjects();
        }

        private void ProjectsAfterCheck(object sender, TreeViewEventArgs e)
        {
            var disableCheckEvents = _disableCheckEvents;
            DisableCheckEvents(() =>
            {
                var isBuildDefinition = e.Node.Tag != null;
                if (isBuildDefinition)
                {
                    SetBuildDefinitionActive(e.Node);
                    if (disableCheckEvents) return;
                    RefreshCheckednessOfParentNodes();
                }
                else
                {
                    if (disableCheckEvents) return;
                    SelectAllChildren(e.Node);
                }
            });
        }

        private void SelectAllChildren(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = node.Checked;
                SelectAllChildren(child);
            }
        }

        private void SetBuildDefinitionActive(TreeNode node)
        {
            var buildDefinitionId = (string)node.Tag;
            var buildDefinitionSetting = _ciEntryPointSetting.GetBuildDefinition(buildDefinitionId);
            buildDefinitionSetting.Active = node.Checked;
            Settings.Save();
        }

        private void DisableCheckEvents(Action action)
        {
            var oldDisableCheckEventsValue = _disableCheckEvents;
            _disableCheckEvents = true;
            try
            {
                action();
            }
            finally
            {
                _disableCheckEvents = oldDisableCheckEventsValue;
            }
        }

        private void RefreshCheckednessOfParentNodes()
        {
            DisableCheckEvents(() =>
            {
                foreach (TreeNode node in _projects.Nodes)
                {
                    RefreshCheckednessOfParentNodes(node);
                }
            });
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

        private void _filter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
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
