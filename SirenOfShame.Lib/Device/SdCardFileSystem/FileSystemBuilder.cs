using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SirenOfShame.Lib.Helpers;
using SoxLib.Helpers;

namespace SirenOfShame.Lib.Device.SdCardFileSystem
{
    public class FileSystemBuilder
    {
        public Stream Build(IEnumerable<UploadAudioPattern> audioPatterns, IEnumerable<UploadLedPattern> ledPatterns)
        {
            MemoryStream result = new MemoryStream();

            WriteHeader(result, audioPatterns.Count(), ledPatterns.Count());
            WriteAllocationTable(result, audioPatterns, ledPatterns);
            WriteLedPatterns(result, ledPatterns);
            WriteAudioPatterns(result, audioPatterns);

            result.Flush();
            result.Position = 0;
            return result;
        }

        private void WriteAudioPatterns(Stream result, IEnumerable<UploadAudioPattern> audioPatterns)
        {
            foreach (var audioPattern in audioPatterns)
            {
                using (Stream stream = audioPattern.OpenData())
                {
                    stream.WriteToStream(result);
                }
                Write32BytePadding(result);
            }
        }

        private void WriteLedPatterns(Stream result, IEnumerable<UploadLedPattern> ledPatterns)
        {
            foreach (var uploadLedPattern in ledPatterns)
            {
                var pattern = uploadLedPattern.Pattern;
                result.Write(pattern, 0, pattern.Length);
                Write32BytePadding(result);
            }
        }

        private void Write32BytePadding(Stream result)
        {
            long paddingLength = 32 - result.Position % 32;
            byte[] padding = new byte[paddingLength];
            result.Write(padding, 0, padding.Length);
        }

        private void WriteAllocationTable(Stream result, IEnumerable<UploadAudioPattern> audioPatterns, IEnumerable<UploadLedPattern> ledPatterns)
        {
            byte[] b;
            int audioPatternCount = audioPatterns.Count();
            int ledPatternCount = ledPatterns.Count();
            int dataAddress = Marshal.SizeOf(typeof(FileSystemHeader));
            dataAddress += 32 - (dataAddress % 32);
            dataAddress += ledPatternCount * Marshal.SizeOf(typeof(UsbLedFatPacket));
            dataAddress += audioPatternCount * Marshal.SizeOf(typeof(UsbAudioFatPacket));
            dataAddress += 32 - dataAddress % 32;

            // led patterns
            foreach (var uploadLedPattern in ledPatterns)
            {
                ushort patternLength = (ushort)(uploadLedPattern.Pattern.Count());
                UsbLedFatPacket usbLedFatPacket = new UsbLedFatPacket
                {
                    Name = Encoding.ASCII.GetBytes(uploadLedPattern.Name.PadRight(16, '\0')),
                    Addr = (uint)dataAddress,
                    Length = patternLength
                };

                b = usbLedFatPacket.SerializeToBytes();
                result.Write(b, 0, b.Length);

                dataAddress += patternLength;
                dataAddress += 32 - dataAddress % 32;
            }

            // audio pattern
            foreach (var audioPattern in audioPatterns)
            {
                ushort patternLength = (ushort)(audioPattern.DataLength);
                UsbAudioFatPacket usbAudioFatPacket = new UsbAudioFatPacket
                {
                    Name = Encoding.ASCII.GetBytes(audioPattern.Name.PadRight(16, '\0')),
                    Addr = (uint)dataAddress,
                    Length = patternLength
                };

                b = usbAudioFatPacket.SerializeToBytes();
                result.Write(b, 0, b.Length);

                dataAddress += patternLength;
                dataAddress += 32 - dataAddress % 32;
            }

            Write32BytePadding(result);
        }

        public void WriteHeader(Stream result, int audioPatternCount, int ledPatternCount)
        {
            FileSystemHeader usbFatPacket = new FileSystemHeader
            {
                SoS = new[] { (byte)'S', (byte)'o', (byte)'S' },
                AudioPatCount = (ushort)audioPatternCount,
                LedPatCount = (ushort)ledPatternCount
            };
            var b = usbFatPacket.SerializeToBytes();
            result.Write(b, 0, b.Length);
            Write32BytePadding(result);
        }
    }
}
