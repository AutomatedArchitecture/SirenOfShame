using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    public class SosDbFake : SosDb
    {
        public Dictionary<string, string> Files = new Dictionary<string, string>();
        
        protected override void Write(string location, string contents)
        {
            string existingFile;
            Files.TryGetValue(location, out existingFile);
            if (existingFile == null) existingFile = "";
            string newContents = existingFile + contents;
            Files[location] = newContents;
        }

        public IEnumerable<BuildStatus> BuildStatuses { get; set; }
        
        protected override IEnumerable<BuildStatus> ReadAllInternal(Lib.Settings.BuildDefinitionSetting buildDefinitionSetting)
        {
            return BuildStatuses;
        }

        public override IList<BuildStatus> ReadAll(IEnumerable<Lib.Settings.BuildDefinitionSetting> buildDefinitionSettings)
        {
            return BuildStatuses.ToList();
        }
    }
}