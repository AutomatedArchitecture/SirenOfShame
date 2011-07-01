using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace TfsServices.Configuration
{
    public class MyTfsProjectCollection
    {
        private readonly ICommonStructureService _commonStructureService;
        private readonly TfsTeamProjectCollection _tfsTeamProjectCollection;
        private readonly IBuildServer _buildServer;

        public MyTfsProjectCollection(CatalogNode teamProjectCollectionNode, TfsConfigurationServer tfsConfigurationServer)
        {
            Name = teamProjectCollectionNode.Resource.DisplayName;
            ServiceDefinition tpcServiceDefinition = teamProjectCollectionNode.Resource.ServiceReferences["Location"];
            var configLocationService = tfsConfigurationServer.GetService<ILocationService>();
            var tpcUri = new Uri(configLocationService.LocationForCurrentConnection(tpcServiceDefinition));
            _tfsTeamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(tpcUri);
            _commonStructureService = _tfsTeamProjectCollection.GetService<ICommonStructureService>();
            _buildServer = _tfsTeamProjectCollection.GetService<IBuildServer>();
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
