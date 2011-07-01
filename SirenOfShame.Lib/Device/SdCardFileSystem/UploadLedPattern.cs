using System.Collections.Generic;

namespace SirenOfShame.Lib.Device.SdCardFileSystem
{
    public class UploadLedPattern
    {
        private readonly string _name;
        private readonly byte[] _pattern;

        public string Name { get { return _name; } }
        public byte[] Pattern { get { return _pattern; } }

        public UploadLedPattern(string name, byte[] pattern)
        {
            _name = name;
            _pattern = pattern;
        }
    }
}
