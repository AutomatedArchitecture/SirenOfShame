using System;
using System.Windows.Forms;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class Sound
    {
        public string Location { get; set; }
        public string Name { get; set; }

        public ListViewItem AsListViewItem()
        {
            var listViewItem = new ListViewItem(Name)
            {
                Tag = this
            };
            return listViewItem;
        }
    }
}