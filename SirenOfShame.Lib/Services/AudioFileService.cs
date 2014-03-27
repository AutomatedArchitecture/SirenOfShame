using System;
using System.ComponentModel.Composition;
using System.IO;
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
        private const int _samplingRate = SirenOfShameDevice.AudioSampleRate;

        public AudioFileService()
        {
            _sox = new Sox
            {
                SoxDirectory = @"..\..\libs\sox-14.3.2\" // todo: figure this out
            };
        }

        public string ConvertToWav(string fileName)
        {
            FileInfo outputFormat = new FileInfo
            {
                FileType = FileType.Wav,
                Channels = 1,
                SampleSizeInBits = 8,
                SamplingRate = _samplingRate,
                EncodingType = EncodingType.UnsignedInteger
            };

            var fileNameExt = Path.GetExtension(fileName);
            ConvertOptions convertOptions = new ConvertOptions
            {
                InputFileInfo = new FileInfo
                {
                    FileType = Sox.GetFileTypeFromExtension(fileNameExt)
                },
                OutputFileInfo = outputFormat
            };
            using (Stream input = File.OpenRead(fileName))
            {
                string resultFileName = Path.GetTempFileName();
                _sox.Convert(input, convertOptions).WriteToFile(resultFileName);
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
                bw.Write(_samplingRate);
                bw.Write(_samplingRate * ((bitsPerSample * channels) / 8));
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
                SamplingRate = _samplingRate,
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
            return TimeSpan.FromSeconds((double)fi.Length / _samplingRate);
        }
    }
}
