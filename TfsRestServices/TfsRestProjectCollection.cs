using System.Collections.Generic;

namespace TfsRestServices
{
    public class TfsRestProjectCollection
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public List<TfsRestProject> Projects { get; set; }
    }
}