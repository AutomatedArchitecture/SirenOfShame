using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Settings;

public class RuleDropDownItemTag
{
    public string WindowsAudioLocation { get; set; }
    public AlertType? AlertType { get; set; }
    public string BuildDefinitionId { get; set; }
    public string TriggerPerson { get; set; }
    public TriggerType TriggerType { get; set; }
    public int? Duration { get; set; }
    public LedPattern LedPattern { get; set; }
    public AudioPattern AudioPattern { get; set; }
}