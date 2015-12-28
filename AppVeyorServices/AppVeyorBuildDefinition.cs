using AppVeyorServices.AppVeyor;
using ServiceStack;
using SirenOfShame.Lib.Settings;

namespace AppVeyorServices
{
    public class AppVeyorBuildDefinition : MyBuildDefinition
    {
        private const string ID_FORMAT = "{0}.{1}";
        private readonly string _id;
        private readonly string _name;

        public AppVeyorBuildDefinition(Project project)
        {
            _id = ToId(project);
            _name = project.Name;
        }

        public override string Id
        {
            get { return _id; }
        }

        public override string Name
        {
            get { return _name; }
        }

        private static string ToId(Project project)
        {
            return ID_FORMAT.Fmt(project.AccountName, project.Slug);
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