namespace SoxLib
{
    public class FileInfo
    {
        public int? Channels { get; set; }
        public int? SamplingRate { get; set; }
        public EncodingType? EncodingType { get; set; }
        public int? SampleSizeInBits { get; set; }
        public FileType? FileType { get; set; }
    }
}
