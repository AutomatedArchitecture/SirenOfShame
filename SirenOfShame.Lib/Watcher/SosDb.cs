using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using SirenOfShame.Lib.Settings;
using log4net;

namespace SirenOfShame.Lib.Watcher
{
    public class SosDb
    {
        private readonly string _folder = SirenOfShameSettings.GetSosAppDataFolder();
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(SosDb));

        protected virtual void Write(string location, string contents)
        {
            try
            {
                File.AppendAllText(location, contents);
            } 
            catch (IOException ex)
            {
                _log.Error("Unable to write: " + contents + " to " + location, ex);
            }
        }
        
        public void Write(BuildStatus buildStatus, SirenOfShameSettings settings)
        {
            AppendToFile(buildStatus);
            UpdateStatsInSettings(buildStatus, settings);
        }

        private static void UpdateStatsInSettings(BuildStatus buildStatus, SirenOfShameSettings settings)
        {
            if (string.IsNullOrEmpty(buildStatus.RequestedBy)) return;
            var personSetting = settings.FindAddPerson(buildStatus.RequestedBy);
            if (buildStatus.BuildStatusEnum == BuildStatusEnum.Broken)
            {
                personSetting.FailedBuilds++;
            }
            personSetting.TotalBuilds++;
            settings.Save();
        }

        private void AppendToFile(BuildStatus buildStatus)
        {
            string[] items = new[]
            {
                buildStatus.StartedTime == null ? "" : buildStatus.StartedTime.Value.Ticks.ToString(CultureInfo.InvariantCulture),
                buildStatus.FinishedTime == null ? "" : buildStatus.FinishedTime.Value.Ticks.ToString(CultureInfo.InvariantCulture),
                ((int) buildStatus.BuildStatusEnum).ToString(CultureInfo.InvariantCulture),
                buildStatus.RequestedBy,
            };
            string contents = string.Join(",", items) + "\r\n";
            string location = GetBuildLocation(buildStatus);
            Write(location, contents);
        }

        private string GetBuildLocation(BuildStatus buildStatus)
        {
            return GetBuildLocation(buildStatus.BuildDefinitionId);
        }

        private static string RemoveIllegalCharacters(string s)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()) + "\\.";
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(s, "");
        }
        
        private string GetBuildLocation(BuildDefinitionSetting buildDefinition)
        {
            return GetBuildLocation(buildDefinition.Id);
        }
        
        private string GetEventsLocation()
        {
            return _folder + "\\SirenOfShameEvents.txt";
        }
        
        private string GetBuildLocation(string buildDefinitionId)
        {
            return _folder + "\\" + RemoveIllegalCharacters(buildDefinitionId) + ".txt";
        }

        public virtual IList<BuildStatus> ReadAll(IEnumerable<BuildDefinitionSetting> buildDefinitionSettings)
        {
            return buildDefinitionSettings
                .SelectMany(ReadAllInternal)
                .AsParallel() // since there is a File.ReadAllLines for each build definition this should parallelize nicely
                .ToList();
        }
        
        protected virtual IEnumerable<BuildStatus> ReadAllInternal(BuildDefinitionSetting buildDefinitionSetting)
        {
            string location = GetBuildLocation(buildDefinitionSetting);
            if (!File.Exists(location)) return new List<BuildStatus>();
            var lines = File.ReadAllLines(location);
            return lines.Select(l => l.Split(','))
                .Where(l => l.Length == 4) // just in case there are partially written records
                .Select(l => new BuildStatus
                {
                    StartedTime = string.IsNullOrEmpty(l[0]) ? (DateTime?) null : new DateTime(long.Parse(l[0])),
                    FinishedTime = string.IsNullOrEmpty(l[1]) ? (DateTime?) null : new DateTime(long.Parse(l[1])),
                    BuildStatusEnum = (BuildStatusEnum) int.Parse(l[2]),
                    RequestedBy = l[3],
                    BuildDefinitionId = buildDefinitionSetting.Id
                });
        }
        
        public IList<BuildStatus> ReadAll(string buildId)
        {
            var location = GetBuildLocation(buildId);
            return ReadAllFromLocation(location);
        }

        private IList<BuildStatus> ReadAllFromLocation(string location)
        {
            if (!File.Exists(location)) return new List<BuildStatus>();
            var lines = File.ReadAllLines(location);
            var statuses = lines.Select(l => l.Split(','))
                .Where(l => l.Length == 4) // just in case there are partially written records
                .Select(BuildStatus.Parse)
                .Where(i => i != null) // ignore parse errors
                .ToList();
            return statuses;
        }

        public IList<BuildStatus> ReadAll(BuildDefinitionSetting buildDefinitionSetting)
        {
            string location = GetBuildLocation(buildDefinitionSetting);
            return ReadAllFromLocation(location);
        }

        public string ExportNewBuilds(SirenOfShameSettings settings)
        {
            if (string.IsNullOrEmpty(settings.MyRawName)) return null;
            DateTime? highWaterMark = settings.GetHighWaterMark();
            var initialExport = highWaterMark == null;
            var allBuildDefinitions = ReadAll(settings.GetAllActiveBuildDefinitions());
            var currentUsersBuilds = allBuildDefinitions
                .Where(i => i.RequestedBy == settings.MyRawName)
                .Where(i => i.StartedTime != null);
            var buildsAfterHighWaterMark = initialExport ? currentUsersBuilds : currentUsersBuilds.Where(i => i.StartedTime > highWaterMark);
            var buildsAsExport = buildsAfterHighWaterMark.Select(i => i.AsSosOnlineExport());
            var result = string.Join("\r\n", buildsAsExport);
            return string.IsNullOrEmpty(result) ? null : result;
        }

        public void ExportNewNewsItem(NewNewsItemEventArgs args)
        {
            var location = GetEventsLocation();
            string asCommaSeparated = args.AsCommaSeparated();
            if (!string.IsNullOrEmpty(asCommaSeparated))
            {
                string contents = asCommaSeparated + "\r\n";
                try
                {
                    File.AppendAllText(location, contents);
                } 
                catch (IOException ex)
                {
                    _log.Error("Unable to export news item: " + contents, ex);
                }
            }
        }

        public void GetMostRecentNewsItems(SirenOfShameSettings settings, Action<IList<NewNewsItemEventArgs>> onGetNewsItems)
        {
            var location = GetEventsLocation();
            if (!File.Exists(location)) onGetNewsItems(new List<NewNewsItemEventArgs>());

            var context = TaskScheduler.FromCurrentSynchronizationContext();

            var newsItemGetter = new Task<List<NewNewsItemEventArgs>> (() => File.ReadAllLines(location)
                                                                                .Select(i => NewNewsItemEventArgs.FromCommaSeparated(i, settings))
                                                                                .Where(i => i != null)
                                                                                .Reverse()
                                                                                .ToList());
            newsItemGetter.ContinueWith(result => onGetNewsItems(result.Result), new CancellationToken(), TaskContinuationOptions.OnlyOnRanToCompletion, context);
            newsItemGetter.ContinueWith(t =>
            {
                if (t.Exception != null)
                {
                    var exception = t.Exception.InnerExceptions.First();
                    if (!(exception is FileNotFoundException))
                        _log.Error(exception);
                }
                onGetNewsItems(new List<NewNewsItemEventArgs>());
            }, new CancellationToken(), TaskContinuationOptions.OnlyOnFaulted, context);
            newsItemGetter.Start();
        }

        public void GetMostRecentNewsItems(SirenOfShameSettings settings, int newsItemsToGet, Action<IEnumerable<NewNewsItemEventArgs>> onGetNewsItems)
        {
            GetMostRecentNewsItems(settings, newNewsItems => onGetNewsItems(newNewsItems.Take(newsItemsToGet)));
        }
    }
}