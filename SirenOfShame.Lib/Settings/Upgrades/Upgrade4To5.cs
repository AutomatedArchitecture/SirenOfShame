using System.Linq;

namespace SirenOfShame.Lib.Settings.Upgrades
{
    public class Upgrade4To5 : UpgradeBase
    {
        public override int ToVersion
        {
            get { return 5; }
        }

        public override void Upgrade(SirenOfShameSettings sirenOfShameSettings)
        {
            if (sirenOfShameSettings.GetAllActiveBuildDefinitions().Any())
            {
                sirenOfShameSettings.TryToFindOldAchievementsAtNextOpportunity = true;
            }
        }
    }
}
