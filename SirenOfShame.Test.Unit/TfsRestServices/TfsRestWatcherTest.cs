using System.Collections.Generic;
using System.Net;
using Moq;
using NUnit.Framework;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using TfsRestServices;

namespace SirenOfShame.Test.Unit.TfsRestServices
{
    public class MyTfsRestWatcher : TfsRestWatcher
    {
        private readonly IEnumerable<BuildDefinitionSetting> _buildDefinitions;

        public MyTfsRestWatcher(TfsRestService tfsRestService, IEnumerable<BuildDefinitionSetting> buildDefinitions, CiEntryPointSetting ciEntryPointSetting) : base(null, null)
        {
            _buildDefinitions = buildDefinitions;
            _service = tfsRestService;
            CiEntryPointSetting = ciEntryPointSetting;
        }

        protected override IEnumerable<BuildDefinitionSetting> GetAllWatchedBuildDefinitions()
        {
            return _buildDefinitions;
        }

        public IList<BuildStatus> MyGetBuildStatus()
        {
            return GetBuildStatus();
        }
    }

    [TestFixture]
    public class TfsRestWatcherTest
    {
        [Test]
        public void GivenNullUrlInCiEntryPointSetting_WhenGettingBuildStatus_ThenSosException()
        {
            // arrange
            var ciEntryPointSetting = new CiEntryPointSetting();
            var tfsRestWatcher = new MyTfsRestWatcher(null, new BuildDefinitionSetting[] { }, ciEntryPointSetting);

            // act
            Assert.Throws<SosException>(() => tfsRestWatcher.MyGetBuildStatus(), "TFS URL is null or empty")
            ;
        }

        [Test]
        public void GivenWebExceptionRemoteNameCouldNotBeResolved_WhenGettingBuildStatus_ThenServerUnavailableException()
        {
            // arrange
            var buildDefinitionSettings = new[] {new BuildDefinitionSetting()};
            var tfsRestService = new Mock<TfsRestService>();
            var ciEntryPointSetting = new CiEntryPointSetting { Url = "url" };
            tfsRestService.Setup(i => i.GetBuildsStatuses(ciEntryPointSetting, buildDefinitionSettings))
                .ThrowsAsync(new WebException("The remote name could not be resolved:"));
            var tfsRestWatcher = new MyTfsRestWatcher(tfsRestService.Object, buildDefinitionSettings, ciEntryPointSetting);

            // assert & act
            Assert.Throws<ServerUnavailableException>(() =>
                tfsRestWatcher.MyGetBuildStatus()
            );
        }

        [Test]
        public void GivenWebExceptionUnableConnectRemoteServer_WhenGettingBuildStatus_ThenServerUnavailableException()
        {
            // arrange
            var buildDefinitionSettings = new[] {new BuildDefinitionSetting()};
            var tfsRestService = new Mock<TfsRestService>();
            var ciEntryPointSetting = new CiEntryPointSetting { Url = "url" };
            tfsRestService.Setup(i => i.GetBuildsStatuses(ciEntryPointSetting, buildDefinitionSettings))
                .ThrowsAsync(new WebException("Unable to connect to the remote server"));
            var tfsRestWatcher = new MyTfsRestWatcher(tfsRestService.Object, buildDefinitionSettings, ciEntryPointSetting);

            // assert & act
            Assert.Throws<ServerUnavailableException>(() =>
                tfsRestWatcher.MyGetBuildStatus()
            );
        }

        [Test]
        public void GivenHttpRequestException_WhenGettingBuildStatus_ThenServerUnavailableException()
        {
            // arrange
            var buildDefinitionSettings = new[] {new BuildDefinitionSetting()};
            var tfsRestService = new Mock<TfsRestService>();
            var ciEntryPointSetting = new CiEntryPointSetting { Url = "url" };
            tfsRestService.Setup(i => i.GetBuildsStatuses(ciEntryPointSetting, buildDefinitionSettings))
                .ThrowsAsync(new System.Net.Http.HttpRequestException("Unable to connect to the remote server"));
            var tfsRestWatcher = new MyTfsRestWatcher(tfsRestService.Object, buildDefinitionSettings, ciEntryPointSetting);

            // assert & act
            Assert.Throws<ServerUnavailableException>(() =>
                tfsRestWatcher.MyGetBuildStatus()
            );
        }

        [Test]
        public void GivenHttpRequestExceptionWith401InMessage_WhenGettingBuildStatus_ThenInvalidCredentialsException()
        {
            // arrange
            var buildDefinitionSettings = new[] { new BuildDefinitionSetting() };
            var tfsRestService = new Mock<TfsRestService>();
            var ciEntryPointSetting = new CiEntryPointSetting { Url = "url" };
            tfsRestService.Setup(i => i.GetBuildsStatuses(ciEntryPointSetting, buildDefinitionSettings))
                .ThrowsAsync(new System.Net.Http.HttpRequestException("Response status code does not indicate success: 401 (Unauthorized)."));
            var tfsRestWatcher = new MyTfsRestWatcher(tfsRestService.Object, buildDefinitionSettings, ciEntryPointSetting);

            // assert & act
            Assert.Throws<InvalidCredentialsException>(() =>
                tfsRestWatcher.MyGetBuildStatus()
            );
        }
    }
}
