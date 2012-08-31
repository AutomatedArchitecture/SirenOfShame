using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SirenOfShame.Test.Unit.UI
{
    [TestClass]
    public class ViewBuildsTest
    {
        [TestMethod]
        public void GetIdealSmallRows()
        {
            Assert.AreEqual(2, ViewBuilds.GetIdealSmallRows(1));
            Assert.AreEqual(2, ViewBuilds.GetIdealSmallRows(2));
            Assert.AreEqual(2, ViewBuilds.GetIdealSmallRows(3));
            Assert.AreEqual(2, ViewBuilds.GetIdealSmallRows(4));
            Assert.AreEqual(2, ViewBuilds.GetIdealSmallRows(5));
            Assert.AreEqual(2, ViewBuilds.GetIdealSmallRows(6));
            Assert.AreEqual(2, ViewBuilds.GetIdealSmallRows(7));
            Assert.AreEqual(2, ViewBuilds.GetIdealSmallRows(8));
            Assert.AreEqual(1, ViewBuilds.GetIdealSmallRows(9));
            Assert.AreEqual(1, ViewBuilds.GetIdealSmallRows(10));
            Assert.AreEqual(0, ViewBuilds.GetIdealSmallRows(11));
            Assert.AreEqual(0, ViewBuilds.GetIdealSmallRows(12));
            Assert.AreEqual(0, ViewBuilds.GetIdealSmallRows(13));
            Assert.AreEqual(0, ViewBuilds.GetIdealSmallRows(14));
            Assert.AreEqual(0, ViewBuilds.GetIdealSmallRows(15));
            Assert.AreEqual(0, ViewBuilds.GetIdealSmallRows(16));
        }
    }
}
