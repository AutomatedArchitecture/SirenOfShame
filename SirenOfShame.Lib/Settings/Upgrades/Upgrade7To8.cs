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
            if (settings.SosOnlineWhatToSync != WhatToSyncEnum.BuildStatuses)
            {
                settings.ShowUpgradeWindowAtNextOpportunity = true;
            }
        }
    }
}
