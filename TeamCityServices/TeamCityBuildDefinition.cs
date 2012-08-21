using System;
using System.Xml.Linq;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;

namespace TeamCityServices
{
    // <buildType id="bt2" name="Test Project 1 Build" href="/httpAuth/app/rest/buildTypes/id:bt2"
    //            projectName="Test Project 1" projectId="project2" webUrl="http://localhost:8080/viewType.html?buildTypeId=bt2"/>
    [Serializable]
    public class TeamCityBuildDefinition : MyBuildDefinition
    {
        private readonly string _id;
        private readonly string _name;

        public string RootUrl { get; set; }
        public override string Id { get { return _id; } }
        public override string Name { get { return _name; } }

        public TeamCityBuildDefinition(string rootUrl, XElement buildTypeXml)
        {
            RootUrl = rootUrl;
            _id = buildTypeXml.AttributeValueOrDefault("id");
            _name = buildTypeXml.AttributeValueOrDefault("name");
        }
    }
}