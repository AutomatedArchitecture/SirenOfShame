using System;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Watcher
{
    public class NewNewsItemEventArgs
    {
        public DateTime EventDate { get; set; }
        public PersonSetting Person { get; set; }
        public string Title { get; set; }
    }
}