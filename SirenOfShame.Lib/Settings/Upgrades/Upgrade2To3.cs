namespace SirenOfShame.Lib.Settings.Upgrades
{
    /// <summary>
    /// aka upgrade to 1.4.0
    /// </summary>
    public class Upgrade2To3 : UpgradeBase
    {
        public override int ToVersion
        {
            get { return 3; }
        }

        public override void Upgrade(SirenOfShameSettings sirenOfShameSettings)
        {
            foreach (var rule in sirenOfShameSettings.Rules)
            {
                if (rule.TriggerType == TriggerType.InitialFailedBuild || rule.TriggerType == TriggerType.SubsequentFailedBuild || rule.TriggerType == TriggerType.BuildFailed)
                {
                    rule.WindowsAudioLocation = "SirenOfShame.Resources.Sad-Trombone.wav";
                }
                if (rule.TriggerType == TriggerType.BuildTriggered)
                {
                    rule.WindowsAudioLocation = "SirenOfShame.Resources.Plunk.wav";
                }
                if (rule.TriggerType == TriggerType.InitialSuccess)
                {
                    rule.WindowsAudioLocation = "SirenOfShame.Resources.applause-moderate-01.wav";
                }
            }
        }
    }
}
