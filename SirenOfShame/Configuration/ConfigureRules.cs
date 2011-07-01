using System;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class ConfigureRules : Form
    {
        private readonly SirenOfShameSettings _settings;

        public ConfigureRules(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();
        }

        private void NewRuleClick(object sender, EventArgs e)
        {
            AddRule addRule = new AddRule(_settings);
            addRule.ShowDialog();
            DataBind();
        }

        private void ConfigureRulesLoad(object sender, EventArgs e)
        {
            DataBind();
        }

        private void DataBind()
        {
            _rulesList.Items.Clear();
            _rulesList.Items.AddRange(_settings.Rules.Select(r => r.AsListViewItem()).ToArray());
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            if (_rulesList.SelectedItems.Count == 0) return;
            ListViewItem i = _rulesList.SelectedItems[0];
            var rule = _settings.Rules.First(r => i.Tag == r);
            _settings.Rules.Remove(rule);
            DataBind();
            _settings.Save();
        }

        private void RulesListDoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = _rulesList.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (item == null) return;
            Rule rule = (Rule)item.Tag;
            AddRule addRule = new AddRule(_settings, rule);
            addRule.ShowDialog();
            DataBind();
        }

        private void ResetClick(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete all your rules?", "The obligatory 'are you sure?' dialog", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;
            _settings.ResetRules();
            DataBind();
        }
    }
}