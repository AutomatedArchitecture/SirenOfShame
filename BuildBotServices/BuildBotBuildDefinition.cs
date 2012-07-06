using System;
using System.Xml.Linq;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using System.Collections.Generic;

namespace BuildBotServices
{
    /*
     * {
     *      runtests: 
     *      {
     *          basedir: "runtests",
     *          cachedBuilds: 
     *          [
     *              0,
     *              1,
     *              2,
     *              3
     *          ],
     *          schedulers: 
     *          [
     *              "all",
     *              "force"
     *          ],
     *          slaves: 
     *          [
     *              "example-slave"
     *          ],
     *          state: "idle"
     *      }
     * }
     */

    public class BuildBotBuildersJSONQuery
    {
        public string BaseDir { get; set; }
        public List<int> CachedBuilds { get; set; }
        public string Category { get; set; }
        public List<int> CurrentBuilds { get; set; }
        public List<string> Schedulers { get; set; }
        public List<string> Slaves { get; set; }
        public string State { get; set; }
    }


    public class BuildBotBuildDefinition : MyBuildDefinition
    {
        private readonly string _id;
        private readonly string _name;

        public string RootUrl { get; set; }
        public override string Id { get { return _id; } }
        public override string Name { get { return _name; } }

        public BuildBotBuildDefinition(string rootUrl, string name)
        {
            RootUrl = rootUrl;
            _name = _id = name;
        }
    }
}
