using System.Collections.Generic;
using NUnit.Framework;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.StatCalculators;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.StatCalculators
{
    [TestFixture]
    public class SuccessInARowTest
    {
        [Test]
        public void CalculateSuccessInARow_FailThenSuccess_OneSuccessInARow()
        {
            IEnumerable<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { BuildDefinitionId = "1", RequestedBy = "currentUser", CurrentBuildStatus = BuildStatusEnum.Broken },
                new BuildStatus { BuildDefinitionId = "1", RequestedBy = "currentUser", CurrentBuildStatus = BuildStatusEnum.Working },
            };
            var actual = SuccessInARow.CalculateSuccessInARow(new PersonSetting {RawName = "currentUser"}, builds);
            Assert.AreEqual(1, actual);
        }

        [Test]
        public void CalculateSuccessInARow_AllSomeoneElse_ZeroSuccessInARow()
        {
            IEnumerable<BuildStatus> builds = new List<BuildStatus>
            {
                new BuildStatus { BuildDefinitionId = "1", RequestedBy = "someoneElse", CurrentBuildStatus = BuildStatusEnum.Broken },
                new BuildStatus { BuildDefinitionId = "1", RequestedBy = "someoneElse", CurrentBuildStatus = BuildStatusEnum.Working },
            };
            var actual = SuccessInARow.CalculateSuccessInARow(new PersonSetting {RawName = "currentUser"}, builds);
            Assert.AreEqual(0, actual);
        }
    }
}
