namespace SirenOfShame.Lib.Services
{
    public class ApiResultBase
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}