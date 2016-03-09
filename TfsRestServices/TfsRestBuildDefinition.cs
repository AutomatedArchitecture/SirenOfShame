using SirenOfShame.Lib.Settings;

namespace TfsRestServices
{
    public class TfsRestBuildDefinition : MyBuildDefinition
    {
        public TfsRestBuildDefinition() {  }

        public TfsRestBuildDefinition(TfsJsonBuildDefinition jsonBuildDefinition)
        {
            Id = jsonBuildDefinition.Definition.Id.ToString();
            Name = jsonBuildDefinition.Definition.Name;
        }

        public override string Id { get; }
        public override string Name { get; }
    }
}