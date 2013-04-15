// ReSharper disable InconsistentNaming
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Settings.Upgrades;
using SirenOfShame.Test.Unit.Watcher;

namespace SirenOfShame.Test.Unit.Settings.Upgrades
{
    [TestClass]
    public class Upgrade1To2Test
    {
        [TestMethod]
        public void Upgrade_AllPeopleNotHidden()
        {
            var settings = new SirenOfShameSettingsFake
                               {
                                   Version = null
                               };
            settings.People.Add(new PersonSetting
                                    {
                                        DisplayName = "Bob Thomas",
                                        FailedBuilds = 3,
                                        RawName = "domain\\bthomas",
                                        TotalBuilds = 100
                                    });
            new Upgrade1To2().Upgrade(settings);
            Assert.AreEqual(1, settings.People.Count);
            Assert.AreEqual(false, settings.People[0].Hidden);
        }
    }
}
