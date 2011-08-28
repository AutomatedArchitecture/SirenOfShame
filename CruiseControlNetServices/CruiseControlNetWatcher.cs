using System.Collections.Generic;
using System.Linq;
using System.Net;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace CruiseControlNetServices
{
    public class CruiseControlNetWatcher : WatcherBase
    {
        private readonly CruiseControlNetCIEntryPoint _cruiseControlNetCiEntryPoint;
        private readonly CruiseControlNetService _service = new CruiseControlNetService();

        public CruiseControlNetWatcher(SirenOfShameSettings settings, CruiseControlNetCIEntryPoint cruiseControlNetCiEntryPoint)
            : base(settings)
        {
            _cruiseControlNetCiEntryPoint = cruiseControlNetCiEntryPoint;
        }

        protected override IList<BuildStatus> GetBuildStatus()
        {
            CiEntryPointSetting ciEntryPointSetting = CiEntryPointSetting;
            var watchedBuildDefinitions = GetAllWatchedBuildDefinitions().ToArray();

            if (string.IsNullOrEmpty(ciEntryPointSetting.Url))
                throw new SosException("CruiseControl.NET URL is null or empty");

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
            var activeBuildDefinitionSettings = CiEntryPointSetting.BuildDefinitionSettings.Where(bd => bd.Active && bd.BuildServer == _cruiseControlNetCiEntryPoint.Name);
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
