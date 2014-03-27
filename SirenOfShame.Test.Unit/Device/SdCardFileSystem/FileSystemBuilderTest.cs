using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using SirenOfShame.Lib.Device.SdCardFileSystem;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Test.Unit.TestHelpers;
using SoxLib.Helpers;

namespace SirenOfShame.Test.Unit.Device.SdCardFileSystem
{
    [TestFixture]
    public class FileSystemBuilderTest
    {
        private readonly Random _random = new Random();

        [Test]
        public void Build()
        {
            var audioPattern0Data = CreateRandomPattern(1056);
            var audioPattern1Data = new byte[2000];
            var audioPattern2Data = new byte[5000];
            var audioPattern0 = new UploadAudioPatternMemory("Audio 0", audioPattern0Data);
            var audioPattern1 = new UploadAudioPatternMemory("Audio 1", audioPattern1Data);
            var audioPattern2 = new UploadAudioPatternMemory("Audio 2", audioPattern2Data);
            var audioPatterns = new[] { audioPattern0, audioPattern1, audioPattern2 };

            var ledPattern0Data = new byte[102];
            var ledPattern1Data = new byte[48];
            var ledPattern2Data = new byte[60];
            var ledPattern0 = new UploadLedPattern("LED 0", ledPattern0Data);
            var ledPattern1 = new UploadLedPattern("LED 1", ledPattern1Data);
            var ledPattern2 = new UploadLedPattern("LED 2", ledPattern2Data);
            var ledPatterns = new[] { ledPattern0, ledPattern1, ledPattern2 };

            Stream resultStream = new FileSystemBuilder().Build(audioPatterns, ledPatterns);
            byte[] result = resultStream.ReadToEnd();

            // 0x20c0 - EOF
            Assert.AreEqual(8672, result.Length);

            // 0x0000 - Header
            var header = ObjectHelpers.Deserialize<FileSystemHeader>(result, 0x0000);
            Assert.AreEqual("SoS", Encoding.ASCII.GetString(header.SoS).TrimEnd('\0'));
            Assert.AreEqual(3, header.LedPatCount);
            Assert.AreEqual(3, header.AudioPatCount);

            // 0x0020 - Allocation Table - LED 0 (102 bytes)
            var led0 = ObjectHelpers.Deserialize<UsbLedFatPacket>(result, 0x0020);
            Assert.AreEqual("LED 0", Encoding.ASCII.GetString(led0.Name).TrimEnd('\0'));
            Assert.AreEqual(0x00c0, (int)led0.Addr);
            Assert.AreEqual(ledPattern0Data.Length, led0.Length);

            // 0x0036 - Allocation Table - LED 1 (24 bytes)
            var led1 = ObjectHelpers.Deserialize<UsbLedFatPacket>(result, 0x0036);
            Assert.AreEqual("LED 1", Encoding.ASCII.GetString(led1.Name).TrimEnd('\0'));
            Assert.AreEqual(0x0140, (int)led1.Addr);
            Assert.AreEqual(ledPattern1Data.Length, led1.Length);

            // 0x004c - Allocation Table - LED 2 (30 bytes)
            var led2 = ObjectHelpers.Deserialize<UsbLedFatPacket>(result, 0x004c);
            Assert.AreEqual("LED 2", Encoding.ASCII.GetString(led2.Name).TrimEnd('\0'));
            Assert.AreEqual(0x0180, (int)led2.Addr);
            Assert.AreEqual(ledPattern2Data.Length, led2.Length);

            // 0x0062 - Allocation Table - Audio 0 (1000 bytes)
            var audio0 = ObjectHelpers.Deserialize<UsbLedFatPacket>(result, 0x0062);
            Assert.AreEqual("Audio 0", Encoding.ASCII.GetString(audio0.Name).TrimEnd('\0'));
            Assert.AreEqual(0x01c0, (int)audio0.Addr);
            Assert.AreEqual(audioPattern0Data.Length + 32, audio0.Length);

            // 0x0078 - Allocation Table - Audio 1 (2000 bytes)
            var audio1 = ObjectHelpers.Deserialize<UsbLedFatPacket>(result, 0x0078);
            Assert.AreEqual("Audio 1", Encoding.ASCII.GetString(audio1.Name).TrimEnd('\0'));
            Assert.AreEqual(1568, (int)audio1.Addr);
            Assert.AreEqual(audioPattern1Data.Length + 16, audio1.Length);

            // 0x008e - Allocation Table - Audio 2 (5000 bytes)
            var audio2 = ObjectHelpers.Deserialize<UsbLedFatPacket>(result, 0x008e);
            Assert.AreEqual("Audio 2", Encoding.ASCII.GetString(audio2.Name).TrimEnd('\0'));
            Assert.AreEqual(3616, (int)audio2.Addr);
            Assert.AreEqual(audioPattern2Data.Length + 24, audio2.Length);

            // 0x00c0 - LED 0 Data
            AssertHelpers.AreEquals(ledPattern0Data, 0, result, 0x00c0, ledPattern0Data.Length);

            // 0x0140 - LED 1 Data
            AssertHelpers.AreEquals(ledPattern1Data, 0, result, 0x0140, ledPattern1Data.Length);

            // 0x0160 - LED 2 Data
            AssertHelpers.AreEquals(ledPattern2Data, 0, result, 0x0160, ledPattern2Data.Length);

            // 0x0180 - Audio 0 Data
            AssertHelpers.AreEquals(audioPattern0Data, 0, result, 0x01c0, audioPattern0Data.Length);

            // 0x0580 - Audio 1 Data
            AssertHelpers.AreEquals(audioPattern1Data, 0, result, 1568, audioPattern1Data.Length);

            // 0x0d60 - Audio 2 Data
            AssertHelpers.AreEquals(audioPattern2Data, 0, result, 3616, audioPattern2Data.Length);
        }

        private byte[] CreateRandomPattern(int length)
        {
            byte[] result = new byte[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = (byte)_random.Next();
            }
            return result;
        }
    }
}
