// ReSharper disable InconsistentNaming
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Test.Unit.Watcher;

namespace SirenOfShame.Test.Unit.Settings
{
    [TestClass]
    public class SirenOfShameSettingsTest
    {
        [TestMethod]
        public void Upgrade_NullVersion_UpgradedToMostRecent()
        {
            var settings = new SirenOfShameSettingsFake
                               {
                                   Version = null
                               };
            settings.DoUpgrade();
            Assert.AreEqual(4, settings.Version);
        }
        
        [TestMethod]
        public void Upgrade_FutureVersion_NotDowngraded()
        {
            var settings = new SirenOfShameSettingsFake
                               {
                                   Version = int.MaxValue
                               };
            settings.DoUpgrade();
            Assert.AreEqual(int.MaxValue, settings.Version);
        }

        [TestMethod]
        public void SosOnlinePasswordIsStoredEncrypted()
        {
            var settings = new SirenOfShameSettings();
            Assert.IsNull(settings.SosOnlinePassword);
            settings.SetSosOnlinePassword("blah!");
            Assert.AreNotEqual("blah!", settings.SosOnlinePassword);
        }
        
        [TestMethod]
        public void SosOnlinePasswordDecryptsAsEncrypted()
        {
            var settings = new SirenOfShameSettings();
            settings.SetSosOnlinePassword("blah!");
            Assert.AreEqual("blah!", settings.GetSosOnlinePassword());
        }
    }
}
