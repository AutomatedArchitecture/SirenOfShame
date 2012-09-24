using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Settings.Upgrades
{
    public class Upgrade6To7 : UpgradeBase
    {
        public Upgrade6To7()
        {
        }

        public override int ToVersion
        {
            get { return 7; }
        }

        public override void Upgrade(SirenOfShameSettings settings)
        {
            SosDb sosDb = new SosDb();
            List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically = sosDb
                .ReadAll(settings.GetAllActiveBuildDefinitions())
                .OrderBy(i => i.StartedTime)
                .ToList();

            settings.People.ForEach(i => i.CalculateStats(allActiveBuildDefinitionsOrderedChronoligically));
        }
    }
}
