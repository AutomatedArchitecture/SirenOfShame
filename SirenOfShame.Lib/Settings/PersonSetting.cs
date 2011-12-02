using System;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class PersonSetting
    {
        public string RawName { get; set; }
        public string DisplayName { get; set; }
        public int TotalBuilds { get; set; }
        public int FailedBuilds { get; set; }
    }
}