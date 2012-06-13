using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    [TestClass]
    public class SosDbTest
    {
        [TestMethod]
        public void ExportNewBuilds_NoBuilds_Null()
        {
            SosDbFake sosDb = new SosDbFake();
            SirenOfShameSettings settings = new SirenOfShameSettings(useMef: false);
            settings.SosOnlineHighWaterMark = null;
            sosDb.BuildStatuses = new BuildStatus[] { };
            var result = sosDb.ExportNewBuilds(settings);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Write_Writes()
        {
            string expectedFileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Automated Architecture", "SirenOfShame", "BuildDefinitionId.txt");
            File.Delete(expectedFileLocation);
            var sosDb = new SosDb();
            BuildStatus buildStatus = new BuildStatus
            {
                BuildDefinitionId = "BuildDefinitionId",
                BuildStatusEnum = BuildStatusEnum.Working,
                Comment = "hi",
                FinishedTime = new DateTime(2010, 1, 1, 1, 1, 1),
                StartedTime = new DateTime(2010, 1, 1, 1, 1, 2),
                LocalStartTime = new DateTime(2010, 1, 1, 1, 1, 3),
                RequestedBy = "Lee",
                Name = "BuildName",
            };
            SirenOfShameSettingsFake fakeSettings = new SirenOfShameSettingsFake();
            sosDb.Write(buildStatus, fakeSettings);
            Assert.IsTrue(File.Exists(expectedFileLocation));
            string[] linesOutput = File.ReadAllLines(expectedFileLocation);
            Assert.AreEqual(1, linesOutput.Length);
            Assert.AreEqual("633979044620000000,633979044610000000,1,Lee", linesOutput[0]);
            File.Delete(expectedFileLocation);
        }
        
        [TestMethod]
        public void Write_TwoStatuses_TwoWrites()
        {
            string expectedFileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Automated Architecture", "SirenOfShame", "BuildDefinitionId.txt");
            File.Delete(expectedFileLocation);
            var sosDb = new SosDb();
            BuildStatus buildStatus = new BuildStatus
            {
                BuildDefinitionId = "BuildDefinitionId",
                BuildStatusEnum = BuildStatusEnum.Working,
                Comment = "hi",
                FinishedTime = new DateTime(2010, 1, 1, 1, 1, 1),
                StartedTime = new DateTime(2010, 1, 1, 1, 1, 2),
                LocalStartTime = new DateTime(2010, 1, 1, 1, 1, 3),
                RequestedBy = "Lee",
                Name = "BuildName",
            };
            SirenOfShameSettingsFake fakeSettings = new SirenOfShameSettingsFake();
            sosDb.Write(buildStatus, fakeSettings);
            sosDb.Write(buildStatus, fakeSettings);
            Assert.IsTrue(File.Exists(expectedFileLocation));
            string[] linesOutput = File.ReadAllLines(expectedFileLocation);
            Assert.AreEqual(2, linesOutput.Length);
            Assert.AreEqual("633979044620000000,633979044610000000,1,Lee", linesOutput[0]);
            Assert.AreEqual("633979044620000000,633979044610000000,1,Lee", linesOutput[1]);
            File.Delete(expectedFileLocation);
        }
        
        [TestMethod]
        public void Write_InvalidCharacters_Removed()
        {
            const string uglyBuildDefinition = "\"M\"\\(a)/ry/ h**ad:>> a\\/:*?\"| li*tt|le|| la\"mb.?";
            string expectedFileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Automated Architecture", "SirenOfShame", "M(a)ry had a little lamb.txt");
            File.Delete(expectedFileLocation);
            var sosDb = new SosDb();
            BuildStatus buildStatus = new BuildStatus
            {
                BuildDefinitionId = uglyBuildDefinition,
                BuildStatusEnum = BuildStatusEnum.Working,
                Name = "BuildName",
            };
            SirenOfShameSettingsFake fakeSettings = new SirenOfShameSettingsFake();
            sosDb.Write(buildStatus, fakeSettings);
            Assert.IsTrue(File.Exists(expectedFileLocation));
            File.Delete(expectedFileLocation);
        }
    }
}
