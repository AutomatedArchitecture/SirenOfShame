using SirenOfShame.Lib.Settings;

namespace TfsRestServices
{
    public class TfsRestBuildDefinition : MyBuildDefinition
    {
        public TfsRestBuildDefinition() {  }

        public TfsRestBuildDefinition(TfsJsonBuildDefinition jsonBuildDefinition, TfsJsonProject project, TfsJsonProjectCollection projectCollection)
        {
            Id = jsonBuildDefinition.Id.ToString();
            Name = jsonBuildDefinition.Name;
            Parent = projectCollection.Name + "/" + project.Name;
        }

        public override string Id { get; }
        public override string Name { get; }
    }
}