using System;
using System.IO;

namespace SirenOfShame.Lib.Device.SdCardFileSystem
{
    public class UploadAudioPatternStream : UploadAudioPattern
    {
        private readonly string _name;
        private readonly Stream _stream;

        public UploadAudioPatternStream(string name, Stream stream)
        {
            _name = name;
            _stream = stream;
        }

        public override string Name { get { return _name; } }

        public override int DataLength
        {
            get { return (int)_stream.Length; }
        }

        public override Stream OpenData()
        {
            return _stream;
        }
    }
}
