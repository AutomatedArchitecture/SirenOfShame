using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.StatCalculators;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.StatCalculators
{
    [TestClass]
    public class SuccessInARowTest
    {
        [TestMethod]
        public void CalculateSuccessInARow_FailThenSuccess_OneSuccessInARow()
        {
            IEnumerable<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { BuildDefinitionId = "1", RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Broken },
                new BuildStatus { BuildDefinitionId = "1", RequestedBy = "currentUser", BuildStatusEnum = BuildStatusEnum.Working },
            };
            var actual = SuccessInARow.CalculateSuccessInARow(new PersonSetting {RawName = "currentUser"}, builds);
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void CalculateSuccessInARow_AllSomeoneElse_ZeroSuccessInARow()
        {
            IEnumerable<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { BuildDefinitionId = "1", RequestedBy = "someoneElse", BuildStatusEnum = BuildStatusEnum.Broken },
                new BuildStatus { BuildDefinitionId = "1", RequestedBy = "someoneElse", BuildStatusEnum = BuildStatusEnum.Working },
            };
            var actual = SuccessInARow.CalculateSuccessInARow(new PersonSetting {RawName = "currentUser"}, builds);
            Assert.AreEqual(0, actual);
        }
    }
}
