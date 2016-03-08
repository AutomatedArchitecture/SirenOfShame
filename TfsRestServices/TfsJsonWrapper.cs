namespace TfsRestServices
{
    public class TfsJsonWrapper
    {
        public TfsJsonBuildDefinition[] Value { get; set; }
    }

    public class TfsJsonBuildDefinition
    {
        public int Id { get; set; }
        public TfsJsonBuildDefinitionDefinition Definition { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public TfsJsonPerson RequestedFor { get; set; }
    }

    public class TfsJsonPerson
    {
        public string DisplayName { get; set; }
    }

    public class TfsJsonBuildDefinitionDefinition
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}