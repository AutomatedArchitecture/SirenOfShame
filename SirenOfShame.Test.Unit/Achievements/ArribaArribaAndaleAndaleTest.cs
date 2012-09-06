using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.StatCalculators;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestClass]
    public class ArribaArribaAndaleAndaleTest
    {
        [TestMethod]
        public void BackToBackThreeTimes()
        {
            var fakePersonSetting = new PersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 2, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 2, 2, 4, 3)},
                
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 3, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 3, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 3, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 3, 2, 4, 3)},
                
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 4, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 4, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 4, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 4, 2, 4, 3)},
            };
            Assert.AreEqual(3, BackToBackBuilds.HowManyTimesHasPerformedBackToBackBuilds(fakePersonSetting, builds));
        }
        
        [TestMethod]
        public void BackToBackExcludesOtherUsers()
        {
            var fakePersonSetting = new PersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 2, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "someoneElse", StartedTime = new DateTime(2010, 2, 2, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 2, 2, 4, 3)},
            };
            Assert.AreEqual(0, BackToBackBuilds.HowManyTimesHasPerformedBackToBackBuilds(fakePersonSetting, builds));
        }
        
        [TestMethod]
        public void BackToBackExcludesBrokenBuilds()
        {
            var fakePersonSetting = new PersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 2, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 3, 3), FinishedTime = new DateTime(2010, 2, 2, 2, 4, 3)},
            };
            Assert.AreEqual(0, BackToBackBuilds.HowManyTimesHasPerformedBackToBackBuilds(fakePersonSetting, builds));
        }
        
        [TestMethod]
        public void BuildsArentBackToBack()
        {
            var fakePersonSetting = new PersonSetting { RawName = "currentUser" };
            var builds = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 2, 2, 2), FinishedTime = new DateTime(2010, 2, 2, 2, 3, 2)},
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser", StartedTime = new DateTime(2010, 2, 2, 3, 3, 3), FinishedTime = new DateTime(2010, 2, 2, 3, 4, 3)},
            };
            Assert.AreEqual(0, BackToBackBuilds.HowManyTimesHasPerformedBackToBackBuilds(fakePersonSetting, builds));
        }
    }
}
