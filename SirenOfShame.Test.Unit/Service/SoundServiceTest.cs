using NUnit.Framework;
using SirenOfShame.Lib.Services;

namespace SirenOfShame.Test.Unit.Service
{
    [TestFixture]
    public class SoundServiceTest
    {
        [Test]
        public void LocationToDisplayName_Typical()
        {
            Assert.AreEqual("Sad Trombone", SoundService.InternalAudioLocationToDisplayName("SirenOfShame.Resources.Audio-Sad-Trombone.wav"));
        }
    }
}
