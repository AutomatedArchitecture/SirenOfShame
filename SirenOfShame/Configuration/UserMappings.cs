using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class UserMappings : FormBase
    {
        private readonly SirenOfShameSettings _settings;

        public UserMappings(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();
            RefreshMappingList();
        }

        private void AddClick(object sender, System.EventArgs e)
        {
            AddMapping addMapping = new AddMapping(_settings);
            addMapping.ShowDialog(this);
            RefreshMappingList();
        }

        private void RefreshMappingList()
        {
            _mappingList.Items.Clear();
            _mappingList.Items.AddRange(_settings.UserMappings.Select(r => r.AsListViewItem()).ToArray());
            ShowHideDeleteButton();
        }

        private void ShowHideDeleteButton()
        {
            _delete.Enabled = _mappingList.SelectedIndices.Count > 0;
        }

        private void MappingListSelectedIndexChanged(object sender, System.EventArgs e)
        {
            ShowHideDeleteButton();
        }

        private void DeleteClick(object sender, System.EventArgs e)
        {
            if (_mappingList.SelectedItems.Count == 0) return;
            ListViewItem lvi = _mappingList.SelectedItems[0];
            var mapping = _settings.UserMappings.First(r => lvi.Tag == r);
            _settings.UserMappings.Remove(mapping);
            RefreshMappingList();
            _settings.Save();
        }
    }
}
