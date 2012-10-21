using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace HudsonServices
{
    using System.Globalization;

    public class HudsonService : ServiceBase
    {
        protected override XDocument DownloadXml(string url, string userName, string password, string cookie = null)
        {
            WebClientXml webClientXml = new WebClientXml
            {
                // hudson/jenkins api's apparently always require base64 encoded credentials rather than basic auth
                AuthenticationType = AuthenticationTypeEnum.Base64EncodeInHeader
            };

            return webClientXml.DownloadXml(url, userName, password, cookie);
        }
        
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(HudsonService));

        public delegate void GetProjectsCompleteDelegate(HudsonBuildDefinition[] buildDefinitions);

        public void GetProjects(string rootUrl, string userName, string password, GetProjectsCompleteDelegate complete, Action<Exception> onError)
        {
            WebClientXml webClient = new WebClientXml
            {
                AuthenticationType = AuthenticationTypeEnum.Base64EncodeInHeader,
                UserName = userName,
                Password = password
            };
            rootUrl = GetRootUrl(rootUrl);

            webClient.DownloadXmlAsync(rootUrl + "/api/xml", onSuccess: doc =>
            {
                if (doc.Root == null)
                {
                    onError(new Exception("No results returned"));
                    return;
                }
                HudsonBuildDefinition[] buildDefinitions = doc.Root
                    .Elements("job")
                    .Select(projectXml => new HudsonBuildDefinition(rootUrl, projectXml))
                    .ToArray();
                complete(buildDefinitions);
            }, onError: OnError(onError));
        }

        private static Action<Exception> OnError(Action<Exception> onError)
        {
            return delegate(Exception ex)
            {
                _log.Error("Error connecting to server", ex);
                onError(ex);
            };
        }

        private string GetRootUrl(string rootUrl)
        {
            if (string.IsNullOrEmpty(rootUrl)) return null;
            rootUrl = rootUrl.TrimEnd('/');
            return rootUrl;
        }

        public IList<HudsonBuildStatus> GetBuildsStatuses(string rootUrl, string userName, string password, BuildDefinitionSetting[] watchedBuildDefinitions)
        {
            rootUrl = GetRootUrl(rootUrl);

            var parallelResult = from buildDefinitionSetting in watchedBuildDefinitions
                                 select GetBuildStatus(rootUrl, buildDefinitionSetting, userName, password);
            return parallelResult.AsParallel().ToList();
        }

        private HudsonBuildStatus GetBuildStatus(string rootUrl, BuildDefinitionSetting buildDefinitionSetting, string userName, string password)
        {
            string url = rootUrl + "/job/" + buildDefinitionSetting.Id + "/api/xml";
            try
            {
                XDocument doc = DownloadXml(url, userName, password);
                if (doc.Root == null)
                {
                    return new HudsonBuildStatus(null, buildDefinitionSetting, "Could not get project status");
                }
                var lastBuildElem = doc.Root.Element("lastBuild");
                if (lastBuildElem == null)
                {
                    return new HudsonBuildStatus(null, buildDefinitionSetting, "No project builds found");
                }
                var buildNumber = lastBuildElem.ElementValueOrDefault("number");
                var buildUrl = rootUrl + "/job/" + buildDefinitionSetting.Id + "/" + buildNumber;
                if (string.IsNullOrWhiteSpace(buildUrl)) throw new Exception("Could not get build url");
                buildUrl += "/api/xml";
                doc = DownloadXml(buildUrl, userName, password);
                if (doc.Root == null) throw new Exception("Could not get project build status");
                return GetBuildStatusAndCommentsFromXDocument(rootUrl, userName, password, buildDefinitionSetting, doc);
            }
            catch (SosException ex)
            {
                if (ex.Message.ToLower(CultureInfo.CurrentCulture).Contains("not_found"))
                {
                    throw new BuildDefinitionNotFoundException(buildDefinitionSetting);
                }
                throw;
            }
        }

        private HudsonBuildStatus GetBuildStatusAndCommentsFromXDocument(IComparable rootUrl, string userName, string password, BuildDefinitionSetting buildDefinitionSetting, XDocument doc)
        {
            return new HudsonBuildStatus(doc, buildDefinitionSetting);
        }
    }
}
