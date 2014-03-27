using System;
using System.Collections.Generic;
using NUnit.Framework;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.StatCalculators;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestFixture]
    public class InTheZoneTest
    {
        [Test]
        public void OneBuildEachDay_MaxIsOne()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser" };
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { RequestedBy = "currentUser", StartedTime = new DateTime(2010, 1, 1, 1, 1, 1)},
                new BuildStatus { RequestedBy = "currentUser", StartedTime = new DateTime(2010, 1, 2, 1, 1, 1)},
            };
            var max = MaxBuildsInOneDay.GetMaxBuildsInOneDay(personSetting, builds);
            Assert.AreEqual(1, max);
        }
        
        [Test]
        public void TwoBuildsInADayBySomeoneElse_MaxIsOne()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser" };
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { RequestedBy = "someoneElse", StartedTime = new DateTime(2010, 1, 1, 1, 1, 1)},
                new BuildStatus { RequestedBy = "someoneElse", StartedTime = new DateTime(2010, 1, 1, 1, 5, 1)},
                new BuildStatus { RequestedBy = "currentUser", StartedTime = new DateTime(2010, 1, 2, 1, 1, 1)},
            };
            var max = MaxBuildsInOneDay.GetMaxBuildsInOneDay(personSetting, builds);
            Assert.AreEqual(1, max);
        }
        
        [Test]
        public void TwoBuildsInADayByCurrentUser_MaxIsTwo()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser" };
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { RequestedBy = "currentUser", StartedTime = new DateTime(2010, 1, 1, 1, 1, 1)},
                new BuildStatus { RequestedBy = "currentUser", StartedTime = new DateTime(2010, 1, 1, 1, 5, 1)},
                new BuildStatus { RequestedBy = "currentUser", StartedTime = new DateTime(2010, 1, 2, 1, 1, 1)},
            };
            var max = MaxBuildsInOneDay.GetMaxBuildsInOneDay(personSetting, builds);
            Assert.AreEqual(2, max);
        }
        
        [Test]
        public void ExcludesNullDates()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser" };
            List<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { RequestedBy = "currentUser", StartedTime = null},
            };
            var max = MaxBuildsInOneDay.GetMaxBuildsInOneDay(personSetting, builds);
            Assert.AreEqual(0, max);
        }
    }
}
