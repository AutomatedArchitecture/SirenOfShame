using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        
        public void Write(BuildStatus buildStatus, SirenOfShameSettings settings)
        {
            AppendToFile(buildStatus);
            UpdateStatsInSettings(buildStatus, settings);
        }

        private static void UpdateStatsInSettings(BuildStatus buildStatus, SirenOfShameSettings settings)
        {
            if (!string.IsNullOrEmpty(buildStatus.RequestedBy))
            {
                var personSetting = settings.FindAddPerson(buildStatus.RequestedBy);
                if (buildStatus.BuildStatusEnum == BuildStatusEnum.Broken)
                {
                    personSetting.FailedBuilds++;
                }
                personSetting.TotalBuilds++;
                settings.Save();
            }
        }

        private void AppendToFile(BuildStatus buildStatus)
        {
            string[] items = new[]
            {
                buildStatus.StartedTime == null ? "" : buildStatus.StartedTime.Value.Ticks.ToString(),
                buildStatus.FinishedTime == null ? "" : buildStatus.FinishedTime.Value.Ticks.ToString(),
                ((int) buildStatus.BuildStatusEnum).ToString(),
                buildStatus.RequestedBy,
            };
            string contents = string.Join(",", items) + "\r\n";
            string location = GetLocation(buildStatus.BuildDefinitionId);
            Write(location, contents);
        }

        private char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
        
        protected static string RemoveIllegalCharacters(string s)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()) + "\\.";
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(s, "");
        }
        
        private string GetLocation(string buildDefinitionId)
        {
            return folder + "\\" + RemoveIllegalCharacters(buildDefinitionId) + ".txt";
        }

        public List<BuildStatus> ReadAll(BuildDefinitionSetting buildDefinitionSetting)
        {
            string location = GetLocation(buildDefinitionSetting.Id);
            if (!File.Exists(location)) return new List<BuildStatus>();
            var lines = File.ReadAllLines(location);
            var statuses = lines.Select(l => l.Split(','))
                .Where(l => l.Length == 4) // just in case there are partially written records
                .Select(l => new BuildStatus
                {
                    StartedTime = string.IsNullOrEmpty(l[0]) ? (DateTime?)null : new DateTime(long.Parse(l[0])),
                    FinishedTime = string.IsNullOrEmpty(l[1]) ? (DateTime?)null : new DateTime(long.Parse(l[1])),
                    BuildStatusEnum = (BuildStatusEnum)int.Parse(l[2]),
                    RequestedBy = l[3]
                })
                .ToList();
            return statuses;
        }
    }
}