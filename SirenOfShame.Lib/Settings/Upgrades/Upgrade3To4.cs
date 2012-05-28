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
            // todo: add this back in
            //FindOldAchievements.TryFindOldAchievements(sirenOfShameSettings);
        }
    }
}
