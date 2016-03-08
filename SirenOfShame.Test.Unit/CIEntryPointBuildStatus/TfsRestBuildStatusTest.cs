using Newtonsoft.Json;
using NUnit.Framework;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Test.Unit.Resources;
using TfsRestServices;

namespace SirenOfShame.Test.Unit.CIEntryPointBuildStatus
{
    [TestFixture]
    public class TfsRestBuildStatusTest
    {
        [Test]
        public void GivenATraditionalXmlBuildDefinition_WhenWeParseIt_ThenWeParseItCorrectly()
        {
            var tfsRestService = new TfsRestService();
            var tfsRestWorkingBuild = ResourceManager.TfsRestBuildDefinitions1;
            var jsonWrapper = JsonConvert.DeserializeObject<TfsJsonWrapper>(tfsRestWorkingBuild);
            var buildDefinition = jsonWrapper.Value[1];
            var buildStatus = new TfsRestBuildStatus(buildDefinition);
            Assert.AreEqual(BuildStatusEnum.Working, buildStatus.BuildStatusEnum);
            //Assert.AreEqual("New Visual Studio definition 1", buildStatus.BuildDefinitionId);
            //Assert.AreEqual("Name", buildStatus.Name);
            //Assert.AreEqual("Lee Richardson", buildStatus.RequestedBy);
            //Assert.AreEqual("Merge branch 'master' of https://github.com/tfsRest-ci/tfsRest-ci", buildStatus.Comment);
            //Assert.AreEqual("https://sirenofshame.visualstudio.com/DefaultCollection/_permalink/_build/index?collectionId=3be0f19d-62d0-4f45-a140-f219cb9c08ae&projectId=cd1d630e-e0fc-46d3-9540-a477d17a84b1&buildId=17", buildStatus.Url);
            //Assert.AreEqual("18", buildStatus.BuildId);
        }
   }
}
