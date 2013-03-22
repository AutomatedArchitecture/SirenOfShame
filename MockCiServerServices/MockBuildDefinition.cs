using SirenOfShame.Lib.Settings;

namespace MockCiServerServices
{
    public class MockBuildDefinition : MyBuildDefinition
    {
        private readonly string _id;
        private readonly string _name;

        public MockBuildDefinition(string id, string name)
        {
            _id = id;
            _name = name;
        }

        public override string Id
        {
            get { return _id; }
        }

        public override string Name
        {
            get { return _name; }
        }
    }
}
