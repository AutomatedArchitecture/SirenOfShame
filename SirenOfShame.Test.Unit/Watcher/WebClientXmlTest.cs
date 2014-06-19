using System.Net;
using NUnit.Framework;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    [TestFixture]
    public class WebClientXmlTest
    {
        [Test]
        public void TeamCityBuildDefinitionUnavailable()
        {
            const string message = @"Error has occurred during request processing (Not Found).
Error: jetbrains.buildServer.server.rest.errors.NotFoundException: No build type or template is found by id, internal id or name 'SimpleApp_MainBuildConfiguration'.
Could not find the entity requested. Check the reference is correct and the user has permissions to access the entity.";
            var result = WebClientXml.ToServerUnavailableException("http://localhost:8083/httpAuth/app/rest/builds/buildType:SimpleApp_MainBuildConfiguration", new WebException(message), message);
            Assert.IsTrue(result is BuildDefinitionNotFoundException);
        }
    }
}
