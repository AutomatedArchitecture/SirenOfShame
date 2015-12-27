using AppVeyorServices.AppVeyor;
using ServiceStack;
using SirenOfShame.Lib.Settings;

namespace AppVeyorServices
{
    public class AppVeyorBuildDefinition : MyBuildDefinition
    {
        private const string IdFormat = "{0}.{1}";
        private readonly string id;
        private readonly string name;

        public AppVeyorBuildDefinition(Project project)
        {
            id = ToId(project);
            name = project.Name;
        }

        public override string Id
        {
            get { return id; }
        }

        public override string Name
        {
            get { return name; }
        }

        internal static string ToId(Project project)
        {
            return IdFormat.Fmt(project.AccountName, project.Slug);
        }

        internal static ProjectInfo FromId(string id)
        {
            var parts = id.Split('.');

            return new ProjectInfo
            {
                AccountName = parts[0],
                Slug = parts[1]
            };
        }
    }

    public class ProjectInfo
    {
        public string AccountName { get; set; }
        public string Slug { get; set; }
    }
}