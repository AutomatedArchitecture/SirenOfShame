using System;

namespace SirenOfShame.Lib.Watcher {
    public class UpdateStatusBarEventArgs {
        public Exception Exception { get; set; }
        public string StatusText { get; set; }
    }
}