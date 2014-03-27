using NUnit.Framework;
using SirenOfShame.Resources2;

namespace SirenOfShame.Test.Unit.Resources
{
    [TestFixture]
    public class AudioFileTest
    {
        [Test]
        public void LocationToDisplayName_Typical()
        {
            Assert.AreEqual("Sad Trombone", AudioFile.LocationToDisplayName("SirenOfShame.Resources.Audio-Sad-Trombone.wav"));
        }
    }
}
