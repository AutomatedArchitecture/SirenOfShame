using System;

using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Watcher
{
    public class BuildStatusDto
    {
        public string BuildDefinitionDisplayName { get; set; }
        public string BuildDefinitionId { get; set; }
        public string BuildId { get; set; }
        public BuildStatusEnum BuildStatusEnum { get; set; }
        public string BuildStatusMessage { get; set; }
        public string Comment { get; set; }
        public string Duration { get; set; }
        public int ImageIndex { get; set; }
        public DateTime LocalStartTime { get; set; }
        public string RequestedByDisplayName { get; set; }
        public string RequestedByRawName { get; set; }
        public string StartTimeShort { get; set; }
        public string Url { get; set; }

        public void SetDisplayName(SirenOfShameSettings settings)
        {
            var person = settings.FindAddPerson(RequestedByRawName);
            RequestedByDisplayName = person == null ? RequestedByRawName : person.DisplayName;
        }
    }
}
