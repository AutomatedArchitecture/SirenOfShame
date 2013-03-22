using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Test.Unit.Watcher
{
    public class SirenOfShameSettingsFake : SirenOfShameSettings {
        public SirenOfShameSettingsFake() : base(false)
        {
        }

        public override void Save()
        {
            // do nothing
        }

        public void DoUpgrade()
        {
            TryUpgrade();
        }
    }
}
