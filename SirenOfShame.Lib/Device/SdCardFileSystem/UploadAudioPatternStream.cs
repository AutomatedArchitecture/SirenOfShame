using System;
using System.IO;

namespace SirenOfShame.Lib.Device.SdCardFileSystem
{
    public class UploadAudioPatternStream : UploadAudioPattern
    {
        private readonly string _name;
        private readonly string _fileName;

        public UploadAudioPatternStream(string name, string fileName)
        {
            _name = name;
            _fileName = fileName;
        }

        public override string Name { get { return _name; } }

        public override int DataLength
        {
            get
            {
                // todo: memoize
                using (var stream = OpenData())
                {
                    return (int) stream.Length;
                }
            }
        }

        public override Stream OpenData()
        {
            return File.OpenRead(_fileName);
        }
    }
}
