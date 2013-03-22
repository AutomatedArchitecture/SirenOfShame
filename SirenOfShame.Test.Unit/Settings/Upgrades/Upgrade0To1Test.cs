// ReSharper disable InconsistentNaming
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Test.Unit.Watcher;

namespace SirenOfShame.Test.Unit.Settings.Upgrades
{
    [TestClass]
    public class Upgrade0To1Test
    {
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

        [TestMethod]
        public void Upgrade_PeopleInSettings_AddedToPeopleInBase()
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
                        }
                    }
                }
            });
            settings.DoUpgrade();
            Assert.AreEqual(1, settings.People.Count);
            PersonSetting personSetting = settings.People.First();
            Assert.AreEqual("Bob", personSetting.RawName);
            Assert.AreEqual("Bob", personSetting.DisplayName);
            Assert.AreEqual(0, personSetting.FailedBuilds);
            Assert.AreEqual(0, personSetting.TotalBuilds);
        }

        [TestMethod]
        public void Upgrade_SamePersonInMultipleSettings_NoDuplicates()
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
                        }
                    },
                    new BuildDefinitionSetting
                    {
                        People = new List<string>
                        {
                            "Bob",
                        }
                    }
                }
            });
            settings.DoUpgrade();
            Assert.AreEqual(1, settings.People.Count);
        }

    }
}
