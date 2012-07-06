using System.Collections.Generic;
using System.Linq;
using System.Net;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace BuildBotServices
{
    public class BuildBotWatcher : WatcherBase
    {
        private readonly BuildBotCIEntryPoint _BuildBotCiEntryPoint;
        private readonly BuildBotService _service = new BuildBotService();

        public BuildBotWatcher(SirenOfShameSettings settings, BuildBotCIEntryPoint BuildBotCiEntryPoint)
            : base(settings)
        {
            _BuildBotCiEntryPoint = BuildBotCiEntryPoint;
        }

        protected override IList<BuildStatus> GetBuildStatus()
        {
            CiEntryPointSetting ciEntryPointSetting = CiEntryPointSetting;
            var watchedBuildDefinitions = GetAllWatchedBuildDefinitions().ToArray();

            if (string.IsNullOrEmpty(ciEntryPointSetting.Url))
                throw new SosException("Buildbot URL is null or empty");

            try
            {
                return _service.GetBuildsStatuses(ciEntryPointSetting.Url,
                                                       ciEntryPointSetting.UserName,
                                                       ciEntryPointSetting.GetPassword(),
                                                       watchedBuildDefinitions)
                    .Cast<BuildStatus>().ToList();
            }
            catch (WebException ex)
            {
                if (
                    ex.Message.StartsWith("The remote name could not be resolved:") ||
                    ex.Message.Contains("Unable to connect to the remote server")
                    )
                {
                    throw new ServerUnavailableException();
                }
                throw;
            }
        }

        private IEnumerable<BuildDefinitionSetting> GetAllWatchedBuildDefinitions()
        {
            var activeBuildDefinitionSettings = CiEntryPointSetting.BuildDefinitionSettings.Where(bd => bd.Active && bd.BuildServer == _BuildBotCiEntryPoint.Name);
            return activeBuildDefinitionSettings;
        }

        public override void StopWatching()
        {

        }

        public override void Dispose()
        {

        }
    }
}
