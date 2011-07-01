using System;
using SirenOfShame.Lib.Device;

namespace SirenOfShame.Lib.Watcher {
    public class SetAudioEventArgs {
        public AudioPattern AudioPattern { get; set; }
        public int? Duration { get; set; }

        public TimeSpan? TimeSpan
        {
            get { return Duration == null ? (TimeSpan?)null : new TimeSpan(0, 0, 0, Duration.Value); }
        }
    }
}