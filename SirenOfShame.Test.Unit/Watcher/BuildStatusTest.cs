// ReSharper disable InconsistentNaming
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
        public void Parse_InvalidDate()
        {
            var actual = BuildStatus.Parse(new [] { "63460976461000000Z", "", "1", "jshimpty" });
            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void Parse_NullEndDate()
        {
            var actual = BuildStatus.Parse(new [] { "634609764610000000", "", "1", "jshimpty" });
            Assert.AreEqual(null, actual.FinishedTime);
        }

        [TestMethod]
        public void Parse_ValidDatesAndStatus()
        {
            var actual = BuildStatus.Parse(new [] { "634609764610000000", "634637449220000000", "1", "jshimpty" });
            Assert.AreEqual(new DateTime(2012, 1, 1, 1, 1, 1), actual.StartedTime);
            Assert.AreEqual(new DateTime(2012, 2, 2, 2, 2, 2), actual.FinishedTime);
            Assert.AreEqual("jshimpty", actual.RequestedBy);
            Assert.AreEqual(BuildStatusEnum.Working, actual.BuildStatusEnum);
        }

        [TestMethod]
        public void IsBackToBack_NineSecondsApart()
        {
            var finishedTime = new DateTime(2010, 1, 1, 1, 1, 1);
            var startedTime = new DateTime(2010, 1, 1, 1, 1, 9);
            Assert.IsTrue(new BuildStatus { FinishedTime = finishedTime}.IsBackToBackWithNextBuild(new BuildStatus {  StartedTime = startedTime}, 10));
        }

        [TestMethod]
        public void IsBackToBack_OlderBuildInsteadOfNewerOne()
        {
            var finishedTime = new DateTime(2010, 1, 1, 1, 1, 9);
            var startedTime = new DateTime(2010, 1, 1, 1, 1, 1);
            Assert.IsFalse(new BuildStatus { FinishedTime = finishedTime}.IsBackToBackWithNextBuild(new BuildStatus {  StartedTime = startedTime}, 10));
        }

        [TestMethod]
        public void IsBackToBack_TenSecondsApart()
        {
            var finishedTime = new DateTime(2010, 1, 1, 1, 1, 1);
            var startedTime = new DateTime(2010, 1, 1, 1, 1, 10);
            Assert.IsTrue(new BuildStatus { FinishedTime = finishedTime}.IsBackToBackWithNextBuild(new BuildStatus {  StartedTime = startedTime}, 10));
        }

        [TestMethod]
        public void IsBackToBack_FinishedTimeNull()
        {
            DateTime? finishedTime = null;
            var startedTime = new DateTime(2010, 1, 1, 1, 1, 10);
            Assert.IsFalse(new BuildStatus { FinishedTime = finishedTime}.IsBackToBackWithNextBuild(new BuildStatus {  StartedTime = startedTime}, 10));
        }

        [TestMethod]
        public void IsBackToBack_StartedTimeNull()
        {
            var finishedTime = new DateTime(2010, 1, 1, 1, 1, 1);
            DateTime? startedTime = null;
            Assert.IsFalse(new BuildStatus { FinishedTime = finishedTime }.IsBackToBackWithNextBuild(new BuildStatus { StartedTime = startedTime }, 10));
        }

        [TestMethod]
        public void AsBuildStatusListViewItem_InProgressNoPreviousDuration_DurationCountsUp()
        {
            BuildStatus buildStatus = new BuildStatus
            {
                BuildDefinitionId = "MyBuild",
                LocalStartTime = new DateTime(2010, 1, 1, 1, 1, 1),
                BuildStatusEnum = BuildStatusEnum.InProgress
            };
            var now = new DateTime(2010, 1, 1, 1, 2, 2);
            var previousWorkingOrBrokenBuildStatus = new Dictionary<string, BuildStatus>();
            var settings = new SirenOfShameSettingsFake();
            var result = buildStatus.AsBuildStatusDto(now, previousWorkingOrBrokenBuildStatus, settings);
            Assert.AreEqual("1:01", result.Duration);
        }
        
        [TestMethod]
        public void AsBuildStatusListViewItem_InProgressPreviousRunOneMinuteBuildJustStarted_DurationCountsDown()
        {
            BuildStatus buildStatus = new BuildStatus
            {
                BuildDefinitionId = "MyBuild",
                LocalStartTime = new DateTime(2010, 1, 1, 1, 1, 1),
                BuildStatusEnum = BuildStatusEnum.InProgress
            };
            var now = new DateTime(2010, 1, 1, 1, 1, 1);
            var previousWorkingOrBrokenBuildStatus = new Dictionary<string, BuildStatus>
            {
                { "MyBuild", new BuildStatus
                {
                    StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), 
                    FinishedTime = new DateTime(2010, 1, 1, 1, 2, 0)
                } } 
            };
            var settings = new SirenOfShameSettingsFake();
            var result = buildStatus.AsBuildStatusDto(now, previousWorkingOrBrokenBuildStatus, settings);
            Assert.AreEqual("1:00", result.Duration); // would have been 0:00 if counting up
        }
        
        [TestMethod]
        public void AsBuildStatusListViewItem_InProgressBuildRunningOverPreviousRunDuration_DurationCountsUpOvertime()
        {
            // current duration: 2 minute, 1 second
            BuildStatus buildStatus = new BuildStatus
            {
                BuildDefinitionId = "MyBuild",
                LocalStartTime = new DateTime(2010, 1, 1, 1, 1, 1),
                BuildStatusEnum = BuildStatusEnum.InProgress
            };
            var now = new DateTime(2010, 1, 1, 1, 3, 2);
            
            // previous duration 1 minute
            var previousWorkingOrBrokenBuildStatus = new Dictionary<string, BuildStatus>
            {
                { "MyBuild", new BuildStatus
                {
                    StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), 
                    FinishedTime = new DateTime(2010, 1, 1, 1, 2, 0)
                } } 
            };
            var settings = new SirenOfShameSettingsFake();
            var result = buildStatus.AsBuildStatusDto(now, previousWorkingOrBrokenBuildStatus, settings);
            Assert.AreEqual("OT: 1:01", result.Duration);
        }
    }
}
