using System.Runtime.Serialization;
using ServiceStack;

namespace AppVeyorServices.AppVeyor
{
    [DataContract]
    [Route("/projects/{accountName}/{projectSlug}", "GET")]
    public class GetProjectLastBuild : IReturn<GetProjectLastBuildResponse>
    {
        [DataMember(Name = "accountName")]
        public string AccountName { get; set; }

        [DataMember(Name = "projectSlug")]
        public string ProjectSlug { get; set; }
    }
}