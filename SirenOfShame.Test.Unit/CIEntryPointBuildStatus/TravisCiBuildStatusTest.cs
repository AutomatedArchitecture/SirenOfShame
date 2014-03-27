using NUnit.Framework;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Test.Unit.Resources;
using TravisCiServices;

namespace SirenOfShame.Test.Unit.CIEntryPointBuildStatus
{
    [TestFixture]
    public class TravisCiBuildStatusTest
    {
        [Test]
        public void TravisBuildStatus_PassingBuildNoComment()
        {
            var travisCiWorkingBuild = ResourceManager.TravisCiWorkingBuild;
            var travisCiBuildDefinition = TravisCiBuildDefinition.FromIdString("ownerName/projectName/59");
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            TravisCiBuildStatus buildStatus = new TravisCiBuildStatus(travisCiBuildDefinition, travisCiWorkingBuild, buildDefinitionSetting);

            Assert.AreEqual(BuildStatusEnum.Working, buildStatus.BuildStatusEnum);
            Assert.AreEqual("BuildDefinitionId", buildStatus.BuildDefinitionId);
            Assert.AreEqual("Name", buildStatus.Name);
            Assert.AreEqual("Bob Smith", buildStatus.RequestedBy);
            Assert.AreEqual("Merge branch 'master' of https://github.com/travis-ci/travis-ci", buildStatus.Comment);
            Assert.AreEqual("https://api.travis-ci.org/ownerName/projectName/builds/1591278", buildStatus.Url);
            Assert.AreEqual("1591278", buildStatus.BuildId);
        }
        
        [Test]
        public void TravisBuildStatus_FunkyDate()
        {
            var travisCiWorkingBuild = ResourceManager.TravisFunkyDate;
            var travisCiBuildDefinition = TravisCiBuildDefinition.FromIdString("ownerName/projectName/59");
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            TravisCiBuildStatus buildStatus = new TravisCiBuildStatus(travisCiBuildDefinition, travisCiWorkingBuild, buildDefinitionSetting);

            Assert.AreEqual(BuildStatusEnum.InProgress, buildStatus.BuildStatusEnum);
            Assert.AreEqual("BuildDefinitionId", buildStatus.BuildDefinitionId);
            Assert.AreEqual("Name", buildStatus.Name);
            Assert.AreEqual("Garima Singh", buildStatus.RequestedBy);
            Assert.AreEqual("Adding rake db:migrate for sample app in code", buildStatus.Comment);
            Assert.AreEqual("https://api.travis-ci.org/ownerName/projectName/builds/1791928", buildStatus.Url);
            Assert.AreEqual("1791928", buildStatus.BuildId);
        }
    }
}
