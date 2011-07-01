using System.IO;

namespace SirenOfShame.Lib.Device.SdCardFileSystem
{
    public abstract class UploadAudioPattern
    {
        public abstract string Name { get; }
        public abstract int DataLength { get; }

        public abstract Stream OpenData();
    }
}