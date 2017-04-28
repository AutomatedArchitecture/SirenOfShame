// ReSharper disable InconsistentNaming
using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    [TestFixture]
    public class SosDbTest
    {
        [Test]
        public void ExportNewBuilds_InitialExportWithNoBuilds_Null()
        {
            SosDbFake sosDb = new SosDbFake();
            SirenOfShameSettings settings = new SirenOfShameSettingsFake
            {
                SosOnlineHighWaterMark = null, 
                MyRawName = "CurrentUser"
            };
            sosDb.Write(new BuildStatus { BuildDefinitionId = "BD"}, settings, true);
            var result = sosDb.ExportNewBuilds(settings);
            Assert.IsNull(result);
        }

        [Test]
        public void ExportNewBuilds_InitialExportWithOneSuccessfulBuildByCurrentUser_Exports()
        {
            SosDbFake sosDb = new SosDbFake();
            SirenOfShameSettings settings = new SirenOfShameSettingsFake
            {
                SosOnlineHighWaterMark = null,
                MyRawName = "CurrentUser",
                CiEntryPointSettings = new List<CiEntryPointSetting>
                {
                    new CiEntryPointSetting
                    {
                        BuildDefinitionSettings = new List<BuildDefinitionSetting>
                        {
                            new BuildDefinitionSetting
                            {
                                Id = "BuildDefinitionId",
                                Active = true
                            }
                        }
                    }
                }
            };
            sosDb.Write(new BuildStatus
            {
                StartedTime = new DateTime(2010, 1, 1, 1, 1, 1),
                FinishedTime = new DateTime(2010, 1, 1, 1, 1, 2),
                CurrentBuildStatus = BuildStatusEnum.Working,
                BuildDefinitionId = "BuildDefinitionId",
                BuildId = "BuildId",
                Name = "Name",
                RequestedBy = "CurrentUser",
                Comment = "Comment",
            }, settings, false);
            var result = sosDb.ExportNewBuilds(settings);
            Assert.AreEqual("633979044610000000,633979044620000000,1", result);
        }

        [Test]
        public void ExportNewBuilds_InitialExportWithOneSuccessfulBuildBySomeoneElse_Null()
        {
            SosDbFake sosDb = new SosDbFake();
            SirenOfShameSettings settings = new SirenOfShameSettingsFake
            {
                SosOnlineHighWaterMark = null, 
                MyRawName = "CurrentUser"
            };
            sosDb.Write(new BuildStatus
            {
                StartedTime = new DateTime(2010, 1, 1, 1, 1, 1),
                FinishedTime = new DateTime(2010, 1, 1, 1, 1, 2),
                CurrentBuildStatus = BuildStatusEnum.Working,
                BuildDefinitionId = "BuildDefinitionId",
                BuildId = "BuildId",
                Name = "Name",
                RequestedBy = "SomeoneElse",
                Comment = "Comment",
            }, settings, true);
            var result = sosDb.ExportNewBuilds(settings);
            Assert.AreEqual(null, result);
        }

        [Test]
        public void ExportNewBuilds_SecondExportIgnoresOlderExports()
        {
            SosDbFake sosDb = new SosDbFake();
            SirenOfShameSettings settings = new SirenOfShameSettingsFake
            {
                SosOnlineHighWaterMark = 633979044610000000, 
                MyRawName = "CurrentUser"
            };
            sosDb.Write(new BuildStatus
            {
                StartedTime = new DateTime(2010, 1, 1, 1, 1, 1),
                FinishedTime = new DateTime(2010, 1, 1, 1, 1, 2),
                CurrentBuildStatus = BuildStatusEnum.Working,
                BuildDefinitionId = "BuildDefinitionId",
                BuildId = "BuildId",
                Name = "Name",
                RequestedBy = "CurrentUser",
                Comment = "Comment",
            }, settings, true);
            var result = sosDb.ExportNewBuilds(settings);
            Assert.AreEqual(null, result);
        }

        [Test]
        public void ExportNewBuilds_SecondExportGetsNewerExports()
        {
            SosDbFake sosDb = new SosDbFake();
            SirenOfShameSettings settings = new SirenOfShameSettingsFake
            {
                SosOnlineHighWaterMark = 633979044610000000, 
                MyRawName = "CurrentUser",
                CiEntryPointSettings = new List<CiEntryPointSetting>
                {
                    new CiEntryPointSetting
                    {
                        BuildDefinitionSettings = new List<BuildDefinitionSetting>
                        {
                            new BuildDefinitionSetting
                            {
                                Id = "BuildDefinitionId",
                                Active = true
                            }
                        }
                    }
                }
            };
            sosDb.Write(new BuildStatus
            {
                StartedTime = new DateTime(2010, 1, 1, 1, 1, 2),
                FinishedTime = new DateTime(2010, 1, 1, 1, 1, 3),
                CurrentBuildStatus = BuildStatusEnum.Broken,
                BuildDefinitionId = "BuildDefinitionId",
                BuildId = "BuildId",
                Name = "Name",
                RequestedBy = "CurrentUser",
                Comment = "Comment",
            }, settings, false);
            var result = sosDb.ExportNewBuilds(settings);
            Assert.AreEqual("633979044620000000,633979044630000000,0", result);
        }

        [Test]
        public void Write_Writes()
        {
            string expectedFileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Automated Architecture", "SirenOfShame", "BuildDefinitionId.txt");
            File.Delete(expectedFileLocation);
            var sosDb = new SosDb();
            BuildStatus buildStatus = new BuildStatus
            {
                BuildDefinitionId = "BuildDefinitionId",
                CurrentBuildStatus = BuildStatusEnum.Working,
                Comment = "hi",
                FinishedTime = new DateTime(2010, 1, 1, 1, 1, 1),
                StartedTime = new DateTime(2010, 1, 1, 1, 1, 2),
                LocalStartTime = new DateTime(2010, 1, 1, 1, 1, 3),
                RequestedBy = "Lee",
                Name = "BuildName",
            };
            SirenOfShameSettingsFake fakeSettings = new SirenOfShameSettingsFake();
            sosDb.Write(buildStatus, fakeSettings, false);
            Assert.IsTrue(File.Exists(expectedFileLocation));
            string[] linesOutput = File.ReadAllLines(expectedFileLocation);
            Assert.AreEqual(1, linesOutput.Length);
            Assert.AreEqual("633979044620000000,633979044610000000,1,Lee", linesOutput[0]);
            File.Delete(expectedFileLocation);
        }
        
        [Test]
        public void Write_TwoStatuses_TwoWrites()
        {
            string expectedFileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Automated Architecture", "SirenOfShame", "BuildDefinitionId.txt");
            File.Delete(expectedFileLocation);
            var sosDb = new SosDb();
            BuildStatus buildStatus = new BuildStatus
            {
                BuildDefinitionId = "BuildDefinitionId",
                CurrentBuildStatus = BuildStatusEnum.Working,
                Comment = "hi",
                FinishedTime = new DateTime(2010, 1, 1, 1, 1, 1),
                StartedTime = new DateTime(2010, 1, 1, 1, 1, 2),
                LocalStartTime = new DateTime(2010, 1, 1, 1, 1, 3),
                RequestedBy = "Lee",
                Name = "BuildName",
            };
            SirenOfShameSettingsFake fakeSettings = new SirenOfShameSettingsFake();
            sosDb.Write(buildStatus, fakeSettings, false);
            sosDb.Write(buildStatus, fakeSettings, false);
            Assert.IsTrue(File.Exists(expectedFileLocation));
            string[] linesOutput = File.ReadAllLines(expectedFileLocation);
            Assert.AreEqual(2, linesOutput.Length);
            Assert.AreEqual("633979044620000000,633979044610000000,1,Lee", linesOutput[0]);
            Assert.AreEqual("633979044620000000,633979044610000000,1,Lee", linesOutput[1]);
            File.Delete(expectedFileLocation);
        }
        
        [Test]
        public void Write_InvalidCharacters_Removed()
        {
            const string uglyBuildDefinition = "\"M\"\\(a)/ry/ h**ad:>> a\\/:*?\"| li*tt|le|| la\"mb.?";
            string expectedFileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Automated Architecture", "SirenOfShame", "M(a)ry had a little lamb.txt");
            File.Delete(expectedFileLocation);
            var sosDb = new SosDb();
            BuildStatus buildStatus = new BuildStatus
            {
                BuildDefinitionId = uglyBuildDefinition,
                CurrentBuildStatus = BuildStatusEnum.Working,
                Name = "BuildName",
            };
            SirenOfShameSettingsFake fakeSettings = new SirenOfShameSettingsFake();
            sosDb.Write(buildStatus, fakeSettings, false);
            Assert.IsTrue(File.Exists(expectedFileLocation));
            File.Delete(expectedFileLocation);
        }
    }
}
