using System;

namespace AppVeyorServices.AppVeyor
{
    public class ProjectBuild
    {
        public string BuildId { get; set; }
        public string Status { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public string Message { get; set; }
        public string AuthorName { get; set; }
        public string ComitterName { get; set; }
        public string Version { get; set; }
    }
}