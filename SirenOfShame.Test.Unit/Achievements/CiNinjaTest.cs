using System.Collections.Generic;
using NUnit.Framework;
using SirenOfShame.Lib.StatCalculators;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestFixture]
    public class CiNinjaTest
    {
        [Test]
        public void AcrossBuilds_BrokenBuildInProjectOneAndFixedInProjectOne_Fixed()
        {
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, BuildDefinitionId = "1", RequestedBy = "someoneElse" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, BuildDefinitionId = "1", RequestedBy = "currentUser" }
            };
            Assert.AreEqual(1, FixedSomeoneElsesBuild.HowManyTimesFixedSomeoneElsesBuildForAllBuilds(currentBuildDefinitionOrderedChronoligically, "currentUser"));
        }

        [Test]
        public void AcrossBuilds_BrokenBuildInProjectOneAndFixedInProjectTwo_NotFixed()
        {
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, BuildDefinitionId = "1", RequestedBy = "someoneElse" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, BuildDefinitionId = "2", RequestedBy = "currentUser" }
            };
            Assert.AreEqual(0, FixedSomeoneElsesBuild.HowManyTimesFixedSomeoneElsesBuildForAllBuilds(currentBuildDefinitionOrderedChronoligically, "currentUser"));
        }

        [Test]
        public void HowManyTimesHasFixedSomeoneElsesBuild_NoBuilds_Zero()
        {
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>();
            Assert.AreEqual(0, FixedSomeoneElsesBuild.HowManyTimesHasFixedSomeoneElsesBuildForBuild(currentBuildDefinitionOrderedChronoligically, "currentUser"));
        }

        [Test]
        public void HowManyTimesHasFixedSomeoneElsesBuild_FixedSomeoneElsesBuild_One()
        {
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "someoneElse" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" }
            };
            Assert.AreEqual(1, FixedSomeoneElsesBuild.HowManyTimesHasFixedSomeoneElsesBuildForBuild(currentBuildDefinitionOrderedChronoligically, "currentUser"));
        }

        [Test]
        public void HowManyTimesHasFixedSomeoneElsesBuild_FixedOwnBuild_Zero()
        {
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" }
            };
            Assert.AreEqual(0, FixedSomeoneElsesBuild.HowManyTimesHasFixedSomeoneElsesBuildForBuild(currentBuildDefinitionOrderedChronoligically, "currentUser"));
        }

        [Test]
        public void HowManyTimesHasFixedSomeoneElsesBuild_SomoeneElseFixedMyBuild_Zero()
        {
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "someoneElse" }
            };
            Assert.AreEqual(0, FixedSomeoneElsesBuild.HowManyTimesHasFixedSomeoneElsesBuildForBuild(currentBuildDefinitionOrderedChronoligically, "currentUser"));
        }

        [Test]
        public void HowManyTimesHasFixedSomeoneElsesBuild_FixedSomeoneElsesBuildThenBuildAgain_OnlyOne()
        {
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "someoneElse" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" },
            };
            Assert.AreEqual(1, FixedSomeoneElsesBuild.HowManyTimesHasFixedSomeoneElsesBuildForBuild(currentBuildDefinitionOrderedChronoligically, "currentUser"));
        }
    }
}
