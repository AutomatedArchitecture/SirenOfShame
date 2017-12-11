﻿using System;
using HudsonServices;
using NUnit.Framework;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Test.Unit.Resources;

namespace SirenOfShame.Test.Unit.CIEntryPointBuildStatus
{
    [TestFixture]
    public class HudsonBuildStatusTest
    {
        [Test]
        public void HudsonBuildStatus_WhenTreatUnstableAsSuccessIsTrueAndBuildIsUnstable_ResultSuccessful()
        {
            var jenkinsUnstable = ResourceManager.JenkinsUnstable;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            HudsonBuildStatus buildStatus = new HudsonBuildStatus(jenkinsUnstable, buildDefinitionSetting, null, treatUnstableAsSuccess: true);
            Assert.AreEqual(BuildStatusEnum.Working, buildStatus.BuildStatusEnum);
        }

        [Test]
        public void HudsonBuildStatus_WhenTreatUnstableAsSuccessIsFalseAndBuildIsUnstable_ResultUnsuccessful()
        {
            var jenkinsUnstable = ResourceManager.JenkinsUnstable;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            HudsonBuildStatus buildStatus = new HudsonBuildStatus(jenkinsUnstable, buildDefinitionSetting, null, treatUnstableAsSuccess: false);
            Assert.AreEqual(BuildStatusEnum.Broken, buildStatus.BuildStatusEnum);
        }

        [Test]
        public void HudsonBuildStatus_PassingBuildNoComment()
        {
            var jenkinsBuildStatusForIssue10 = ResourceManager.JenkinsPassingBuild;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            HudsonBuildStatus buildStatus = new HudsonBuildStatus(jenkinsBuildStatusForIssue10, buildDefinitionSetting, null, treatUnstableAsSuccess: false);
            
            Assert.AreEqual(BuildStatusEnum.Working, buildStatus.BuildStatusEnum);
            Assert.AreEqual("BuildDefinitionId", buildStatus.BuildDefinitionId);
            Assert.AreEqual("Name", buildStatus.Name);
            Assert.AreEqual("anonymous", buildStatus.RequestedBy);
            Assert.AreEqual(new DateTime(2011, 12, 23, 21, 8, 21), buildStatus.StartedTime, DateAsCode(buildStatus.StartedTime.Value));
            Assert.AreEqual("Started by user anonymous", buildStatus.Comment);
            Assert.AreEqual(new DateTime(2011, 12, 23, 21, 8, 22, 465), buildStatus.FinishedTime, DateAsCode(buildStatus.FinishedTime.Value)); // timestamp+duration
            Assert.AreEqual("http://win7ci:8081/job/SvnTest/30/", buildStatus.Url);
            Assert.AreEqual("30", buildStatus.BuildId);
        }

        [Test]
        public void HudsonBuildStatus_Issues10_AlwaysUseTimestampIfAvailable()
        {
            var jenkinsBuildStatusForIssue10 = ResourceManager.JenkinsBuildStatusForIssue10;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            HudsonBuildStatus buildStatus = new HudsonBuildStatus(jenkinsBuildStatusForIssue10, buildDefinitionSetting, null, treatUnstableAsSuccess: false);

            Assert.AreEqual(BuildStatusEnum.Working, buildStatus.BuildStatusEnum);
            Assert.AreEqual("BuildDefinitionId", buildStatus.BuildDefinitionId);
            Assert.AreEqual("Name", buildStatus.Name);
            Assert.AreEqual("STCO", buildStatus.RequestedBy);
            Assert.AreEqual(new DateTime(2012, 1, 19, 12, 45, 17, 736), buildStatus.StartedTime, "" + buildStatus.StartedTime.Value.Millisecond);
            Assert.IsNull(buildStatus.Comment);
            Assert.AreEqual(new DateTime(2012, 1, 19, 12, 50, 56, 422), buildStatus.FinishedTime, "" + buildStatus.FinishedTime.Value.Millisecond); // timestamp+duration
            Assert.AreEqual("https://tr-w03.statoil.net:10945/jenkins-prod/view/eBOSS/job/eBOSS/49/", buildStatus.Url);
            Assert.AreEqual("49", buildStatus.BuildId);
        }
        
        [Test]
        public void HudsonBuildStatus_Bug152HudsonDuration()
        {
            var jenkinsBuildStatusForIssue10 = ResourceManager.Bug152HudsonDuration;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            HudsonBuildStatus buildStatus = new HudsonBuildStatus(jenkinsBuildStatusForIssue10, buildDefinitionSetting, null, treatUnstableAsSuccess: false);

            Assert.AreEqual(BuildStatusEnum.Working, buildStatus.BuildStatusEnum);
            Assert.AreEqual(null, buildStatus.RequestedBy);

            // 2/15/2012 5:00:54 PM
            Assert.AreEqual(new DateTime(2012, 2, 15, 17, 0, 54, 361), buildStatus.StartedTime, DateAsCode(buildStatus.StartedTime.Value));
            Assert.IsNull(buildStatus.Comment);
            Assert.AreEqual(new DateTime(2012, 2, 15, 17, 6, 4, 51), buildStatus.FinishedTime, DateAsCode(buildStatus.FinishedTime.Value));
            Assert.AreEqual("277", buildStatus.BuildId);
            Assert.AreEqual("https://tr-w03.statoil.net:10945/jenkins-prod/view/eBOSS/job/eBOSS/49/", buildStatus.Url);
        }

        [Test]
        public void Bug95_JenkinsWithUnicodeCharacters()
        {
            var bug95UnicodeCharacters = ResourceManager.Bug95UnicodeCharacters;
            var buildDefinitionSetting = new BuildDefinitionSetting
            {
                Name = "Name",
                Id = "BuildDefinitionId"
            };
            var buildStatus = new HudsonBuildStatus(bug95UnicodeCharacters, buildDefinitionSetting, null, treatUnstableAsSuccess: false);
            Assert.AreEqual("Keeping it passing from Zoë", buildStatus.Comment);
            Assert.AreEqual("zoë", buildStatus.RequestedBy);
        }

        public static string DateAsCode(DateTime d)
        {
            return string.Format("new DateTime({0}, {1}, {2}, {3}, {4}, {5}, {6})", 
                d.Year,
                d.Month,
                d.Day,
                d.ToString("HH"),
                d.Minute,
                d.Second,
                d.Millisecond
                );
        }
    }
}
