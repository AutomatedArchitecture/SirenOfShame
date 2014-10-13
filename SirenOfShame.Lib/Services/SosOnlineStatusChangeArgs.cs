using System;

namespace SirenOfShame.Lib.Services
{
    public class SosOnlineStatusChangeArgs
    {
        public string TextStatus { get; set; }
        public Exception Exception { get; set; }
    }
}