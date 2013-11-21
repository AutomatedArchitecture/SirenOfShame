using SirenOfShame.Lib.Watcher;
// ReSharper disable InconsistentNaming
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Test.Unit.Watcher;

namespace SirenOfShame.Test.Unit.Settings
{
    public class MyMainForm : MainForm
    {
        protected override SirenOfShameSettings GetAppSettings()
        {
            return new SirenOfShameSettingsFake();
        }

        protected override void ShowInMainWindow(MainWindowEnum mainWindow)
        {
        }

        protected override void InitializeNotifyIcon()
        {
            NotityIconVisible = false;
        }
    }

    [TestClass]
    public class SirenOfShameSettingsTest
    {
        [TestMethod]
        public void GetUsersContainedInBuildsAsDto_BuildListContainsPerson_PersonIsReturned()
        {
            var settings = new SirenOfShameSettingsFake();
            settings.People.Add(new PersonSetting { RawName = "Bob"});
            IList<BuildStatus> changedBuildStatuses = new List<BuildStatus>
            {
                new BuildStatus { RequestedBy = "Bob" }
            };
            var results = settings.GetUsersContainedInBuildsAsDto(changedBuildStatuses);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Bob", results[0].RawName);
        }

        [TestMethod]
        public void GetUsersContainedInBuildsAsDto_BuildListContainsHiddenPerson_PersonStillReturned()
        {
            var settings = new SirenOfShameSettingsFake();
            settings.People.Add(new PersonSetting { RawName = "Bob", Hidden = true });
            IList<BuildStatus> changedBuildStatuses = new List<BuildStatus>
            {
                new BuildStatus { RequestedBy = "Bob" }
            };
            var results = settings.GetUsersContainedInBuildsAsDto(changedBuildStatuses);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Bob", results[0].RawName);
        }

        [TestMethod]
        public void GetUsersContainedInBuildsAsDto_BuildListDoesNotContainPerson_PersonIsNotReturned()
        {
            var settings = new SirenOfShameSettingsFake();
            settings.People.Add(new PersonSetting { RawName = "Bob"});
            IList<BuildStatus> changedBuildStatuses = new List<BuildStatus>
            {
                new BuildStatus { RequestedBy = "Sam" }
            };
            var results = settings.GetUsersContainedInBuildsAsDto(changedBuildStatuses);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void IsGettingStarted_OneServerAndAlwaysOffline()
        {
            SirenOfShameSettingsFake settings = new SirenOfShameSettingsFake();
            settings.SosOnlineAlwaysOffline = true;
            settings.CiEntryPointSettings.Add(new CiEntryPointSetting());
            Assert.IsFalse(settings.IsGettingStarted());
        }
        
        [TestMethod]
        public void IsGettingStarted_OneServerAndIsSosOnline()
        {
            SirenOfShameSettingsFake settings = new SirenOfShameSettingsFake();
            settings.SosOnlineUsername = "Bob";
            settings.CiEntryPointSettings.Add(new CiEntryPointSetting());
            Assert.IsFalse(settings.IsGettingStarted());
        }
        
        [TestMethod]
        public void IsGettingStarted_NoServerNoSosOnline()
        {
            SirenOfShameSettingsFake settings = new SirenOfShameSettingsFake();
            settings.SosOnlineUsername = null;
            Assert.IsTrue(settings.IsGettingStarted());
        }
        
        [TestMethod]
        public void AvatarCountConstIsCorrect()
        {
            var mainForm = new MyMainForm();
            const int genericImageCount = 1;
            Assert.AreEqual(SirenOfShameSettings.AVATAR_COUNT + genericImageCount, mainForm.AvatarCount);
        }

        [TestMethod]
        public void FindAddUser_SecondUser_AvatarIdIsTwo()
        {
            var settings = new SirenOfShameSettingsFake
            {
                People = new List<PersonSetting>
                {
                    new PersonSetting {RawName = "Bob"}
                }
            };
            settings.FindAddPerson("Sam");
            Assert.AreEqual(2, settings.People.Count);
            Assert.AreEqual(1, settings.People[1].AvatarId);
        }

        [TestMethod]
        public void FindAddUser_ThirdUserWithTwoAvatars_NewUserAvatarIdLoopsToZero()
        {
            var settings = new SirenOfShameSettingsFake
            {
                People = new List<PersonSetting>
                {
                    new PersonSetting {RawName = "Bob"},
                    new PersonSetting {RawName = "Sam"}
                }
            };
            settings.FindAddPerson("Sally", 2);
            Assert.AreEqual(3, settings.People.Count);
            Assert.AreEqual(0, settings.People[2].AvatarId);
        }

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
            Assert.AreEqual(SirenOfShameSettings.CurrentVersion, settings.Version);
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
