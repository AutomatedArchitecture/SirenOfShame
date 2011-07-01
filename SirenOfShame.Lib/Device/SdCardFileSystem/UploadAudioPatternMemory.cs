using System.IO;

namespace SirenOfShame.Lib.Device.SdCardFileSystem
{
    public class UploadAudioPatternMemory : UploadAudioPattern
    {
        private readonly string _name;
        private readonly byte[] _data;

        public byte[] Data { get { return _data; } }
        public override string Name { get { return _name; } }

        public UploadAudioPatternMemory(string name, byte[] data)
        {
            _name = name;
            _data = data;
        }

        public override int DataLength
        {
            get { return Data.Length; }
        }

        public override Stream OpenData()
        {
            return new MemoryStream(Data, false);
        }
    }
}