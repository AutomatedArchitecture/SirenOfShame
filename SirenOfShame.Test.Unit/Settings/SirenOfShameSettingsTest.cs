// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Test.Unit.Watcher;

namespace SirenOfShame.Test.Unit.Settings
{
    [TestClass]
    public class SirenOfShameSettingsTest
    {
        [TestMethod]
        public void ExportNewAchievements_InitialExportWithNoAchievements_Null()
        {
            var settings = new SirenOfShameSettingsFake
            {
                SosOnlineHighWaterMark = null,
                MyRawName = "CurrentUser",
                People = new List<PersonSetting>
                {
                    new PersonSetting
                    {
                        RawName = "CurrentUser",
                        Achievements = new List<AchievementSetting>()
                    }
                }
            };
            Assert.IsNull(settings.ExportNewAchievements());
        }

        [TestMethod]
        public void ExportNewAchievements_InitialExportWithOneNewAchievementByCurrentUser_Exports()
        {
            var settings = new SirenOfShameSettingsFake
            {
                SosOnlineHighWaterMark = null,
                MyRawName = "CurrentUser",
                People = new List<PersonSetting>
                {
                    new PersonSetting
                    {
                        RawName = "CurrentUser",
                        Achievements = new List<AchievementSetting>
                        {
                            new AchievementSetting { AchievementId = 1, DateAchieved = new DateTime(2010, 1, 1, 1, 1, 2)}
                        }
                    }
                }
            };
            Assert.AreEqual("1,633979044620000000", settings.ExportNewAchievements());
        }

        [TestMethod]
        public void ExportNewBuilds_InitialExportWithOneSuccessfulBuildBySomeoneElse_Null()
        {
            var settings = new SirenOfShameSettingsFake
            {
                SosOnlineHighWaterMark = null,
                MyRawName = "CurrentUser",
                People = new List<PersonSetting>
                {
                    new PersonSetting
                    {
                        RawName = "SomeoneElse",
                        Achievements = new List<AchievementSetting>
                        {
                            new AchievementSetting { AchievementId = 1, DateAchieved = new DateTime(2010, 1, 1, 1, 1, 2)}
                        }
                    }
                }
            };
            Assert.AreEqual(null, settings.ExportNewAchievements());
        }

        [TestMethod]
        public void ExportNewBuilds_SecondExportIgnoresOlderExports()
        {
            var settings = new SirenOfShameSettingsFake
            {
                SosOnlineHighWaterMark = 633979044610000000,
                MyRawName = "CurrentUser",
                People = new List<PersonSetting>
                {
                    new PersonSetting
                    {
                        RawName = "CurrentUser",
                        Achievements = new List<AchievementSetting>
                        {
                            new AchievementSetting { AchievementId = 1, DateAchieved = new DateTime(2010, 1, 1, 1, 1, 1)},
                        }
                    }
                }
            };
            Assert.AreEqual(null, settings.ExportNewAchievements());
        }

        [TestMethod]
        public void ExportNewBuilds_SecondExportGetsNewAchievements()
        {
            var settings = new SirenOfShameSettingsFake
            {
                SosOnlineHighWaterMark = 633979044610000000,
                MyRawName = "CurrentUser",
                People = new List<PersonSetting>
                {
                    new PersonSetting
                    {
                        RawName = "CurrentUser",
                        Achievements = new List<AchievementSetting>
                        {
                            new AchievementSetting { AchievementId = 1, DateAchieved = new DateTime(2010, 1, 1, 1, 1, 1)},
                            new AchievementSetting { AchievementId = 2, DateAchieved = new DateTime(2010, 1, 1, 1, 1, 2)}
                        }
                    }
                }
            };
            Assert.AreEqual("2,633979044620000000", settings.ExportNewAchievements());
        }

        [TestMethod]
        public void Upgrade_NullVersion_UpgradedToMostRecent()
        {
            var settings = new SirenOfShameSettingsFake
                               {
                                   Version = null
                               };
            settings.DoUpgrade();
            Assert.AreEqual(5, settings.Version);
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
            var settings = new SirenOfShameSettings(useMef: false);
            Assert.IsNull(settings.SosOnlinePassword);
            settings.SetSosOnlinePassword("blah!");
            Assert.AreNotEqual("blah!", settings.SosOnlinePassword);
        }
        
        [TestMethod]
        public void SosOnlinePasswordDecryptsAsEncrypted()
        {
            var settings = new SirenOfShameSettings(useMef: false);
            settings.SetSosOnlinePassword("blah!");
            Assert.AreEqual("blah!", settings.GetSosOnlinePassword());
        }
    }
}
