using System;
using System.Collections.Generic;
using NUnit.Framework;
using SirenOfShame.Lib.Achievements;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestFixture]
    public class AndGotAwayWithItTest
    {
        [Test]
        public void TwoBuildsWithin59Seconds_BrokenThenFixed_BothByCurrentUser()
        {
            PersonSetting personSetting = new PersonSetting {RawName = "currentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { RequestedBy = "currentUser", FinishedTime = new DateTime(2010, 1, 1, 1, 1, 1), CurrentBuildStatus = BuildStatusEnum.Broken },
                new BuildStatus { RequestedBy = "currentUser", StartedTime = new DateTime(2010, 1, 1, 1, 1, 59), CurrentBuildStatus = BuildStatusEnum.Working },
            };
            Assert.IsTrue(new AndGotAwayWithIt(personSetting, builds).HasJustAchieved());
        }

        [Test]
        public void TwoBuildsWithin59Seconds_BrokenThenFixed_ButByDifferentUsers()
        {
            PersonSetting personSetting = new PersonSetting {RawName = "currentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { RequestedBy = "someoneElse", FinishedTime = new DateTime(2010, 1, 1, 1, 1, 1), CurrentBuildStatus = BuildStatusEnum.Broken },
                new BuildStatus { RequestedBy = "currentUser", StartedTime = new DateTime(2010, 1, 1, 1, 1, 59), CurrentBuildStatus = BuildStatusEnum.Working },
            };
            Assert.IsFalse(new AndGotAwayWithIt(personSetting, builds).HasJustAchieved());
        }

        [Test]
        public void TwoBuildsWithin59Seconds_BothByCurrentUser_ButBrokenThenBroken_()
        {
            PersonSetting personSetting = new PersonSetting {RawName = "currentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { RequestedBy = "currentUser", FinishedTime = new DateTime(2010, 1, 1, 1, 1, 1), CurrentBuildStatus = BuildStatusEnum.Broken },
                new BuildStatus { RequestedBy = "currentUser", StartedTime = new DateTime(2010, 1, 1, 1, 1, 59), CurrentBuildStatus = BuildStatusEnum.Broken },
            };
            Assert.IsFalse(new AndGotAwayWithIt(personSetting, builds).HasJustAchieved());
        }
        
        [Test]
        public void BrokenThenFixed_BothByCurrentUser_ButWithin60Seconds_()
        {
            PersonSetting personSetting = new PersonSetting {RawName = "currentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { RequestedBy = "currentUser", FinishedTime = new DateTime(2010, 1, 1, 1, 1, 1), CurrentBuildStatus = BuildStatusEnum.Broken },
                new BuildStatus { RequestedBy = "currentUser", StartedTime = new DateTime(2010, 1, 1, 1, 2, 1), CurrentBuildStatus = BuildStatusEnum.Working },
            };
            Assert.IsFalse(new AndGotAwayWithIt(personSetting, builds).HasJustAchieved());
        }
        
        [Test]
        public void NullStartAndEndTimes()
        {
            PersonSetting personSetting = new PersonSetting {RawName = "currentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { RequestedBy = "currentUser", CurrentBuildStatus = BuildStatusEnum.Broken },
                new BuildStatus { RequestedBy = "currentUser", CurrentBuildStatus = BuildStatusEnum.Working },
            };
            Assert.IsFalse(new AndGotAwayWithIt(personSetting, builds).HasJustAchieved());
        }
        
        [Test]
        public void OnlyOneBuild()
        {
            PersonSetting personSetting = new PersonSetting {RawName = "currentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { RequestedBy = "currentUser", StartedTime = new DateTime(2010, 1, 1, 1, 1, 59), CurrentBuildStatus = BuildStatusEnum.Working },
            };
            Assert.IsFalse(new AndGotAwayWithIt(personSetting, builds).HasJustAchieved());
        }
    }
}
