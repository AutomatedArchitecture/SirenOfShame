using System;
using System.Collections;
using System.Windows.Forms;

namespace SirenOfShame
{
    public class ListViewItemComparer : IComparer
    {
        private readonly int _sortColumn;
        private readonly SortOrder _sortOrder;

        public ListViewItemComparer(int sortColumn, SortOrder sortOrder)
        {
            _sortColumn = sortColumn;
            _sortOrder = sortOrder;
        }

        public int CompareNumeric(long x, long y, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Descending)
            {
                return CompareNumeric(y, x, SortOrder.Ascending);
            }
            
            if (x > y) return 1;
            if (x == y) return 0;
            return -1;
        }
        
        public int Compare(object x, object y)
        {
            var xListViewItem = x as ListViewItem;
            var yListViewItem = y as ListViewItem;
            if (xListViewItem == null || yListViewItem == null) return 0;

            ListViewItem.ListViewSubItem xlistViewSubItem = xListViewItem.SubItems[_sortColumn];
            ListViewItem.ListViewSubItem ylistViewSubItem = yListViewItem.SubItems[_sortColumn];

            object yTag = ylistViewSubItem.Tag;
            object xTag = xlistViewSubItem.Tag;
            if (xTag != null || yTag != null)
            {
                if (xTag is long || yTag is long)
                {
                    var xLong = xTag == null ? 0 : (long)xTag;
                    var yLong = yTag == null ? 0 : (long)yTag;
                    return CompareNumeric(xLong, yLong, _sortOrder);
                }
            }
            
            return SortByText(ylistViewSubItem, xlistViewSubItem);
        }

        private int SortByText(ListViewItem.ListViewSubItem ylistViewSubItem, ListViewItem.ListViewSubItem xlistViewSubItem)
        {
            var xText = xlistViewSubItem.Text;
            var yText = ylistViewSubItem.Text;
            if (_sortOrder == SortOrder.Descending)
                return String.CompareOrdinal(yText, xText);
            return String.CompareOrdinal(xText, yText);
        }
    }
}