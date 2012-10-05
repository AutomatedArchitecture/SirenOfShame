using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame.Test.Unit.Helpers
{
    [TestClass]
    public class StringHelpersTest
    {
        [TestMethod]
        public void RemoveHttpPrefix_Null_Null()
        {
            Assert.IsNull(StringHelpers.RemoveUrlPrefix(null));
        }
        
        [TestMethod]
        public void RemoveUrlPrefix_UnderLength_ReturnsFullString()
        {
            Assert.AreEqual("server", StringHelpers.RemoveUrlPrefix("server"));
        }
        
        [TestMethod]
        public void RemoveUrlPrefix_TypicalUrl_RemovesHttp()
        {
            Assert.AreEqual("google.com", StringHelpers.RemoveUrlPrefix("http://google.com"));
        }
    }
}
