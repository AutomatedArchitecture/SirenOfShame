using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Test.Unit.CiEntryPointBuildStatus;
using SirenOfShame.Test.Unit.Resources;
using TeamCityServices;

namespace SirenOfShame.Test.Unit.CIEntryPointBuildStatus
{
    [TestClass]
    public class TeamCityBuildStatusTest
    {
        [TestMethod]
        public void HudsonBuildStatus_PassingBuildNoComment()
        {
            var teamCityFailingBuild = ResourceManager.TeamCityFailingBuild;
            var teamCityFailingChange = ResourceManager.TeamCityFailingChange;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            var buildStatus = new TeamCityBuildStatus(buildDefinitionSetting, teamCityFailingBuild, teamCityFailingChange);

            Assert.AreEqual(BuildStatusEnum.Broken, buildStatus.BuildStatusEnum);
            Assert.AreEqual("BuildDefinitionId", buildStatus.BuildDefinitionId);
            Assert.AreEqual("BuildConfig4", buildStatus.Name);
            Assert.AreEqual("lee", buildStatus.RequestedBy);
            Assert.AreEqual(new DateTime(2012, 1, 27, 0, 15, 45, 0), buildStatus.StartedTime, HudsonBuildStatusTest.DateAsCode(buildStatus.StartedTime.Value));
            string expectedComment = @"Merge branch 'master' of C:\dev\CiTest

Conflicts:
	CiTest/Program.cs".Replace("\r", "");
            Assert.AreEqual(expectedComment.Replace("\r\n", "\r"), buildStatus.Comment);
            Assert.AreEqual(new DateTime(2012, 1, 27, 0, 16, 2, 0), buildStatus.FinishedTime, HudsonBuildStatusTest.DateAsCode(buildStatus.FinishedTime.Value)); // timestamp+duration
            Assert.AreEqual("http://win7ci:8080/viewLog.html?buildId=35&buildTypeId=bt2", buildStatus.Url);
            Assert.AreEqual(35, buildStatus.BuildId);
        }
    }
}
