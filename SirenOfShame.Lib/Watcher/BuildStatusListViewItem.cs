using System;
using System.Windows.Forms;

namespace SirenOfShame.Lib.Watcher
{
    public class BuildStatusListViewItem
    {
        public int ImageIndex { get; set; }
        public string Comment { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public string RequestedBy { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}