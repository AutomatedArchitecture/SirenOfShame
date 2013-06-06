using System.Linq;

namespace SirenOfShame.Lib.Settings.Upgrades
{
    public class Upgrade7To8 : UpgradeBase
    {
        public override int ToVersion
        {
            get { return 8; }
        }

        public override void Upgrade(SirenOfShameSettings settings)
        {
            var isUpgradingFromPreviousVersion = settings.GetAllActiveBuildDefinitions().Any();
            bool isntAlreadyUsingMyCi = settings.SosOnlineWhatToSync != WhatToSyncEnum.BuildStatuses;
            if (isUpgradingFromPreviousVersion && isntAlreadyUsingMyCi)
            {
                settings.ShowUpgradeWindowAtNextOpportunity = true;
            }
        }
    }
}
