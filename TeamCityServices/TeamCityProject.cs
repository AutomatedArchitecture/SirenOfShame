using System;
using System.Xml.Linq;
using SirenOfShame.Lib.Helpers;

namespace TeamCityServices
{
    // <project name="Test Project 1" id="project2" href="/httpAuth/app/rest/projects/id:project2"/>
    public class TeamCityProject
    {
        public string RootUrl { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Href { get; set; }

        public TeamCityProject(string rootUrl, XElement projectXml)
        {
            RootUrl = rootUrl;
            Name = projectXml.AttributeValueOrDefault("name");
            Id = projectXml.AttributeValueOrDefault("id");
            Href = projectXml.AttributeValueOrDefault("href");
        }
    }
}