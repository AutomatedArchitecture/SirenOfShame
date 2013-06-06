using System.Linq;
using System.Xml.Serialization;

namespace SirenOfShame.Lib.Settings.Upgrades
{
    public class Upgrade3To4 : UpgradeBase
    {
        public override int ToVersion
        {
            get { return 4; }
        }

        public override void Upgrade(SirenOfShameSettings sirenOfShameSettings)
        {
            if (sirenOfShameSettings.GetAllActiveBuildDefinitions().Any())
            {
                sirenOfShameSettings.ShowUpgradeWindowAtNextOpportunity = true;
            }
        }
    }
}
