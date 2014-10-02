using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace TravisCiServices
{
    public class TravisCiWatcher : WatcherBase
    {
        private readonly TravisCiEntryPoint _travisCiEntryPoint;
        private readonly TravisCiService _service = new TravisCiService();

        public TravisCiWatcher(SirenOfShameSettings settings, TravisCiEntryPoint travisCiEntryPoint)
            : base(settings)
        {
            _travisCiEntryPoint = travisCiEntryPoint;
        }

        protected override IList<BuildStatus> GetBuildStatus()
        {
            var watchedBuildDefinitions = GetAllWatchedBuildDefinitions()
                .ToArray();

            return _service.GetBuildsStatuses(CiEntryPointSetting, watchedBuildDefinitions)
                .Cast<BuildStatus>()
                .ToList();
        }

        private IEnumerable<BuildDefinitionSetting> GetAllWatchedBuildDefinitions()
        {
            var activeBuildDefinitionSettings = CiEntryPointSetting.BuildDefinitionSettings.Where(bd => bd.Active && bd.BuildServer == _travisCiEntryPoint.Name);
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
