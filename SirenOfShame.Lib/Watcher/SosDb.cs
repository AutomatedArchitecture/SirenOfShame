using System.IO;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Watcher
{
    public class SosDb
    {
        private string folder = SirenOfShameSettings.GetSosAppDataFolder();

        protected virtual void Write(string location, string contents)
        {
            File.AppendAllText(location, contents);
        }
        
        public void Write(BuildStatus buildStatus)
        {
            string[] items = new []
            {
                buildStatus.StartedTime == null ? "" : buildStatus.StartedTime.Value.Ticks.ToString(),
                buildStatus.FinishedTime == null ? "" : buildStatus.FinishedTime.Value.Ticks.ToString(),
                ((int)buildStatus.BuildStatusEnum).ToString(),
                buildStatus.RequestedBy,
            };
            string contents = string.Join(",", items) + "\r\n";
            Write(folder + "\\" + buildStatus.BuildDefinitionId + ".txt", contents);
        }
    }
}