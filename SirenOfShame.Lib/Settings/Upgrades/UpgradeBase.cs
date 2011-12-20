namespace SirenOfShame.Lib.Settings.Upgrades
{
    public abstract class UpgradeBase
    {
        public abstract int ToVersion { get; }
        public virtual int? FromVersion
        {
            get { return ToVersion - 1; }
        }
        public abstract void Upgrade(SirenOfShameSettings sirenOfShameSettings);
    }
}
