using System;
using HudsonServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Test.Unit.Resources;

namespace SirenOfShame.Test.Unit.CiEntryPointBuildStatus
{
    [TestClass]
    public class HudsonBuildStatusTest
    {
        [TestMethod]
        public void HudsonBuildStatus_PassingBuildNoComment()
        {
            var jenkinsBuildStatusForIssue10 = ResourceManager.JenkinsPassingBuild;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            HudsonBuildStatus buildStatus = new HudsonBuildStatus(jenkinsBuildStatusForIssue10, buildDefinitionSetting);
            
            Assert.AreEqual(BuildStatusEnum.Working, buildStatus.BuildStatusEnum);
            Assert.AreEqual("BuildDefinitionId", buildStatus.BuildDefinitionId);
            Assert.AreEqual("Name", buildStatus.Name);
            Assert.AreEqual("anonymous", buildStatus.RequestedBy);
            Assert.AreEqual(new DateTime(2011, 12, 23, 21, 8, 21), buildStatus.StartedTime);
            Assert.IsNull(buildStatus.Comment);
            Assert.AreEqual(new DateTime(2011, 12, 23, 21, 8, 22, 465), buildStatus.FinishedTime); // timestamp+duration
        }

        [TestMethod]
        public void HudsonBuildStatus_Issues10_AlwaysUseTimestampIfAvailable()
        {
            var jenkinsBuildStatusForIssue10 = ResourceManager.JenkinsBuildStatusForIssue10;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            HudsonBuildStatus buildStatus = new HudsonBuildStatus(jenkinsBuildStatusForIssue10, buildDefinitionSetting);

            Assert.AreEqual(BuildStatusEnum.Working, buildStatus.BuildStatusEnum);
            Assert.AreEqual("BuildDefinitionId", buildStatus.BuildDefinitionId);
            Assert.AreEqual("Name", buildStatus.Name);
            Assert.AreEqual("STCO", buildStatus.RequestedBy);
            Assert.AreEqual(new DateTime(2012, 1, 19, 12, 45, 17, 736), buildStatus.StartedTime, "" + buildStatus.StartedTime.Value.Millisecond);
            Assert.IsNull(buildStatus.Comment);
            Assert.AreEqual(new DateTime(2012, 1, 19, 12, 50, 56, 422), buildStatus.FinishedTime, "" + buildStatus.FinishedTime.Value.Millisecond); // timestamp+duration
        }
    }
}
