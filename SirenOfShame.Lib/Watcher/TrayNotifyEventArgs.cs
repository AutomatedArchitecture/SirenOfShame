using System.Windows.Forms;

namespace SirenOfShame.Lib.Watcher {
    public class TrayNotifyEventArgs {
        public string Title { get; set; }

        public string TipText { get; set; }

        public ToolTipIcon TipIcon { get; set; }
    }
}