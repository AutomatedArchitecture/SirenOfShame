using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Test.Unit.Resources;
using TeamCityServices;

namespace SirenOfShame.Test.Unit.Service
{
    public class FakeTeamCityService : TeamCityService
    {
        public TeamCityBuildStatus GetBuildStatusAndCommentsFromXDocumentFake(
            string rootUrl,
            string userName,
            string password,
            BuildDefinitionSetting buildDefinitionSetting,
            XDocument buildResultXDoc)
        {
            return GetBuildStatusAndCommentsFromXDocument(rootUrl,
                                                               userName,
                                                               password,
                                                               buildDefinitionSetting,
                                                               buildResultXDoc);
        }
    }
    
    [TestClass]
    public class TeamCityBuildStatusTest
    {
        [TestMethod]
        [ExpectedException(typeof(ServerUnavailableException))]
        public void ServerIsDoingACleanup()
        {
            FakeTeamCityService teamCityService = new FakeTeamCityService();
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            XDocument xDoc = ResourceManager.TeamCityServerCleanup;
            teamCityService.GetBuildStatusAndCommentsFromXDocumentFake(
                "fakeurl",
                "username",
                "password",
                buildDefinitionSetting,
                xDoc);
            //Assert.AreEqual(BuildStatusEnum.Unknown, result.BuildStatusEnum);
        }
    }
}
