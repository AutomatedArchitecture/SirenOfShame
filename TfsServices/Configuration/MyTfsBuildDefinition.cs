using System;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Build.Client;
using SirenOfShame.Lib.Settings;

namespace TfsServices.Configuration
{
    public class MyTfsBuildDefinition : MyBuildDefinition
    {
        private readonly IBuildDefinition _buildDefinition;
        private readonly MyBuildServer _myBuildServer;

        public MyTfsBuildDefinition(IBuildDefinition buildDefinition, MyTfsProject myTfsProject)
        {
            _buildDefinition = buildDefinition;
            _myBuildServer = new MyBuildServer(_buildDefinition.BuildServer, myTfsProject);
        }

        public override string Name
        {
            get { return _buildDefinition.Name; }
        }

        public override string Id
        {
            get { return _buildDefinition.Name; }
        }

        public MyBuildServer BuildServer
        {
            get { return _myBuildServer; }
        }

        public Uri Uri
        {
            get { return _buildDefinition.Uri; }
        }

        public TreeNode GetAsNode(bool active)
        {
            return new TreeNode(Name) { Tag = Id, Checked = active };
        }
    }
}
