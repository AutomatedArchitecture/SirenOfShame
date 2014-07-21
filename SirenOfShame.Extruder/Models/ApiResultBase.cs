namespace SirenOfShame.Extruder.Models
{
    public class ApiResultBase
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public object Result { get; set; }
    }
}
