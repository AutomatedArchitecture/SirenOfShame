using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Watcher
{
    public class BuildStatusDto
    {
        public int ImageIndex { get; set; }
        public string Comment { get; set; }
        public string StartTime { get; set; }
        public long StartTimeTicks { get; set; }
        public string Duration { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedByDisplayName { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string BuildId { get; set; }
        public BuildStatusEnum BuildStatusEnum { get; set; }

        public void SetDisplayName(SirenOfShameSettings settings)
        {
            RequestedByDisplayName = settings.TryGetDisplayName(RequestedBy);
        }
    }
}