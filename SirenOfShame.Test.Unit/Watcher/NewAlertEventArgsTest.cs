using System;
using NUnit.Framework;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    [TestFixture]
    public class NewAlertEventArgsTest
    {
        [Test]
        public void Instantiate_ThreeArgsAllGood_ReturnsTrue()
        {
            var newAlertArgs = new NewAlertEventArgs();
            var result = newAlertArgs.Instantiate(@"56
http://www.google.com
Hello World
633979872000000000");
            Assert.IsTrue(result);
            Assert.AreEqual(56, newAlertArgs.SoftwareInstanceId);
            Assert.AreEqual("Hello World", newAlertArgs.Message);
            Assert.AreEqual("http://www.google.com", newAlertArgs.Url);
            Assert.AreEqual(new DateTime(2010, 1, 2), newAlertArgs.AlertDate);
        }
        
        [Test]
        public void Instantiate_Error_ReturnsFalse()
        {
            var newAlertArgs = new NewAlertEventArgs();
            var result = newAlertArgs.Instantiate("Error Occurred");
            Assert.IsFalse(result);
        }
        
        [Test]
        public void Instantiate_ExtraArgs_Ignored()
        {
            var newAlertArgs = new NewAlertEventArgs();
            var result = newAlertArgs.Instantiate("56\r\nhttp://www.google.com\r\nHello World\r\n633979872000000000\r\n\r\n\r\n\r\n\r\n");
            Assert.IsTrue(result);
            Assert.AreEqual(56, newAlertArgs.SoftwareInstanceId);
            Assert.AreEqual("Hello World", newAlertArgs.Message);
            Assert.AreEqual("http://www.google.com", newAlertArgs.Url);
        }
    }
}
