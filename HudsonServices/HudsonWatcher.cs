using System.Collections.Generic;
using System.Linq;
using System.Net;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace HudsonServices
{
    public class HudsonWatcher : WatcherBase
    {
        private HudsonCIEntryPoint _hudsonCiEntryPoint;
        private readonly HudsonService _service = new HudsonService();

        public HudsonWatcher(SirenOfShameSettings settings, HudsonCIEntryPoint hudsonCiEntryPoint)
            : base(settings)
        {
            _hudsonCiEntryPoint = hudsonCiEntryPoint;
        }

        protected override IList<BuildStatus> GetBuildStatus()
        {
            var watchedBuildDefinitions = GetAllWatchedBuildDefinitions().ToArray();

            if (string.IsNullOrEmpty(CiEntryPointSetting.Url))
                throw new SosException("Jenkins URL is null or empty");

            try
            {
                return _service.GetBuildsStatuses(CiEntryPointSetting, watchedBuildDefinitions)
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
            var activeBuildDefinitionSettings = CiEntryPointSetting.BuildDefinitionSettings.Where(bd => bd.Active && bd.BuildServer == _hudsonCiEntryPoint.Name);
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
