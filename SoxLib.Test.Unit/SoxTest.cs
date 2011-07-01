using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoxLib.Test.Unit.TestHelpers;

namespace SoxLib.Test.Unit
{
    [TestClass]
    public class SoxTest
    {
        [TestMethod]
        public void ConvertWavToRaw()
        {
            Sox sox = new Sox
            {
                SoxDirectory = Path.Combine(TestHelper.GetSolutionDirectory(), @"libs\sox-14.3.2\")
            };
            ConvertOptions options = new ConvertOptions
            {
                InputFileInfo = new FileInfo
                {
                    FileType = FileType.Wav,
                },
                OutputFileInfo = new FileInfo
                {
                    FileType = FileType.RawUnsignedInteger8,
                    Channels = 1,
                    SampleSizeInBits = 8,
                    SamplingRate = 8000,
                    EncodingType = EncodingType.UnsignedInteger
                }
            };

            int preRunTempDirFileCount = Directory.GetFiles(sox.TempDir).Length;
            using (Stream input = GetType().Assembly.GetManifestResourceStream("SoxLib.Test.Unit.doh.wav"))
            using (Stream expected = GetType().Assembly.GetManifestResourceStream("SoxLib.Test.Unit.doh-wav.u8"))
            using (Stream output = sox.Convert(input, options))
            {
                StreamTestHelper.AssertAreEqual(expected, output, 2);
            }
            Assert.AreEqual(preRunTempDirFileCount, Directory.GetFiles(sox.TempDir).Length);
        }

        [TestMethod]
        public void ConvertWavToMp3()
        {
            Sox sox = new Sox
            {
                SoxDirectory = Path.Combine(TestHelper.GetSolutionDirectory(), @"libs\sox-14.3.2\")
            };
            ConvertOptions options = new ConvertOptions
            {
                InputFileInfo = new FileInfo
                {
                    FileType = FileType.Mp3,
                },
                OutputFileInfo = new FileInfo
                {
                    FileType = FileType.RawUnsignedInteger8,
                    Channels = 1,
                    SampleSizeInBits = 8,
                    SamplingRate = 8000,
                    EncodingType = EncodingType.UnsignedInteger
                }
            };

            int preRunTempDirFileCount = Directory.GetFiles(sox.TempDir).Length;
            using (Stream input = GetType().Assembly.GetManifestResourceStream("SoxLib.Test.Unit.doh.mp3"))
            using (Stream expected = GetType().Assembly.GetManifestResourceStream("SoxLib.Test.Unit.doh-mp3.u8"))
            using (Stream output = sox.Convert(input, options))
            {
                StreamTestHelper.AssertAreEqual(expected, output, 2);
            }
            Assert.AreEqual(preRunTempDirFileCount, Directory.GetFiles(sox.TempDir).Length);
        }

        [TestMethod]
        public void Trim()
        {
            Sox sox = new Sox
            {
                SoxDirectory = Path.Combine(TestHelper.GetSolutionDirectory(), @"libs\sox-14.3.2\")
            };
            TrimOptions options = new TrimOptions
            {
                InputFileInfo = new FileInfo
                {
                    FileType = FileType.RawUnsignedInteger8,
                    Channels = 1,
                    SampleSizeInBits = 8,
                    SamplingRate = 8000,
                    EncodingType = EncodingType.UnsignedInteger
                },
                OutputFileInfo = new FileInfo
                {
                    FileType = FileType.RawUnsignedInteger8,
                    Channels = 1,
                    SampleSizeInBits = 8,
                    SamplingRate = 8000,
                    EncodingType = EncodingType.UnsignedInteger
                },
                StartTime = new SoxTimeSamples { Value = 0 },
                Length = new SoxTimeSamples { Value = 1000 },
            };

            int preRunTempDirFileCount = Directory.GetFiles(sox.TempDir).Length;
            using (Stream input = GetType().Assembly.GetManifestResourceStream("SoxLib.Test.Unit.doh-wav.u8"))
            using (Stream expected = GetType().Assembly.GetManifestResourceStream("SoxLib.Test.Unit.doh-wav-trim.u8"))
            using (Stream output = sox.Trim(input, options))
            {
                StreamTestHelper.AssertAreEqual(expected, output, 2);
            }
            Assert.AreEqual(preRunTempDirFileCount, Directory.GetFiles(sox.TempDir).Length);
        }
    }
}
