using System;
using BambooServices;
using HudsonServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Test.Unit.CiEntryPointBuildStatus;
using SirenOfShame.Test.Unit.Resources;

namespace SirenOfShame.Test.Unit.CIEntryPointBuildStatus
{
    [TestClass]
    public class BambooBuildStatusTest
    {
        [TestMethod]
        public void BambooBuildStatus_FailedWithComment()
        {
            var bambooFailingBuild = ResourceManager.BambooFailingBuild;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            var buildStatus = BambooBuildStatus.CreateBuildResult(bambooFailingBuild, buildDefinitionSetting, "http://win7ci:8085");

            Assert.AreEqual(BuildStatusEnum.Broken, buildStatus.BuildStatusEnum);
            Assert.AreEqual("BuildDefinitionId", buildStatus.BuildDefinitionId);
            Assert.AreEqual("Name", buildStatus.Name);
            Assert.AreEqual("Lee", buildStatus.RequestedBy);
            Assert.AreEqual(new DateTime(2012, 3, 3, 19, 4, 23, 98), buildStatus.StartedTime, HudsonBuildStatusTest.DateAsCode(buildStatus.StartedTime.Value));
            Assert.AreEqual("breaking the build", buildStatus.Comment);
            Assert.AreEqual(new DateTime(2012, 3, 3, 19, 4, 27, 61), buildStatus.FinishedTime, HudsonBuildStatusTest.DateAsCode(buildStatus.FinishedTime.Value)); // timestamp+duration
            Assert.AreEqual("2", buildStatus.BuildId);
            Assert.AreEqual("http://win7ci:8085/browse/CITEST-CITESTPLANKEY-2", buildStatus.Url);
        }
    }
}
