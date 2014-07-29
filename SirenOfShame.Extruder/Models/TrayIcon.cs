namespace SirenOfShame.Extruder.Models
{
    public enum TrayIcon
    {
        Red,
        Green,
        Question
    }
    
    public class SetTrayIconEventArgs
    {
        public TrayIcon TrayIcon { get; set; }
    }
}
