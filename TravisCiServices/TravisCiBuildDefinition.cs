using System;
using SirenOfShame.Lib.Settings;

namespace TravisCiServices
{
    public class TravisCiBuildDefinition : MyBuildDefinition
    {
        private readonly string _id;
        private readonly string _projectName;

        private TravisCiBuildDefinition(string fullIdString)
        {
            _id = fullIdString;
            var parts = fullIdString.Split('/');
            if (parts.Length != 3)
            {
                throw new Exception("Could not parse id string");
            }

            OwnerName = parts[0];
            _projectName = parts[1];
            TravisCiId = parts[2];
        }

        public override string Id { get { return _id; } }
        public override string Name { get { return _projectName; } }
        public string OwnerName { get; private set; }
        public string ProjectName { get { return _projectName; } }
        public string TravisCiId { get; private set; }

        public static TravisCiBuildDefinition FromJson(string json)
        {
            var slug = TravisCiService.GetJsonValue(json, "slug");
            var id = TravisCiService.GetJsonValue(json, "id");
            return new TravisCiBuildDefinition(slug + "/" + id);
        }

        public static TravisCiBuildDefinition FromIdString(string id)
        {
            return new TravisCiBuildDefinition(id);
        }
    }
}
