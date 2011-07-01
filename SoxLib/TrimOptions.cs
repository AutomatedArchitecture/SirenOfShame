namespace SoxLib
{
    public class TrimOptions
    {
        public FileInfo InputFileInfo { get; set; }
        public FileInfo OutputFileInfo { get; set; }
        public SoxTime StartTime { get; set; }
        public SoxTime Length { get; set; }
        public SoxTime End { get; set; }
    }
}