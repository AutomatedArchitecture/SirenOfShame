using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class ViewBuilds : UserControl
    {
        public ViewBuilds()
        {
            InitializeComponent();
        }

        public void RefreshListViewWithBuildStatus(RefreshStatusEventArgs args)
        {
            var buildStatusListViewItems = args.BuildStatusListViewItems.ToList();
            bool numberOfBuildsChanged = ViewBuildsCount != 0 && ViewBuildsCount != buildStatusListViewItems.Count();

            var listViewItemsJoinedStatus = (
                from listViewItem in GetViewBuilds()
                join buildStatus in buildStatusListViewItems on listViewItem.BuildName equals buildStatus.Name
                select new { listViewItem, buildStatus }
                ).ToList();

            var someBuildNameChanged = listViewItemsJoinedStatus.Count != buildStatusListViewItems.Count;
            if (numberOfBuildsChanged || someBuildNameChanged)
            {
                flowLayoutPanel1.Controls.Clear();
            }
            if (ViewBuildsCount == 0)
            {
                Control[] listViewItems = buildStatusListViewItems.Select(i => new ViewBuildSmall(i)).ToArray();
                flowLayoutPanel1.Controls.AddRange(listViewItems);
            }
            else
            {
                listViewItemsJoinedStatus.ToList().ForEach(i => i.listViewItem.UpdateListItem(i.buildStatus));
            }

            // todo: implement sorting
            //Sort();
        }

        private int ViewBuildsCount
        {
            get { return flowLayoutPanel1.Controls.Count; }
        }

        private IEnumerable<ViewBuildSmall> GetViewBuilds()
        {
            return flowLayoutPanel1.Controls.Cast<ViewBuildSmall>();
        }
    }
}
