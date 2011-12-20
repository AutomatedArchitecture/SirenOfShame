namespace SirenOfShame.Lib.Settings.Upgrades
{
    public class Upgrade1To2 : UpgradeBase
    {
        public override int ToVersion
        {
            get { return 2; }
        }

        public override void Upgrade(SirenOfShameSettings sirenOfShameSettings)
        {
            foreach (var person in sirenOfShameSettings.People)
            {
                person.Hidden = false;
            }
        }
    }
}
