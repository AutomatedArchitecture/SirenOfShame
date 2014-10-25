namespace SirenOfShame.Extruder.Models
{
    public class ConnectExtruderModel : CredentialApiModel
    {
        public string Name { get; set; }
        public string LedPatterns { get; set; }
        public string AudioPatterns { get; set; }
    }
}