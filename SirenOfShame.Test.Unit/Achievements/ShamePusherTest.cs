using NUnit.Framework;
using SirenOfShame.Lib.Achievements;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestFixture]
    public class ShamePusherTest
    {
        [Test]
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
        
        [Test]
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
        
        [Test]
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
        
        [Test]
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
