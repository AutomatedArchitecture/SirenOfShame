using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.VersionControl.Client;
using SirenOfShame.Lib;
using log4net;

namespace TfsServices.Configuration
{
    public class MyTfsProjectCollection
    {
        class MyCredentials : ICredentialsProvider
        {
            private readonly NetworkCredential _networkCredential;
            public MyCredentials(NetworkCredential networkCredential)
            {
                _networkCredential = networkCredential;
            }

            public ICredentials GetCredentials(Uri uri, ICredentials failedCredentials)
            {
                return _networkCredential;
            }

            public void NotifyCredentialsAuthenticated(Uri uri)
            {
            }
        }

        private static readonly ILog _log = MyLogManager.GetLogger(typeof(MyTfsProjectCollection));
        private readonly ICommonStructureService _commonStructureService;
        private readonly TfsTeamProjectCollection _tfsTeamProjectCollection;
        private readonly IBuildServer _buildServer;
        private readonly ILinking _linkingService;
        public bool CurrentUserHasAccess { get; set; }

        public MyTfsProjectCollection(CatalogNode teamProjectCollectionNode, TfsConfigurationServer tfsConfigurationServer, NetworkCredential networkCredential)
        {
            try
            {
                Name = teamProjectCollectionNode.Resource.DisplayName;
                ServiceDefinition tpcServiceDefinition = teamProjectCollectionNode.Resource.ServiceReferences["Location"];
                var configLocationService = tfsConfigurationServer.GetService<ILocationService>();
                var tpcUri = new Uri(configLocationService.LocationForCurrentConnection(tpcServiceDefinition));
                _tfsTeamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(tpcUri, new MyCredentials(networkCredential));
                _commonStructureService = _tfsTeamProjectCollection.GetService<ICommonStructureService>();
                _buildServer = _tfsTeamProjectCollection.GetService<IBuildServer>();
                _linkingService = _tfsTeamProjectCollection.GetService<ILinking>();
                CurrentUserHasAccess = true;
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
            return _linkingService.GetArtifactUrl(vstfsUri.ToString());
        }
        
        public IEnumerable<MyTfsProject> Projects
        {
            get { return _commonStructureService.ListAllProjects().Select(p => new MyTfsProject(p, _buildServer, this)); }
        }

        public string Name { get; set; }

        public VersionControlServer VersionControlServer
        {
            get { return _tfsTeamProjectCollection.GetService<VersionControlServer>(); }
        }
    }
}
