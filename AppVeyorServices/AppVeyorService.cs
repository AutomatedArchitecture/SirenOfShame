using System;
using System.Collections.Generic;
using System.Linq;
using AppVeyorServices.AppVeyor;
using AppVeyorServices.Properties;
using log4net;
using ServiceStack;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace AppVeyorServices
{
    public class AppVeyorService : ServiceBase
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof (AppVeyorService));

        public static void GetProjects(string baseUrl, string token,
            Action<IEnumerable<AppVeyorBuildDefinition>> complete,
            Action<Exception> onError)
        {
            var serviceClient = CreateClient(baseUrl, token);

            serviceClient.GetAsync(new GetProjects())
                .Success(response => complete(response.Select(project => new AppVeyorBuildDefinition(project))))
                .Error(ex =>
                {
                    _log.Error(Resources.AppVeyorService_ServerError, ex);
                    onError(ex);
                });
        }

        public IEnumerable<AppVeyorBuildStatus> GetBuildsStatuses(CiEntryPointSetting ciEntryPointSetting,
            IEnumerable<BuildDefinitionSetting> watchedBuildDefinitions)
        {
            var parallelResult = from buildDefinitionSetting in watchedBuildDefinitions
                select GetBuildStatus(buildDefinitionSetting, ciEntryPointSetting);
            return parallelResult.AsParallel().ToList();
        }

        private AppVeyorBuildStatus GetBuildStatus(BuildDefinitionSetting buildDefinitionSetting,
            CiEntryPointSetting ciEntryPointSetting)
        {
            var token = ciEntryPointSetting.GetPassword();
            var treatUnstableAsSuccess = ciEntryPointSetting.TreatUnstableAsSuccess;
            var projectInfo = AppVeyorBuildDefinition.FromId(buildDefinitionSetting.Id);


            var serviceClient = CreateClient(ciEntryPointSetting.Url, token);

            var request = new GetProjectLastBuild
            {
                AccountName = projectInfo.AccountName,
                ProjectSlug = projectInfo.Slug
            };

            try
            {
                var response = serviceClient.Get(request);
                if (response.Build == null)
                {
                    throw new BuildDefinitionNotFoundException(buildDefinitionSetting);
                }

                return new AppVeyorBuildStatus(response.Build, buildDefinitionSetting, treatUnstableAsSuccess);
            }
            catch (WebServiceException ex)
            {
                throw new BuildDefinitionNotFoundException(buildDefinitionSetting);
            }
        }

        private static JsonServiceClient CreateClient(string baseUrl, string token)
        {
            var client = new JsonServiceClient(baseUrl.TrimEnd('/'));
            client.AddHeader(HttpHeaders.Authorization, "Bearer {0}".Fmt(token));

            return client;
        }
    }
}