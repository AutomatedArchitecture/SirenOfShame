using System;
using System.Linq;
using NUnit.Framework;
using SirenOfShame.Lib.Util;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Util
{
    [TestFixture]
    public class BuildStatusUtilTest
    {
        [Test]
        public void GivenOneOldStatus_WhenTwoNewBuildsExistWithSameBuildDefinitionId_ThenNewestIsReturned()
        {
            var oldStatus = new[] { new BuildStatus { BuildDefinitionId = "BD1", BuildId = "B1", BuildStatusEnum = BuildStatusEnum.Working } };
            var newStatuses = new[]
            {
                new BuildStatus {BuildDefinitionId = "BD1", BuildId = "B2", BuildStatusEnum = BuildStatusEnum.InProgress, StartedTime = new DateTime(2018, 1, 1, 2, 2, 3) },
                new BuildStatus {BuildDefinitionId = "BD1", BuildId = "B3", BuildStatusEnum = BuildStatusEnum.InProgress, StartedTime = new DateTime(2018, 1, 1, 2, 2, 2)}
            };
            var buildStatuses = BuildStatusUtil.Merge(oldStatus, newStatuses);
            Assert.AreEqual(1, buildStatuses.Length);
            var buildStatuse = buildStatuses[0];
            Assert.AreEqual("B2", buildStatuse.BuildId);
        }

        [Test]
        public void GivenEmptyOldStatus_WhenNewBuildStatusExists_ThenNewBuildStatusIsAdded()
        {
            var oldStatus = new BuildStatus[] {};
            var newStatuses = new[] {new BuildStatus {BuildDefinitionId = "1", BuildStatusEnum = BuildStatusEnum.Working}};
            var result = BuildStatusUtil.Merge(oldStatus, newStatuses);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("1", result[0].BuildDefinitionId);
            Assert.AreEqual(BuildStatusEnum.Working, result[0].BuildStatusEnum);
        }
        
        [Test]
        public void GivenAnOldBuildStatus_WhenEmptyNewBuildStatusMerged_ThenOldBuildStatusIsRetained()
        {
            var oldStatus = new[] {new BuildStatus {BuildDefinitionId = "1", BuildStatusEnum = BuildStatusEnum.Working}};
            var newStatuses = new BuildStatus[] {};
            var result = BuildStatusUtil.Merge(oldStatus, newStatuses);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void GivenOldBuildStatus_WhenBuildStatusWithSameIdIsMerged_ThenOldBuildStatusIsNotOverwritten()
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
