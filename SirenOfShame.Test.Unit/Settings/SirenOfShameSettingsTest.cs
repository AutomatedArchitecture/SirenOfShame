using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var settings = new SirenOfShameSettingsFake();
            settings.DoUpgrade();
            Assert.AreEqual(1, settings.Version);
        }
        
        [TestMethod]
        public void Upgrade_EmptyPeople_Removed()
        {
            var settings = new SirenOfShameSettingsFake();
            settings.CiEntryPointSettings.Add(new CiEntryPointSetting
            {
                BuildDefinitionSettings = new List<BuildDefinitionSetting>
                {
                    new BuildDefinitionSetting
                    {
                        People = new List<string>
                        {
                            "Bob",
                            ""
                        }
                    }
                }
            });
            settings.DoUpgrade();
            var allPeople = settings.CiEntryPointSettings
                .SelectMany(i => i.BuildDefinitionSettings)
                .SelectMany(i => i.People);
            Assert.IsFalse(allPeople.Any(p => string.IsNullOrEmpty(p)));
        }
    }
}
