using System.Collections.Generic;

namespace TfsRestServices
{
    public class TfsRestProject
    {
        public TfsRestProject(TfsJsonProject project)
        {
            Name = project.Name;
            Id = project.Id;
            Url = project.Url;
            BuildDefinitions = new List<TfsRestBuildDefinition>();
        }

        public List<TfsRestBuildDefinition> BuildDefinitions { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
    }
}