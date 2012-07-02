using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Achievements;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestClass]
    public class CriticalTest
    {
        [TestMethod]
        public void Exactly50BuildsOneFailedLowestPercentageIsTwoPercent()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser"};
            List<BuildStatus> builds = new List<BuildStatus>();
            builds.Add(new BuildStatus { RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Broken });
            for (int i = 0; i < 49; i++)
            {
                builds.Add(new BuildStatus { RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Working });
            }
            Assert.AreEqual(50, builds.Count);
            Assert.AreEqual(0.02, Critical.CalculateLowestBuildRatio(personSetting, builds));
        }
        
        [TestMethod]
        public void Exactly100BuildsOneFailedLowestPercentageIsOnePercent()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser"};
            List<BuildStatus> builds = new List<BuildStatus>();
            builds.Add(new BuildStatus { RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Broken });
            for (int i = 0; i < 99; i++)
            {
                builds.Add(new BuildStatus { RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Working });
            }
            Assert.AreEqual(100, builds.Count);
            Assert.AreEqual(0.01, Critical.CalculateLowestBuildRatio(personSetting, builds));
        }
        
        [TestMethod]
        public void AchievesTwoPercentAt50ThenFails50More_PercentageStaysAtTwo()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser"};
            List<BuildStatus> builds = new List<BuildStatus>();
            builds.Add(new BuildStatus { RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Broken });
            for (int i = 0; i < 49; i++)
            {
                builds.Add(new BuildStatus { RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Working });
            }
            for (int i = 0; i < 50; i++)
            {
                builds.Add(new BuildStatus { RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Broken });
            }
            Assert.AreEqual(100, builds.Count);
            Assert.AreEqual(0.02, Critical.CalculateLowestBuildRatio(personSetting, builds));
        }
        
        [TestMethod]
        public void Exactly50BuildsOneFailed_ButFailedBuildIsSomeoneElse()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser"};
            List<BuildStatus> builds = new List<BuildStatus>();
            builds.Add(new BuildStatus { RequestedBy = "someoneElse", BuildStatusEnum = BuildStatusEnum.Broken });
            for (int i = 0; i < 49; i++)
            {
                builds.Add(new BuildStatus { RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Working });
            }
            Assert.AreEqual(50, builds.Count);
            Assert.IsNull(Critical.CalculateLowestBuildRatio(personSetting, builds));
        }
    }
}
