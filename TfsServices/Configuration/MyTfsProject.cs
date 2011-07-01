using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Server;

namespace TfsServices.Configuration
{
    public class MyTfsProject
    {
        private readonly ProjectInfo _projectInfo;
        private readonly IBuildServer _buildServer;
        private readonly MyTfsProjectCollection _projectCollection;

        public MyTfsProject(ProjectInfo projectInfo, IBuildServer buildServer, MyTfsProjectCollection projectCollection)
        {
            _projectInfo = projectInfo;
            _buildServer = buildServer;
            _projectCollection = projectCollection;
        }

        public IEnumerable<MyTfsBuildDefinition> BuildDefinitions
        {
            get { return _buildServer.QueryBuildDefinitions(_projectInfo.Name).Select(pi => new MyTfsBuildDefinition(pi, this)); }
        }

        public string Name
        {
            get { return _projectInfo.Name; }
        }

        public MyTfsProjectCollection ProjectCollection
        {
            get { return _projectCollection; }
        }
    }
}
