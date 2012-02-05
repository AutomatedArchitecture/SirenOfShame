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
                if (rule.TriggerType == TriggerType.InitialFailedBuild || rule.TriggerType == TriggerType.BuildFailed)
                {
                    rule.WindowsAudioLocation = "SirenOfShame.Resources.Audio-Sad-Trombone.wav";
                }
                if (rule.TriggerType == TriggerType.SubsequentFailedBuild)
                {
                    rule.WindowsAudioLocation = "SirenOfShame.Resources.Audio-Boo-Hiss.wav";
                }
                if (rule.TriggerType == TriggerType.BuildTriggered)
                {
                    rule.WindowsAudioLocation = "SirenOfShame.Resources.Audio-Plunk.wav";
                }
                if (rule.TriggerType == TriggerType.InitialSuccess)
                {
                    rule.WindowsAudioLocation = "SirenOfShame.Resources.Audio-Applause.wav";
                }
            }
        }
    }
}
