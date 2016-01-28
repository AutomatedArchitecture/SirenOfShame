namespace AppVeyorServices.AppVeyor
{
    public class GetProjectLastBuildResponse
    {
        public Project Project { get; set; }
        public ProjectBuild Build { get; set; }
    }
}