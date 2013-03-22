using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Util;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Util
{
    [TestClass]
    public class BuildStatusUtilTest
    {
        [TestMethod]
        public void Merge_NewBuildStatus_Added()
        {
            var oldStatus = new BuildStatus[] {};
            var newStatuses = new[] {new BuildStatus {BuildDefinitionId = "1", BuildStatusEnum = BuildStatusEnum.Working}};
            var result = BuildStatusUtil.Merge(oldStatus, newStatuses);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("1", result[0].BuildDefinitionId);
            Assert.AreEqual(BuildStatusEnum.Working, result[0].BuildStatusEnum);
        }
        
        [TestMethod]
        public void Merge_RemovedBuildStatus_Retained()
        {
            var oldStatus = new[] {new BuildStatus {BuildDefinitionId = "1", BuildStatusEnum = BuildStatusEnum.Working}};
            var newStatuses = new BuildStatus[] {};
            var result = BuildStatusUtil.Merge(oldStatus, newStatuses);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void Merge_ExistingUnchangedBuildStatus_NotOverwritten()
        {
            var oldStatus = new[] { new BuildStatus { BuildDefinitionId = "1", BuildStatusEnum = BuildStatusEnum.Working, LocalStartTime = new DateTime(2010, 1, 1) } };
            var newStatuses = new[] { new BuildStatus { BuildDefinitionId = "1", BuildStatusEnum = BuildStatusEnum.Working, LocalStartTime = new DateTime(2012, 2, 2)} };
            var result = BuildStatusUtil.Merge(oldStatus, newStatuses);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("1", result[0].BuildDefinitionId);
            Assert.AreEqual(BuildStatusEnum.Working, result[0].BuildStatusEnum);
            Assert.AreEqual(new DateTime(2010, 1, 1), result[0].LocalStartTime);
        }
    }
}
