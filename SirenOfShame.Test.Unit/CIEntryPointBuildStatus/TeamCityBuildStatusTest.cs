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
        public void TeamCityBuildStatus_PassingBuildNoComment()
        {
            var teamCityFailingBuild = ResourceManager.TeamCityFailingBuild;
            var teamCityFailingChange = ResourceManager.TeamCityFailingChange;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting
                                                                {
                                                                    Name = "Name",
                                                                    Id = "BuildDefinitionId"
                                                                };
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
            Assert.AreEqual("35", buildStatus.BuildId);
        }

        [TestMethod]
        public void TeamCityFailureDueToCleanup()
        {
            var teamCityFailureDueToCleanup = ResourceManager.TeamCityFailureDueToCleanup;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting
                                                                {
                                                                    Name = "Name",
                                                                    Id = "BuildDefinitionId"
                                                                };
            var buildStatus = new TeamCityBuildStatus(buildDefinitionSetting, teamCityFailureDueToCleanup, null);

            Assert.AreEqual(BuildStatusEnum.Unknown, buildStatus.BuildStatusEnum);
            Assert.AreEqual("BuildDefinitionId", buildStatus.BuildDefinitionId);
            Assert.AreEqual("db_maintenance Database [GRAVIS1]", buildStatus.Name);
            Assert.AreEqual(null, buildStatus.RequestedBy);
            Assert.AreEqual(new DateTime(2012, 5, 29, 21, 0, 7, 0), buildStatus.StartedTime, HudsonBuildStatusTest.DateAsCode(buildStatus.StartedTime.Value));
            Assert.AreEqual(null, buildStatus.Comment);
            Assert.AreEqual(new DateTime(2012, 5, 29, 21, 0, 12, 0), buildStatus.FinishedTime, HudsonBuildStatusTest.DateAsCode(buildStatus.FinishedTime.Value)); // timestamp+duration
            Assert.AreEqual("http://teamcity.com/viewLog.html?buildId=32470&buildTypeId=bt231", buildStatus.Url);
            Assert.AreEqual("32470", buildStatus.BuildId);
        }
    }
}
