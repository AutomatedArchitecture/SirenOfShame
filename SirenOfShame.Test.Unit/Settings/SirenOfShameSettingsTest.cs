// ReSharper disable InconsistentNaming
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Test.Unit.Watcher;

namespace SirenOfShame.Test.Unit.Settings
{
    [TestClass]
    public class SirenOfShameSettingsTest
    {
        [TestMethod]
        public void Upgrade_NullVersion_UpgradedToMostRecent()
        {
            var settings = new SirenOfShameSettingsFake();
            settings.DoUpgrade();
            Assert.AreEqual(1, settings.Version);
        }
    }
}
