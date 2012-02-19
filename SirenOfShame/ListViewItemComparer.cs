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

        public int Compare(object x, object y)
        {
            var xListViewItem = x as ListViewItem;
            var yListViewItem = y as ListViewItem;
            if (xListViewItem == null || yListViewItem == null) return 0;
            var xText = xListViewItem.SubItems[_sortColumn].Text;
            var yText = yListViewItem.SubItems[_sortColumn].Text;
            if (_sortOrder == SortOrder.Descending)
                return String.CompareOrdinal(yText, xText);
            return String.CompareOrdinal(xText, yText);
        }
    }
}