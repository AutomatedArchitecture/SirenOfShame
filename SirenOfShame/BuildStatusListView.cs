using System;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public class BuildStatusListView : ListView
    {
        public void RefreshListViewWithBuildStatus(RefreshStatusEventArgs args)
        {
            var buildStatusListViewItems = args.BuildStatusListViewItems.ToList();
            bool numberOfBuildsChanged = Items.Count != 0 && Items.Count != buildStatusListViewItems.Count();

            var listViewItemsJoinedStatus = (
                from listViewItem in Items.Cast<ListViewItem>()
                join buildStatus in buildStatusListViewItems on listViewItem.Text equals buildStatus.Name
                select new { listViewItem, buildStatus }
                ).ToList();

            var someBuildNameChanged = listViewItemsJoinedStatus.Count != buildStatusListViewItems.Count;

            if (numberOfBuildsChanged || someBuildNameChanged)
            {
                Items.Clear();
            }
            if (Items.Count == 0)
            {
                var listViewItems = buildStatusListViewItems.Select(MainForm.AsListViewItem).ToArray();
                Items.AddRange(listViewItems);
            }
            else
            {
                listViewItemsJoinedStatus.ToList().ForEach(i => UpdateListItem(i.listViewItem, i.buildStatus));
            }

            Sort();
        }

        public void SetSortColumn(int sortColumn, SortOrder sortOrder)
        {
            ListViewItemSorter = new ListViewItemComparer(sortColumn, sortOrder);
            Sort();
        }

        private void UpdateListItem(ListViewItem listViewItem, BuildStatusListViewItem buildStatus)
        {
            listViewItem.ImageIndex = buildStatus.ImageIndex;
            UpdateSubItem(listViewItem, "ID", buildStatus.BuildId);
            UpdateSubItem(listViewItem, "StartTime", buildStatus.StartTime, buildStatus.StartTimeTicks);
            UpdateSubItem(listViewItem, "Duration", buildStatus.Duration);
            UpdateSubItem(listViewItem, "RequestedBy", buildStatus.RequestedByDisplayName);
            UpdateSubItem(listViewItem, "Comment", buildStatus.Comment);
        }

        private static void UpdateSubItem(ListViewItem lvi, string name, string value, object tag = null)
        {
            var subItem = lvi.SubItems.Cast<ListViewItem.ListViewSubItem>().FirstOrDefault(i => i.Name == name);
            if (subItem == null) throw new Exception("Unable to find list view sub item" + name);
            // ReSharper disable RedundantCheckBeforeAssignment
            if (value != subItem.Text)
            {
                // ReSharper restore RedundantCheckBeforeAssignment
                subItem.Text = value;
                subItem.Tag = tag;
            }
        }
    }
}
