using System.IO;

namespace SoxLib.Helpers
{
    public class DeleteFileStream : Stream
    {
        private readonly FileStream _fileStream;
        private readonly string _fileName;

        public DeleteFileStream(string fileName)
        {
            _fileName = fileName;
            _fileStream = File.OpenRead(fileName);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _fileStream.Dispose();
            File.Delete(_fileName);
        }

        public override void Flush()
        {
            _fileStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _fileStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _fileStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _fileStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _fileStream.Write(buffer, offset, count);
        }

        public override bool CanRead
        {
            get { return _fileStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _fileStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _fileStream.CanWrite; }
        }

        public override long Length
        {
            get { return _fileStream.Length; }
        }

        public override long Position
        {
            get { return _fileStream.Position; }
            set { _fileStream.Position = value; }
        }
    }
}
