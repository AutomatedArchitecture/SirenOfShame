using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Achievements;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestClass]
    public class ShamePusherTest
    {
        [TestMethod]
        public void SirenHasBeenConnectedAndCurrentUserJustCheckedIn()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser" };
            SirenOfShameSettings settings = new SirenOfShameSettings(false)
            {
                SirenEverConnected = true,
                MyRawName = "currentUser"
            };
            Assert.AreEqual(true, new ShamePusher(personSetting, settings).HasJustAchieved());
        }
        
        [TestMethod]
        public void SirenHasBeenConnectedButNoCurrentUser()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser" };
            SirenOfShameSettings settings = new SirenOfShameSettings(false)
            {
                SirenEverConnected = true,
                MyRawName = null
            };
            Assert.AreEqual(false, new ShamePusher(personSetting, settings).HasJustAchieved());
        }
        
        [TestMethod]
        public void SirenHasBeenConnectedButDifferentUserCheckedIn()
        {
            PersonSetting personSetting = new PersonSetting {RawName = "someoneElse"};
            SirenOfShameSettings settings = new SirenOfShameSettings(false)
            {
                SirenEverConnected = true,
                MyRawName = "currentUser"
            };
            Assert.AreEqual(false, new ShamePusher(personSetting, settings).HasJustAchieved());
        }
        
        [TestMethod]
        public void SirenNeverBeenConnected()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser" };
            SirenOfShameSettings settings = new SirenOfShameSettings(false)
            {
                SirenEverConnected = false,
                MyRawName = "currentUser"
            };
            Assert.AreEqual(false, new ShamePusher(personSetting, settings).HasJustAchieved());
        }
    }
}
