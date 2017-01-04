using System.Xml.Linq;
using NUnit.Framework;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Test.Unit.Resources;
using TeamCityServices;
using SirenOfShame.Test.Unit.Resources.TeamCity;

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
    
    [TestFixture]
    public class TeamCityBuildStatusTest
    {
        [Test]
        [ExpectedException(typeof(ServerUnavailableException))]
        public void ServerIsDoingACleanup()
        {
            FakeTeamCityService teamCityService = new FakeTeamCityService();
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            XDocument xDoc = TeamCityResources.TeamCityServerCleanup;
            teamCityService.GetBuildStatusAndCommentsFromXDocumentFake(
                "fakeurl",
                "username",
                "password",
                buildDefinitionSetting,
                xDoc);
        }
    }
}
