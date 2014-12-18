using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.VersionControl.Client;
using Newtonsoft.Json;
using SirenOfShame.Lib;
using log4net;

namespace TfsServices.Configuration
{
    public class MyTfsProjectCollection
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(MyTfsProjectCollection));
        private readonly ICommonStructureService _commonStructureService;
        private readonly TfsTeamProjectCollection _tfsTeamProjectCollection;
        private readonly IBuildServer _buildServer;
        private readonly TswaClientHyperlinkService _tswaClientHyperlinkService;
        private readonly MyTfsServer _myTfsServer;
        public bool CurrentUserHasAccess { get; private set; }

        public MyTfsServer MyTfsServer
        {
            get { return _myTfsServer; }
        }

        public MyTfsProjectCollection(MyTfsServer myTfsServer, CatalogNode teamProjectCollectionNode)
        {
            try
            {
                _myTfsServer = myTfsServer;
                Name = teamProjectCollectionNode.Resource.DisplayName;
                ServiceDefinition tpcServiceDefinition = teamProjectCollectionNode.Resource.ServiceReferences["Location"];
                var configLocationService = myTfsServer.GetConfigLocationService();
                var tpcUri = new Uri(configLocationService.LocationForCurrentConnection(tpcServiceDefinition));
                _tfsTeamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(tpcUri);
                _commonStructureService = _tfsTeamProjectCollection.GetService<ICommonStructureService>();
                _buildServer = _tfsTeamProjectCollection.GetService<IBuildServer>();
                _tswaClientHyperlinkService = _tfsTeamProjectCollection.GetService<TswaClientHyperlinkService>();
                CurrentUserHasAccess = true;
            }
            catch (TeamFoundationServiceUnavailableException ex)
            {
                _log.Debug("Can't access " + Name + ". This could be because the project collection is currently offline.", ex);
                CurrentUserHasAccess = false;
            }
            catch (TeamFoundationServerUnauthorizedException ex)
            {
                _log.Debug("Unauthorized access to " + teamProjectCollectionNode, ex);
                CurrentUserHasAccess = false;
            }
        }

        public string ConvertTfsUriToUrl(Uri vstfsUri)
        {
            if (vstfsUri == null) return null;
            if (_tswaClientHyperlinkService == null) return null;
            var uri = _tswaClientHyperlinkService.GetViewBuildDetailsUrl(vstfsUri);
            if (uri == null) return null;
            return uri.ToString();
        }

        public IEnumerable<MyTfsProject> Projects
        {
            get
            {
                try
                {
                    return _commonStructureService.ListAllProjects().Select(p => new MyTfsProject(p, _buildServer, this));
                }
                catch (TeamFoundationServiceUnavailableException)
                {
                    _log.Debug("Retrieving projects from " + Name + " resulted in TeamFoundationServiceUnavailableException. This could be because the project collection is currently offline.");
                    return Enumerable.Empty<MyTfsProject>();
                }
            }
        }

        public string Name { get; private set; }

        public VersionControlServer VersionControlServer
        {
            get { return _tfsTeamProjectCollection.GetService<VersionControlServer>(); }
        }

        private Uri Uri
        {
            get { return _tfsTeamProjectCollection.Uri; }
        }

        public async Task<T> ExecuteGetHttpClientRequest<T>(string relativeUrl, Func<dynamic, T> action)
        {
            using (var webClient = GetRestWebClient())
            {
                string fullUrl = Uri + relativeUrl;
                var resultString = await webClient.DownloadStringTaskAsync(fullUrl);
                dynamic deserializedResult = JsonConvert.DeserializeObject(resultString);
                return action(deserializedResult.value);
            }
        }

        public WebClient GetRestWebClient()
        {
            var webClient = new WebClient();
            if (MyTfsServer.IsHostedTfs)
            {
                SetBasicAuthCredentials(webClient);
            }
            else
            {
                SetNetworkCredentials(webClient);
            }
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
            return webClient;
        }

        /// <summary>
        /// Using basic auth via network headers should be unnecessary, but with hosted TFS the NetworkCredential method
        /// just doesn't work.  Watch it in Fiddler and it just isn't adding the Authentication header at all.
        /// </summary>
        /// <param name="webClient"></param>
        private void SetBasicAuthCredentials(WebClient webClient)
        {
            var authenticationHeader = MyTfsServer.GetBasicAuthHeader();
            webClient.Headers.Add(authenticationHeader);
        }

        private void SetNetworkCredentials(WebClient webClient)
        {
            var networkCredentials = MyTfsServer.GetCredentialsForTfsServer();
            webClient.UseDefaultCredentials = networkCredentials == null;
            if (networkCredentials != null)
            {
                webClient.Credentials = networkCredentials;
            }
        }
    }
}
