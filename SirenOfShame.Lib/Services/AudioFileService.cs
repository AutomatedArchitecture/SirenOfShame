using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Security.Cryptography;
using log4net;
using SirenOfShame.Lib.Device;
using SoxLib;
using SoxLib.Helpers;
using FileInfo = SoxLib.FileInfo;

namespace SirenOfShame.Lib.Services
{
    [Export(typeof(AudioFileService))]
    public class AudioFileService
    {
        private readonly Sox _sox;
        private readonly ILog _log = MyLogManager.GetLogger(typeof (AudioFileService));
        private const int SAMPLING_RATE = SirenOfShameDevice.AudioSampleRate;

#if DEBUG
                private const string SOX_DIR = @"..\..\libs\sox-14.3.2\";
#else
                private const string SOX_DIR = @"Sox\";
#endif

        public AudioFileService()
        {
            _sox = new Sox
            {
                SoxDirectory = SOX_DIR
            };
        }

        public string ConvertToWav(string sourceFileName, string destinationFileName, bool highQuality = false)
        {
            _log.Debug(string.Format("Atempting to convert '{0}' to '{1}'", sourceFileName, destinationFileName));
            FileInfo outputFormat = new FileInfo
            {
                FileType = FileType.Wav,
                Channels = 1,
                SampleSizeInBits = 8,
                SamplingRate = highQuality ? SAMPLING_RATE * 2 : SAMPLING_RATE,
                EncodingType = EncodingType.UnsignedInteger
            };

            var fileNameExt = Path.GetExtension(sourceFileName);
            ConvertOptions convertOptions = new ConvertOptions
            {
                InputFileInfo = new FileInfo
                {
                    FileType = Sox.GetFileTypeFromExtension(fileNameExt)
                },
                OutputFileInfo = outputFormat
            };
            using (Stream input = File.OpenRead(sourceFileName))
            {
                string resultFileName = string.IsNullOrEmpty(destinationFileName) ? Path.GetTempFileName() : destinationFileName;
                using (var tempConvertedFileStream = _sox.Convert(input, convertOptions))
                {
                    tempConvertedFileStream.WriteToFile(resultFileName);
                };
                return resultFileName;
            }
        }

        public byte[] ConvertToWav(byte[] data)
        {
            const Int16 bitsPerSample = 8;
            const Int16 channels = 1;
            MemoryStream stream = new MemoryStream();
            using (BinaryWriter bw = new BinaryWriter(stream))
            {
                bw.Write(new[] { 'R', 'I', 'F', 'F' });
                bw.Write(data.Length + 44);
                bw.Write(new[] { 'W', 'A', 'V', 'E', 'f', 'm', 't', ' ' });
                bw.Write(16);
                bw.Write((short)1);
                bw.Write(channels);
                bw.Write(SAMPLING_RATE);
                bw.Write(SAMPLING_RATE * ((bitsPerSample * channels) / 8));
                bw.Write((short)((bitsPerSample * channels) / 8));
                bw.Write(bitsPerSample);
                bw.Write(new[] { 'd', 'a', 't', 'a' });
                bw.Write(data.Length);
                bw.Write(data, 0, data.Length);
            }
            return stream.ToArray();
        }

        public Stream Convert(string fileName)
        {
            FileInfo rawFileFormat = new FileInfo
            {
                FileType = FileType.RawUnsignedInteger8,
                Channels = 1,
                SampleSizeInBits = 8,
                SamplingRate = SAMPLING_RATE,
                EncodingType = EncodingType.UnsignedInteger
            };

            var fileNameExt = Path.GetExtension(fileName);
            ConvertOptions convertOptions = new ConvertOptions
            {
                InputFileInfo = new FileInfo
                {
                    FileType = Sox.GetFileTypeFromExtension(fileNameExt)
                },
                OutputFileInfo = rawFileFormat
            };
            using (Stream input = File.OpenRead(fileName))
            {
                return _sox.Convert(input, convertOptions);
            }
        }

        public TimeSpan GetLength(string fileName)
        {
            var fi = new System.IO.FileInfo(fileName);
            return TimeSpan.FromSeconds((double)fi.Length / SAMPLING_RATE);
        }
    }
}
