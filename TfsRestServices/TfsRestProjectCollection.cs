using System.Collections.Generic;

namespace TfsRestServices
{
    public class TfsRestProjectCollection
    {
        public TfsRestProjectCollection(TfsJsonProjectCollection projectCollection)
        {
            Name = projectCollection.Name;
            Id = projectCollection.Id;
            Url = projectCollection.Url;
            Projects = new List<TfsRestProject>();
        }

        public string Name { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public List<TfsRestProject> Projects { get; set; }
    }
}