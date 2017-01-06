using System.Collections.Generic;
using NUnit.Framework;
using SirenOfShame.Lib.Achievements;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestFixture]
    public class ReputationReboundTest
    {
        [Test]
        public void ThreeFailedThenTwelvePass()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser"};
            List<BuildStatus> buildStatuses = new List<BuildStatus>
            {
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Broken, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Broken, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Broken, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
            };
            Assert.AreEqual(true, new ReputationRebound(personSetting, buildStatuses).HasJustAchieved());
        }
        
        [Test]
        public void TwoFailedThenTwelvePass()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser"};
            List<BuildStatus> buildStatuses = new List<BuildStatus>
            {
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Broken, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Broken, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
            };
            Assert.AreEqual(false, new ReputationRebound(personSetting, buildStatuses).HasJustAchieved());
        }
        
        [Test]
        public void ThreeFailedThenElevenPass()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser"};
            List<BuildStatus> buildStatuses = new List<BuildStatus>
            {
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Broken, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Broken, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Broken, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
            };
            Assert.AreEqual(false, new ReputationRebound(personSetting, buildStatuses).HasJustAchieved());
        }
        
        [Test]
        public void IgnoresPassesByOtherPeople()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser"};
            List<BuildStatus> buildStatuses = new List<BuildStatus>
            {
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Broken, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Broken, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Broken, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "currentUser"},
                new BuildStatus { CurrentBuildStatus = BuildStatusEnum.Working, RequestedBy = "someoneElse"},
            };
            Assert.AreEqual(false, new ReputationRebound(personSetting, buildStatuses).HasJustAchieved());
        }
    }
}
