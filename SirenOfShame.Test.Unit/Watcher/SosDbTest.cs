using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    [TestClass]
    public class SosDbTest
    {
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
            sosDb.Write(buildStatus);
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
            sosDb.Write(buildStatus);
            sosDb.Write(buildStatus);
            Assert.IsTrue(File.Exists(expectedFileLocation));
            string[] linesOutput = File.ReadAllLines(expectedFileLocation);
            Assert.AreEqual(2, linesOutput.Length);
            Assert.AreEqual("633979044620000000,633979044610000000,1,Lee", linesOutput[0]);
            Assert.AreEqual("633979044620000000,633979044610000000,1,Lee", linesOutput[1]);
            File.Delete(expectedFileLocation);
        }
    }
}
