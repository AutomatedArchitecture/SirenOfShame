using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace TfsRestServices
{
    public class TfsRestWatcher : WatcherBase
    {
        private readonly TfsRestCiEntryPoint _tfsRestCiEntryPoint;
        protected TfsRestService _service;

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
            catch (AggregateException ex)
            {
                var anyInvalidCredentials = ex.InnerExceptions.Any(IsInvalidCredentials);
                if (anyInvalidCredentials)
                {
                    throw new InvalidCredentialsException();
                }

                var anyServerUnavailable = ex.InnerExceptions.Any(IsServerUnavailable);
                if (anyServerUnavailable)
                {
                    throw new ServerUnavailableException();
                }

                throw;
            }
            catch (WebException ex)
            {
                var isServerUnavailable = IsServerUnavailable(ex);
                if (isServerUnavailable)
                {
                    throw new ServerUnavailableException();
                }
                throw;
            }
        }

        private static bool IsInvalidCredentials(Exception ex)
        {
            if (ex is HttpRequestException httpRequestException)
            {
                return httpRequestException.Message.Contains("401");
            }
            return false;
        }

        private static bool IsServerUnavailable(Exception ex)
        {
            if (ex is TaskCanceledException)
                return true;
            if (ex is WebException webException)
                return IsServerUnavailable(webException);
            if (ex is HttpRequestException httpRequestException)
                return IsServerUnavailable(httpRequestException);
            // SocketException?
            return false;
        }

        private static bool IsServerUnavailable(HttpRequestException ex)
        {
            // todo: is `return true` too broad?  Would this be better:
            // return ex.Message == "An error occurred while sending the request.";
            // but how to handle localization?  Should this instead be `return ex.HResult == -2146233088;`?
            return true;
        }

        private static bool IsServerUnavailable(WebException ex)
        {
            return ex.Message.StartsWith("The remote name could not be resolved:") ||
                   ex.Message.Contains("Unable to connect to the remote server");
        }

        protected virtual IEnumerable<BuildDefinitionSetting> GetAllWatchedBuildDefinitions()
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
