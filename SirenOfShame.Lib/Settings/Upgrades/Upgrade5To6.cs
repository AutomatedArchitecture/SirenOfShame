using System.Linq;

namespace SirenOfShame.Lib.Settings.Upgrades
{
    public class Upgrade5To6 : UpgradeBase
    {
        private readonly int _avatarCount;

        public Upgrade5To6(int avatarCount)
        {
            _avatarCount = avatarCount;
        }

        public override int ToVersion
        {
            get { return 6; }
        }

        public override void Upgrade(SirenOfShameSettings sirenOfShameSettings)
        {
            for (int index = 0; index < sirenOfShameSettings.People.Count; index++)
            {
                var personSetting = sirenOfShameSettings.People[index];
                personSetting.AvatarId = index % _avatarCount;
            }
        }
    }
}
