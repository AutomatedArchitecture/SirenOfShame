using System.Collections.Generic;
using System.Linq;
using System.Net;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace TfsRestServices
{
    public class TfsRestWatcher : WatcherBase
    {
        private readonly TfsRestCiEntryPoint _tfsRestCiEntryPoint;
        private readonly TfsRestService _service;

        public TfsRestWatcher(SirenOfShameSettings settings, TfsRestCiEntryPoint tfsRestCiEntryPoint) : base(settings)
        {
            _tfsRestCiEntryPoint = tfsRestCiEntryPoint;
            _service = new TfsRestService();
        }

        protected override IList<BuildStatus> GetBuildStatus()
        {
            var watchedBuildDefinitions = GetAllWatchedBuildDefinitions().ToArray();
            if (string.IsNullOrEmpty(CiEntryPointSetting.Url))
                throw new SosException("TFS URL is null or empty");

            try
            {
                return _service.GetBuildsStatuses(CiEntryPointSetting, watchedBuildDefinitions).Result
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
            var activeBuildDefinitionSettings = CiEntryPointSetting.BuildDefinitionSettings.Where(bd => bd.Active && bd.BuildServer == _tfsRestCiEntryPoint.Name);
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
