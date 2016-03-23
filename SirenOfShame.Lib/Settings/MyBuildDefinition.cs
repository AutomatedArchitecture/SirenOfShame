namespace SirenOfShame.Lib.Settings
{
    public abstract class MyBuildDefinition
    {
        public abstract string Id { get; }
        public abstract string Name { get; }
        public string Parent { get; protected set; }
    }
}
