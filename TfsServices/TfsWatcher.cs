using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using log4net;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using TfsServices.Configuration;
using BuildStatus = SirenOfShame.Lib.Watcher.BuildStatus;

namespace TfsServices
{
    public class TfsWatcher : WatcherBase
    {
        private static readonly ILog Log = MyLogManager.GetLogger(typeof(TfsWatcher));
        private readonly TfsCiEntryPoint _tfsCiEntryPoint;

        public TfsWatcher(SirenOfShameSettings settings, TfsCiEntryPoint tfsCiEntryPoint) : base(settings)
        {
            _tfsCiEntryPoint = tfsCiEntryPoint;
        }

        private MyTfsServer _myTfsServer;
        private MyTfsBuildDefinition[] _watchedBuildDefinitions;

        protected override IList<BuildStatus> GetBuildStatus()
        {
            try {
                if (_myTfsServer == null) _myTfsServer = new MyTfsServer(CiEntryPointSetting);
                if (_watchedBuildDefinitions == null)
                {
                    _watchedBuildDefinitions = GetAllWatchedBuildDefinitions().ToArray();
                }

                var buildDefinitionsByServer = _watchedBuildDefinitions.GroupBy(bd => bd.BuildServer);
                return buildDefinitionsByServer.SelectMany(g => g.Key.GetBuildStatuses(g)).ToList();
            }
            catch (DatabaseOperationTimeoutException ex)
            {
                Log.Warn(ex);
                throw new ServerUnavailableException(ex.Message, ex);
            }
            catch (DatabaseConnectionException ex)
            {
                throw new ServerUnavailableException(ex.Message, ex);
            }
            catch (TeamFoundationServiceUnavailableException ex)
            {
                throw new ServerUnavailableException(ex.Message, ex);
            }
            catch (BuildServerException ex)
            {
                Log.Error("Logging a BuildServerException as Server Unavailable Exception so SoS will continue to try to find the server", ex);
                throw new ServerUnavailableException(ex.Message, ex);
            }
            catch (WebException ex)
            {
                throw new ServerUnavailableException(ex.Message, ex);
            }
            catch (VersionControlException ex)
            {
                throw new ServerUnavailableException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Error("Error getting build status", ex);
                throw new ServerUnavailableException(ex.Message, ex);
            }
        }

        private IEnumerable<MyTfsBuildDefinition> GetAllWatchedBuildDefinitions()
        {
            IEnumerable<MyTfsBuildDefinition> allTfsBuildDefinitions = _myTfsServer.ProjectCollections
                .SelectMany(pc => pc.Projects)
                .SelectMany(p => p.BuildDefinitions);

            var activeBuildDefinitionSettings = CiEntryPointSetting.BuildDefinitionSettings.Where(bd => bd.Active && bd.BuildServer == _tfsCiEntryPoint.Name);
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
