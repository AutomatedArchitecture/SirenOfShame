using System;
using System.Drawing;
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
            if (Items.Count != 0 && Items.Count != buildStatusListViewItems.Count())
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
                var listViewItemsJoinedStatus = from listViewItem in Items.Cast<ListViewItem>()
                                                join buildStatus in buildStatusListViewItems on listViewItem.Text equals
                                                    buildStatus.Name
                                                select new { listViewItem, buildStatus };
                listViewItemsJoinedStatus.ToList().ForEach(i => UpdateListItem(i.listViewItem, i.buildStatus));
            }
        }

        private void UpdateListItem(ListViewItem listViewItem, BuildStatusListViewItem buildStatus)
        {
            listViewItem.ImageIndex = buildStatus.ImageIndex;
            UpdateSubItem(listViewItem, "StartTime", buildStatus.StartTime);
            UpdateSubItem(listViewItem, "Duration", buildStatus.Duration);
            UpdateSubItem(listViewItem, "RequestedBy", buildStatus.RequestedBy);
            UpdateSubItem(listViewItem, "Comment", buildStatus.Comment);
        }

        private static void UpdateSubItem(ListViewItem lvi, string name, string value)
        {
            var subItem = lvi.SubItems.Cast<ListViewItem.ListViewSubItem>().FirstOrDefault(i => i.Name == name);
            if (subItem == null) throw new Exception("Unable to find list view sub item" + name);
            // ReSharper disable RedundantCheckBeforeAssignment
            if (value != subItem.Text)
                // ReSharper restore RedundantCheckBeforeAssignment
                subItem.Text = value;
        }
    }
}
