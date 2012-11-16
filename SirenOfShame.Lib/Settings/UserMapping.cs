using System;
using System.Windows.Forms;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class UserMapping
    {
        public string WhenISee { get; set; }
        public string PretendItsActually { get; set; }

        public ListViewItem AsListViewItem()
        {
            var listViewItem = new ListViewItem(WhenISee);
            listViewItem.SubItems.Add(PretendItsActually);
            listViewItem.Tag = this;
            return listViewItem;
        }
    }
}