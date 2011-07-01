using System;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class CiEntryPointSettings
    {
        public string Url { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
