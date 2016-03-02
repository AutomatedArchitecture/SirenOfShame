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
        private List<TfsRestBuildDefinition> _buildDefinitions;
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
                var buildDefinitions = await _service.GetProjects(_url.Text, _userName.Text, _password.Text);
                _ciEntryPointSetting.Url = _url.Text;
                _ciEntryPointSetting.UserName = _userName.Text;
                _ciEntryPointSetting.SetPassword(_password.Text);
                Settings.Save();

                _projects.Nodes.Clear();
                _buildDefinitions = buildDefinitions.OrderBy(i => i.Name).ToList();
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

            foreach (TfsRestBuildDefinition project in _buildDefinitions)
            {
                var shouldBeVisible = string.IsNullOrEmpty(_filter.Text) || project.Name.Contains(_filter.Text, StringComparison.CurrentCultureIgnoreCase);
                if (!shouldBeVisible) continue;
                bool exists = Settings.BuildExistsAndIsActive(_ciEntryPoint.Name, project.Name);

                ThreeStateTreeNode node = new ThreeStateTreeNode(project.Name)
                {
                    Tag = project,
                    State = exists ? CheckBoxState.Checked : CheckBoxState.Unchecked
                };
                _projects.Nodes.Add(node);
            }
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            ReloadProjects();
        }
    }
}
