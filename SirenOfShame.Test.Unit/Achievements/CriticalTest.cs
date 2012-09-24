using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Achievements;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.StatCalculators;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestClass]
    public class CriticalTest
    {
        [TestMethod]
        public void CurrentBuildRatio()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser" };
            List<BuildStatus> builds = new List<BuildStatus>();
            builds.Add(new BuildStatus { RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Broken });
            builds.Add(new BuildStatus { RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Working });
            builds.Add(new BuildStatus { RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Working });
            builds.Add(new BuildStatus { RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Working });
            builds.Add(new BuildStatus { RequestedBy = "someoneElse", BuildStatusEnum = BuildStatusEnum.Working });
            Assert.AreEqual(0.25, BuildRatio.CalculateCurrentBuildRatio(personSetting, builds));
        }

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
            Assert.AreEqual(0.02, BuildRatio.CalculateLowestBuildRatioAfter50Builds(personSetting, builds));
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
            Assert.AreEqual(0.01, BuildRatio.CalculateLowestBuildRatioAfter50Builds(personSetting, builds));
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
            Assert.AreEqual(0.02, BuildRatio.CalculateLowestBuildRatioAfter50Builds(personSetting, builds));
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
            Assert.IsNull(BuildRatio.CalculateLowestBuildRatioAfter50Builds(personSetting, builds));
        }
    }
}
