using System.Collections.Generic;

namespace TfsRestServices
{
    public class TfsRestProject
    {
        public List<TfsRestBuildDefinition> BuildDefinitions { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
    }
}