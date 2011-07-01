using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using TfsServices.Configuration;

namespace TfsServices
{
    public class TfsWatcher : WatcherBase
    {
        private readonly TfsCiEntryPoint _tfsCiEntryPoint;

        public TfsWatcher(SirenOfShameSettings settings, TfsCiEntryPoint tfsCiEntryPoint) : base(settings)
        {
            _tfsCiEntryPoint = tfsCiEntryPoint;
        }

        private MyTfsServer _myTfsServer;
        private MyTfsBuildDefinition[] _watchedBuildDefinitions;

        protected override IEnumerable<BuildStatus> GetBuildStatus()
        {
            if (_myTfsServer == null) _myTfsServer = new MyTfsServer(Settings.FindAddSettings(_tfsCiEntryPoint.Name).Url);
            if (_watchedBuildDefinitions == null)
            {
                _watchedBuildDefinitions = GetAllWatchedBuildDefinitions().ToArray();
            }

            var buildDefinitionsByServer = _watchedBuildDefinitions.GroupBy(bd => bd.BuildServer);
            return buildDefinitionsByServer.SelectMany(g => g.Key.GetBuildStatuses(g));
        }

        private IEnumerable<MyTfsBuildDefinition> GetAllWatchedBuildDefinitions()
        {
            IEnumerable<MyTfsBuildDefinition> allTfsBuildDefinitions = _myTfsServer.ProjectCollections
                .SelectMany(pc => pc.Projects)
                .SelectMany(p => p.BuildDefinitions);

            var activeBuildDefinitionSettings = Settings.BuildDefinitionSettings.Where(bd => bd.Active);
            return from buildDef in allTfsBuildDefinitions
                   join buildDefSetting in activeBuildDefinitionSettings on
                       buildDef.Id equals buildDefSetting.Id
                   select buildDef;
        }

        private void Reset()
        {
            if (_myTfsServer != null)
            {
                _myTfsServer.Dispose();
                _myTfsServer = null;
            }
            _watchedBuildDefinitions = null;
        }

        public override void StopWatching()
        {
            Reset();
        }

        public override void Dispose()
        {
            Reset();
        }
    }
}
