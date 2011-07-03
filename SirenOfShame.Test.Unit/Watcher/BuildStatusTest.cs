using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    [TestClass]
    public class BuildStatusTest
    {
        [TestMethod]
        public void AsBuildStatusListViewItem_InProgressNoPreviousDuration_DurationCountsUp()
        {
            BuildStatus buildStatus = new BuildStatus
            {
                Id = "MyBuild",
                LocalStartTime = new DateTime(2010, 1, 1, 1, 1, 1),
                BuildStatusEnum = BuildStatusEnum.InProgress
            };
            var now = new DateTime(2010, 1, 1, 1, 2, 2);
            var previousWorkingOrBrokenBuildStatus = new Dictionary<string, BuildStatus>();
            var result = buildStatus.AsBuildStatusListViewItem(now, previousWorkingOrBrokenBuildStatus);
            Assert.AreEqual("1:01", result.Duration);
        }
    }
}
