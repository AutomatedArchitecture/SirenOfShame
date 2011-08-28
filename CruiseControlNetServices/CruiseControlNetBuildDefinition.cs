using System;
using System.Xml.Linq;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;

namespace CruiseControlNetServices
{
    /*
     * <Project
     *   name="CruiseControlNetProj2"
     *   category=""
     *   activity="Sleeping"
     *   lastBuildStatus="Unknown"
     *   lastBuildLabel="UNKNOWN"
     *   lastBuildTime="2011-08-27T20:21:27.84375+03:00"
     *   nextBuildTime="2011-08-28T20:38:01.71875+03:00"
     *   webUrl="http://VMXP/ccnet"
     *   CurrentMessage=""
     *   BuildStage=""
     *   serverName="local"
     *   description="">
     *   <messages/>
     * </Project>
     */
    public class CruiseControlNetBuildDefinition : MyBuildDefinition
    {
        private readonly string _id;
        private readonly string _name;

        public string RootUrl { get; set; }
        public override string Id { get { return _id; } }
        public override string Name { get { return _name; } }

        public CruiseControlNetBuildDefinition(string rootUrl, XElement projectElem)
        {
            RootUrl = rootUrl;
            _name = _id = projectElem.AttributeValueOrDefault("name");
        }
    }
}
