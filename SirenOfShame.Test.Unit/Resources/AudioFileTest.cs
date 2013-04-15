using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Resources2;

namespace SirenOfShame.Test.Unit.Resources
{
    [TestClass]
    public class AudioFileTest
    {
        [TestMethod]
        public void LocationToDisplayName_Typical()
        {
            Assert.AreEqual("Sad Trombone", AudioFile.LocationToDisplayName("SirenOfShame.Resources.Audio-Sad-Trombone.wav"));
        }
    }
}
