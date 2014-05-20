using NUnit.Framework;
using SirenOfShame.Lib.Services;

namespace SirenOfShame.Test.Unit.Services
{
    [TestFixture]
    public class SoundServiceTest
    {
        [Test]
        public void LocationToDisplayName_Typical()
        {
            Assert.AreEqual("Sad Trombone", SoundService.InternalAudioLocationToDisplayName("SirenOfShame.Resources.Audio-Sad-Trombone.wav"));
        }

        [Test]
        public void InternalAudioLocationToDisplayName_WhenLocationIsOldInternalName_NamespaceIsRemoved()
        {
            var result = SoundService.InternalAudioLocationToDisplayName("SirenOfShame.Resources.Audio-Ding.wav");
            Assert.AreEqual("Ding", result);
        }

        [Test]
        public void InternalAudioLocationToDisplayName_WhenLocationIsNewInternalName_NamespaceIsRemoved()
        {
            var result = SoundService.InternalAudioLocationToDisplayName("SirenOfShame.Lib.Resources.Audio-Ding.wav");
            Assert.AreEqual("Ding", result);
        }
        
        [Test]
        public void InternalAudioLocationToDisplayName_WhenLocationIsFileName_ExtensionIsRemoved()
        {
            var result = SoundService.InternalAudioLocationToDisplayName("MyFile.wav");
            Assert.AreEqual("MyFile", result);
        }
    }
}
