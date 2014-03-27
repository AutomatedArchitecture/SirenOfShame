using System;
using System.Collections.Generic;
using NUnit.Framework;
using SirenOfShame.Lib.Achievements;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestFixture]
    public class LikeLightningTest
    {
        [Test]
        public void WasJustLikeLightning_ElevenSecondsBetweenBuilds()
        {
            var fakePersonSetting = new PersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 2, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 2, 2, 4, 3)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 4, 14), FinishedTime = new DateTime(2010, 2, 2, 2, 5, 14)},
            };
            Assert.AreEqual(false, new LikeLightning(fakePersonSetting, builds).HasJustAchieved());
        }

        [Test]
        public void WasJustLikeLightning_ThreeBackToBackBuilds()
        {
            var fakePersonSetting = new PersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 2, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 2, 2, 4, 3)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 4, 4), FinishedTime = new DateTime(2010, 2, 2, 2, 5, 4)},
            };
            Assert.AreEqual(true, new LikeLightning(fakePersonSetting, builds).HasJustAchieved());
        }

        [Test]
        public void WasJustLikeLightning_ThreeBackToBackBuildsButOneBroken()
        {
            var fakePersonSetting = new PersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 2, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 2, 2, 4, 3)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 4, 4), FinishedTime = new DateTime(2010, 2, 2, 2, 5, 4)},
            };
            Assert.AreEqual(false, new LikeLightning(fakePersonSetting, builds).HasJustAchieved());
        }

        [Test]
        public void WasJustLikeLightning_OneBuildIsMissingFinishedTime()
        {
            var fakePersonSetting = new PersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 2, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 2, 2, 4, 3)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 4, 4), FinishedTime = null},
            };
            Assert.AreEqual(false, new LikeLightning(fakePersonSetting, builds).HasJustAchieved());
        }

        [Test]
        public void WasJustLikeLightning_ZeroBuilds()
        {
            var fakePersonSetting = new PersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
            };
            Assert.AreEqual(false, new LikeLightning(fakePersonSetting, builds).HasJustAchieved());
        }

        [Test]
        public void WasJustLikeLightning_ThreeBuildsOneSecondApartWithDifferentUser()
        {
            var fakePersonSetting = new PersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 2, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "someoneElse", StartedTime = new DateTime(2010, 2, 2, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 2, 2, 4, 3)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 4, 4), FinishedTime = new DateTime(2010, 2, 2, 2, 5, 4)},
            };
            Assert.AreEqual(false, new LikeLightning(fakePersonSetting, builds).HasJustAchieved());
        }
    }
}
