using System;
using System.Xml.Linq;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;

namespace BambooServices
{
    /*
     * <plan name="TestProject1 - Default" key="TESTPROJ1-DEF">
     *   <link rel="self" href="http://localhost:8080/bamboo/rest/api/latest/plan/TESTPROJ1-DEF"/>
     * </plan>
     */
    public class BambooBuildDefinition : MyBuildDefinition
    {
        private readonly string _id;
        private readonly string _name;

        public string RootUrl { get; set; }
        public string Url { get; set; }
        public override string Id { get { return _id; } }
        public override string Name { get { return _name; } }

        public BambooBuildDefinition(string rootUrl, XElement planXml)
        {
            RootUrl = rootUrl;
            _id = planXml.AttributeValueOrDefault("key");
            _name = planXml.AttributeValueOrDefault("name");
            var linkElem = planXml.Element("link");
            if (linkElem == null) throw new Exception("Could not get plan link");
            Url = linkElem.AttributeValueOrDefault("href");
        }
    }
}
