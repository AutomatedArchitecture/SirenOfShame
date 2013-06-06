using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame.Test.Unit.Helpers
{
    [TestClass]
    public class PrettyDateHelperTest
    {
        [TestMethod]
        public void WhenIsNow()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            Assert.AreEqual("just now", d.PrettyDate(now));
        }
        
        [TestMethod]
        public void FiftyNineSecondsAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 1, 0, 0, 59, 999);
            Assert.AreEqual("just now", d.PrettyDate(now));
        }
        
        [TestMethod]
        public void OneMinuteAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 1, 0, 1, 0, 0);
            Assert.AreEqual("1 minute ago", d.PrettyDate(now));
        }
        
        [TestMethod]
        public void OneMinuteFiftyNineSecondsAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 1, 0, 1, 59, 999);
            Assert.AreEqual("1 minute ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void TwoMinutesAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 1, 0, 2, 0, 0);
            Assert.AreEqual("2 minutes ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void FiftyNineMinutesAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 1, 0, 59, 59, 999);
            Assert.AreEqual("59 minutes ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void OneHourAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 1, 1, 0, 0, 0);
            Assert.AreEqual("1 hour ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void OneHourFiftyNineMinutesAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 1, 1, 59, 59, 999);
            Assert.AreEqual("1 hour ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void TwoHoursAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 1, 2, 0, 0, 0);
            Assert.AreEqual("2 hours ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void TwentyThreeHoursAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 1, 23, 59, 59, 999);
            Assert.AreEqual("23 hours ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void OneDayAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 2, 0, 0, 0, 0);
            Assert.AreEqual("yesterday", d.PrettyDate(now));
        }

        [TestMethod]
        public void OneDayTwentyThreeHoursAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 2, 23, 59, 59, 999);
            Assert.AreEqual("yesterday", d.PrettyDate(now));
        }

        [TestMethod]
        public void TwoDaysAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 3, 0, 0, 0, 0);
            Assert.AreEqual("2 days ago", d.PrettyDate(now));
        }
        
        [TestMethod]
        public void SixDaysAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 7, 23, 59, 59, 999);
            Assert.AreEqual("6 days ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void SevenDaysAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 8, 0, 0, 0, 0);
            Assert.AreEqual("last week", d.PrettyDate(now));
        }
        
        [TestMethod]
        public void ThirteenDaysAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 14, 23, 59, 59, 999);
            Assert.AreEqual("last week", d.PrettyDate(now));
        }

        [TestMethod]
        public void FourteenDaysAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 15, 0, 0, 0, 0);
            Assert.AreEqual("2 weeks ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void ThreeWeeksAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 1, 28, 23, 59, 59, 999);
            Assert.AreEqual("3 weeks ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void FourWeeksAgo()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 2, 4, 23, 59, 59, 999);
            Assert.AreEqual("4 weeks ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void OneMonthAgoMin()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 2, 5, 0, 0, 0, 0);
            Assert.AreEqual("1 month ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void OneMonthAgoMax()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 3, 1, 23, 59, 59, 999);
            Assert.AreEqual("1 month ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void TwoMonthsAgoMin()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 3, 3, 0, 0, 0, 0);
            Assert.AreEqual("2 months ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void TwoMonthsAgoMax()
        {
            var d = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 3, 31, 23, 59, 59, 999);
            Assert.AreEqual("2 months ago", d.PrettyDate(now));
        }

        [TestMethod]
        public void OneYearAgo()
        {
            var d = new DateTime(2009, 1, 1, 0, 0, 0, 0);
            var now = new DateTime(2010, 2, 5, 0, 0, 0, 0);
            Assert.AreEqual(d.ToString("d"), d.PrettyDate(now));
        }

    }
}
