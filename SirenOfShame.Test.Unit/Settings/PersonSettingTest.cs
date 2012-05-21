using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Settings
{
    public class FakePersonSetting : PersonSetting
    {
        public int HowManyTimesHasFixedSomeoneElsesBuildFake(List<BuildStatus> currentBuildDefinitionOrderedChronoligically)
        {
            return HowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically);
        }

        public bool WasJustLikeLightningFake(List<BuildStatus> currentBuildDefinitionOrderedChronoligically)
        {
            return WasJustLikeLightning(currentBuildDefinitionOrderedChronoligically);
        }
    }

    [TestClass]
    public class PersonSettingTest
    {
        [TestMethod]
        public void WasJustLikeLightning_ElevenSecondsBetweenBuilds()
        {
            var fakePersonSetting = new FakePersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 2, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 2, 2, 4, 3)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 4, 14), FinishedTime = new DateTime(2010, 2, 2, 2, 5, 14)},
            };
            Assert.AreEqual(false, fakePersonSetting.WasJustLikeLightningFake(builds));
        }

        [TestMethod]
        public void WasJustLikeLightning_ThreeBackToBackBuilds()
        {
            var fakePersonSetting = new FakePersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 2, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 2, 2, 4, 3)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 4, 4), FinishedTime = new DateTime(2010, 2, 2, 2, 5, 4)},
            };
            Assert.AreEqual(true, fakePersonSetting.WasJustLikeLightningFake(builds));
        }

        [TestMethod]
        public void WasJustLikeLightning_OneBuildIsMissingFinishedTime()
        {
            var fakePersonSetting = new FakePersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 2, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 2, 2, 4, 3)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 4, 4), FinishedTime = null},
            };
            Assert.AreEqual(false, fakePersonSetting.WasJustLikeLightningFake(builds));
        }

        [TestMethod]
        public void WasJustLikeLightning_ZeroBuilds()
        {
            var fakePersonSetting = new FakePersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
            };
            Assert.AreEqual(false, fakePersonSetting.WasJustLikeLightningFake(builds));
        }

        [TestMethod]
        public void WasJustLikeLightning_ThreeBuildsOneSecondApartWithDifferentUser()
        {
            var fakePersonSetting = new FakePersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 2, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "someoneElse", StartedTime = new DateTime(2010, 2, 2, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 2, 2, 4, 3)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 4, 4), FinishedTime = new DateTime(2010, 2, 2, 2, 5, 4)},
            };
            Assert.AreEqual(false, fakePersonSetting.WasJustLikeLightningFake(builds));
        }

        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_NoBuilds_Zero()
        {
            var fakePersonSetting = new FakePersonSetting { RawName = "currentUser" };
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>();
            Assert.AreEqual(0, fakePersonSetting.HowManyTimesHasFixedSomeoneElsesBuildFake(currentBuildDefinitionOrderedChronoligically));
        }
        
        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_FixedSomeoneElsesBuild_One()
        {
            var fakePersonSetting = new FakePersonSetting { RawName = "currentUser" };
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "someoneElse" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" }
            };
            Assert.AreEqual(1, fakePersonSetting.HowManyTimesHasFixedSomeoneElsesBuildFake(currentBuildDefinitionOrderedChronoligically));
        }
        
        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_FixedOwnBuild_Zero()
        {
            var fakePersonSetting = new FakePersonSetting { RawName = "currentUser" };
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" }
            };
            Assert.AreEqual(0, fakePersonSetting.HowManyTimesHasFixedSomeoneElsesBuildFake(currentBuildDefinitionOrderedChronoligically));
        }
        
        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_SomoeneElseFixedMyBuild_Zero()
        {
            var fakePersonSetting = new FakePersonSetting { RawName = "currentUser" };
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "someoneElse" }
            };
            Assert.AreEqual(0, fakePersonSetting.HowManyTimesHasFixedSomeoneElsesBuildFake(currentBuildDefinitionOrderedChronoligically));
        }

        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_FixedSomeoneElsesBuildThenBuildAgain_OnlyOne()
        {
            var fakePersonSetting = new FakePersonSetting { RawName = "currentUser" };
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "someoneElse" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" },
            };
            Assert.AreEqual(1, fakePersonSetting.HowManyTimesHasFixedSomeoneElsesBuildFake(currentBuildDefinitionOrderedChronoligically));
        }

    }
}
