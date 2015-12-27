using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace AppVeyorServices
{
    public class AppVeyorWatcher : WatcherBase
    {
        private readonly AppVeyorCiEntryPoint _entryPoint;
        private readonly AppVeyorService _service = new AppVeyorService();

        public AppVeyorWatcher(SirenOfShameSettings settings, AppVeyorCiEntryPoint entryPoint)
            : base(settings)
        {
            _entryPoint = entryPoint;
        }

        protected override IList<BuildStatus> GetBuildStatus()
        {
            var watchedBuildDefinitions =
                CiEntryPointSetting.BuildDefinitionSettings.Where(
                    bd => bd.Active && bd.BuildServer == _entryPoint.Name);

            return _service.GetBuildsStatuses(CiEntryPointSetting, watchedBuildDefinitions)
                .Cast<BuildStatus>().ToList();
        }

        public override void StopWatching()
        {
        }

        public override void Dispose()
        {
        }
    }
}