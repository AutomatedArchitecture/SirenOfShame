using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Achievements;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestClass]
    public class MacgyverTest
    {
        [TestMethod]
        public void DecreaseBuildTimeByTenPercent()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 1, 2, 40), BuildStatusEnum = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 2, 29), BuildStatusEnum = BuildStatusEnum.Working},
            };
            Assert.AreEqual(100, builds[0].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(89, builds[1].GetDuration().Value.TotalSeconds);
            Assert.IsFalse(new Macgyver(person, builds).HasJustAchieved());
        }
        
        [TestMethod]
        public void DecreaseBuildTimeByFiveteenPercent()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 1, 2, 40), BuildStatusEnum = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 2, 24), BuildStatusEnum = BuildStatusEnum.Working},
            };
            Assert.AreEqual(100, builds[0].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(84, builds[1].GetDuration().Value.TotalSeconds);
            Assert.IsTrue(new Macgyver(person, builds).HasJustAchieved());
        }
        
        [TestMethod]
        public void IgnoreNullStartOrEndTimes()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = null, BuildStatusEnum = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 2, 29), BuildStatusEnum = BuildStatusEnum.Working},
            };
            Assert.IsFalse(new Macgyver(person, builds).HasJustAchieved());
        }
        
        [TestMethod]
        public void IgnoresCurrentBrokenBuilds()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 1, 2, 40), BuildStatusEnum = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 2, 29), BuildStatusEnum = BuildStatusEnum.Broken},
            };
            Assert.AreEqual(100, builds[0].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(89, builds[1].GetDuration().Value.TotalSeconds);
            Assert.IsFalse(new Macgyver(person, builds).HasJustAchieved());
        }
        
        [TestMethod]
        public void IgnoresOldBrokenBuilds()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 1, 2, 40), BuildStatusEnum = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 5, 00), BuildStatusEnum = BuildStatusEnum.Broken},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 3, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 3, 2, 40), BuildStatusEnum = BuildStatusEnum.Working},
            };
            Assert.AreEqual(100, builds[0].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(240, builds[1].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(100, builds[2].GetDuration().Value.TotalSeconds);
            Assert.IsFalse(new Macgyver(person, builds).HasJustAchieved());
        }
        
        [TestMethod]
        public void IncreaseBuildTimeByTenPercent()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 1, 2, 40), BuildStatusEnum = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 2, 51), BuildStatusEnum = BuildStatusEnum.Working},
            };
            Assert.AreEqual(100, builds[0].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(111, builds[1].GetDuration().Value.TotalSeconds);
            Assert.IsFalse(new Macgyver(person, builds).HasJustAchieved());
        }
        
        [TestMethod]
        public void DecreaseBuildTimeByNinePercent()
        {
            PersonSetting person = new PersonSetting {RawName = "CurrentUser"};
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 1, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 1, 2, 40), BuildStatusEnum = BuildStatusEnum.Working},
                new BuildStatus { StartedTime = new DateTime(2010, 1, 1, 2, 1, 0), FinishedTime = new DateTime(2010, 1, 1, 2, 2, 31), BuildStatusEnum = BuildStatusEnum.Working},
            };
            Assert.AreEqual(100, builds[0].GetDuration().Value.TotalSeconds);
            Assert.AreEqual(91, builds[1].GetDuration().Value.TotalSeconds);
            Assert.IsFalse(new Macgyver(person, builds).HasJustAchieved());
        }
    }
}
