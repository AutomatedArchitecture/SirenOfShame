using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Settings
{
    public class FakePersonSetting : PersonSetting
    {
        public int FakeHowManyTimesHasFixedSomeoneElsesBuild(List<BuildStatus> currentBuildDefinitionOrderedChronoligically)
        {
            return HowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically);
        }
    }

    [TestClass]
    public class PersonSettingTest
    {
        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_NoBuilds_Zero()
        {
            var fakePersonSetting = new FakePersonSetting
            {
                RawName = "currentUser"
            };
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>();
            Assert.AreEqual(0, fakePersonSetting.FakeHowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically));
        }
        
        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_FixedSomeoneElsesBuild_One()
        {
            var fakePersonSetting = new FakePersonSetting
            {
                RawName = "currentUser"
            };
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "someoneElse" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" }
            };
            Assert.AreEqual(1, fakePersonSetting.FakeHowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically));
        }
        
        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_FixedOwnBuild_Zero()
        {
            var fakePersonSetting = new FakePersonSetting
            {
                RawName = "currentUser"
            };
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" }
            };
            Assert.AreEqual(0, fakePersonSetting.FakeHowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically));
        }
        
        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_SomoeneElseFixedMyBuild_Zero()
        {
            var fakePersonSetting = new FakePersonSetting
            {
                RawName = "currentUser"
            };
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "someoneElse" }
            };
            Assert.AreEqual(0, fakePersonSetting.FakeHowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically));
        }

        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_FixedSomeoneElsesBuildThenBuildAgain_OnlyOne()
        {
            var fakePersonSetting = new FakePersonSetting
            {
                RawName = "currentUser"
            };
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "someoneElse" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" },
            };
            Assert.AreEqual(1, fakePersonSetting.FakeHowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically));
        }

    }
}
