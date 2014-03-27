using NUnit.Framework;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame.Test.Unit.Helpers
{
    [TestFixture]
    public class StringHelpersTest
    {
        [Test]
        public void RemoveHttpPrefix_Null_Null()
        {
            Assert.IsNull(StringHelpers.RemoveUrlPrefix(null));
        }
        
        [Test]
        public void RemoveUrlPrefix_UnderLength_ReturnsFullString()
        {
            Assert.AreEqual("server", StringHelpers.RemoveUrlPrefix("server"));
        }
        
        [Test]
        public void RemoveUrlPrefix_TypicalUrl_RemovesHttp()
        {
            Assert.AreEqual("google.com", StringHelpers.RemoveUrlPrefix("http://google.com"));
        }
    }
}
