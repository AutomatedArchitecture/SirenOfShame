using System.Collections.Generic;
using ServiceStack;

namespace AppVeyorServices.AppVeyor
{
    [Route("/projects", "GET")]
    public class GetProjects : IReturn<List<Project>>
    {
    }
}