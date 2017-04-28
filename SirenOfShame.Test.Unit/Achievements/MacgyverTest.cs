using System;
using System.Collections.Generic;
using NUnit.Framework;
using SirenOfShame.Lib.Achievements;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestFixture]
    public class MacgyverTest
    {
        [Test]
        public void DecreaseBuildTimeByTenPercent()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 1, 2, 40), CurrentBuildStatus = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 2, 29), CurrentBuildStatus = BuildStatusEnum.Working},
            };
            Assert.AreEqual(100, builds[0].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(89, builds[1].GetDuration().Value.TotalSeconds);
            Assert.IsFalse(new Macgyver(person, builds).HasJustAchieved());
        }
        
        [Test]
        public void DecreaseBuildTimeByFiveteenPercent()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 1, 2, 40), CurrentBuildStatus = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 2, 24), CurrentBuildStatus = BuildStatusEnum.Working},
            };
            Assert.AreEqual(100, builds[0].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(84, builds[1].GetDuration().Value.TotalSeconds);
            Assert.IsTrue(new Macgyver(person, builds).HasJustAchieved());
        }
        
        [Test]
        public void IgnoreNullStartOrEndTimes()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = null, CurrentBuildStatus = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 2, 29), CurrentBuildStatus = BuildStatusEnum.Working},
            };
            Assert.IsFalse(new Macgyver(person, builds).HasJustAchieved());
        }
        
        [Test]
        public void IgnoresCurrentBrokenBuilds()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 1, 2, 40), CurrentBuildStatus = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 2, 29), CurrentBuildStatus = BuildStatusEnum.Broken},
            };
            Assert.AreEqual(100, builds[0].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(89, builds[1].GetDuration().Value.TotalSeconds);
            Assert.IsFalse(new Macgyver(person, builds).HasJustAchieved());
        }
        
        [Test]
        public void IgnoresOldBrokenBuilds()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 1, 2, 40), CurrentBuildStatus = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 5, 00), CurrentBuildStatus = BuildStatusEnum.Broken},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 3, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 3, 2, 40), CurrentBuildStatus = BuildStatusEnum.Working},
            };
            Assert.AreEqual(100, builds[0].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(240, builds[1].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(100, builds[2].GetDuration().Value.TotalSeconds);
            Assert.IsFalse(new Macgyver(person, builds).HasJustAchieved());
        }
        
        [Test]
        public void IncreaseBuildTimeByTenPercent()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 1, 2, 40), CurrentBuildStatus = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 2, 51), CurrentBuildStatus = BuildStatusEnum.Working},
            };
            Assert.AreEqual(100, builds[0].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(111, builds[1].GetDuration().Value.TotalSeconds);
            Assert.IsFalse(new Macgyver(person, builds).HasJustAchieved());
        }
        
        [Test]
        public void DecreaseBuildTimeByNinePercent()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 1, 2, 40), CurrentBuildStatus = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 2, 31), CurrentBuildStatus = BuildStatusEnum.Working},
            };
            Assert.AreEqual(100, builds[0].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(91, builds[1].GetDuration().Value.TotalSeconds);
            Assert.IsFalse(new Macgyver(person, builds).HasJustAchieved());
        }
    }
}
