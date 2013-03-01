using System;
using System.Collections.Generic;

namespace SirenOfShame.Lib.Device.SdCardFileSystem
{
    public class UploadLedPattern
    {
        private readonly string _name;
        private readonly byte[] _pattern;
        private readonly string _fileName;

        public string Name { get { return _name; } }
        public string FileName { get { return _fileName; } }
        public byte[] Pattern { get { return _pattern; } }

        public UploadLedPattern(string name, byte[] pattern, string fileName)
        {
            _name = name;
            _pattern = pattern;
            _fileName = fileName;
            
            while (_pattern.Length < SirenOfShameDevice.LedPatternBufferSize * 2)
            {
                var newPattern = new byte[_pattern.Length * 2];
                Array.Copy(_pattern, 0, newPattern, 0, _pattern.Length);
                Array.Copy(_pattern, 0, newPattern, _pattern.Length, _pattern.Length);
                _pattern = newPattern;
            }
        }
    }
}
