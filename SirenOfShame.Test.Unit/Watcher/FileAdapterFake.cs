using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    public class FileAdapterFake : FileAdapter
    {
        public Dictionary<string, string> Files = new Dictionary<string, string>();

        public override bool Exists(string location)
        {
            return Files.ContainsKey(location);
        }
        
        public override void AppendAllText(string location, string newRow)
        {
            if (!Files.ContainsKey(location))
            {
                Files[location] = "";
            }
            Files[location] += newRow;
        }

        public override IEnumerable<string> ReadAllLines(string location)
        {
            var result = Files[location]
                .TrimEnd('\n')
                .Split('\n')
                .Select(i => i.TrimEnd('\r'))
                .ToArray();
            return result;
        }
    }
}
