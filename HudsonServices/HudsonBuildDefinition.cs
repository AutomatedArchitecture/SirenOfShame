using System.Xml.Linq;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;

namespace HudsonServices
{
    /*
     * <job>
     *   <name>TestProject1</name>
     *   <url>http://localhost:8080/hudson/job/TestProject1/</url>
     *   <color>red</color>
     * </job>
     */
    public class HudsonBuildDefinition : MyBuildDefinition
    {
        private readonly string _id;
        private readonly string _name;

        public string RootUrl { get; set; }
        public override string Id { get { return _id; } }
        public override string Name { get { return _name; } }

        public HudsonBuildDefinition(string rootUrl, XElement projectXml)
        {
            RootUrl = rootUrl;
            _id = _name = projectXml.ElementValueOrDefault("name");
        }
    }
}
