using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;
using log4net;

namespace SirenOfShame.Lib.Watcher
{
    public enum NewsItemTypeEnum
    {
        SosOnlineReputationChange = 1,
        SosOnlineNewAchievement = 2,
        SosOnlineComment = 3,
        SosOnlineNewMember= 4,
        SosOnlineMisc = 5,
        BuildStarted = 100,
        BuildSuccess = 101,
        BuildFailed = 102,
        NewAchievement = 103,
        BuildUnknown = 104,
    }
    
    public class NewNewsItemEventArgs
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(NewNewsItemEventArgs));
        
        public string BuildDefinitionId { get; set; }
        public DateTime EventDate { get; set; }
        public PersonBase Person { get; set; }
        public string Title { get; set; }
        public ImageList AvatarImageList { get; set; }
        public NewsItemTypeEnum NewsItemType { get; set; }
        public int? ReputationChange { get; set; }
        public string BuildId { get; set; }
        
        public bool IsSosOnlineEvent
        {
            get { return NewsItemType < NewsItemTypeEnum.BuildStarted; }
        }

        public bool ShouldUpdateOldInProgressNewsItem
        {
            get
            {
                return !string.IsNullOrEmpty(BuildId) && (
                    NewsItemType == NewsItemTypeEnum.BuildFailed ||
                    NewsItemType == NewsItemTypeEnum.BuildSuccess ||
                    NewsItemType == NewsItemTypeEnum.BuildUnknown
                    );
            }
        }

        public static string MakeCsvSafe(string s)
        {
            return string.IsNullOrEmpty(s) ? "" : RemoveNewlines(s.Replace(',', ' '));
        }

        private static string RemoveNewlines(string s)
        {
            return string.IsNullOrEmpty(s) ? "" : s.Replace("\r\n", " ").Replace("\n", " ");
        }

        public string AsCommaSeparated()
        {
            try
            {
                return string.Format("{0},{1},{2},{3},{4},{5},{6}",
                                     EventDate.Ticks,
                                     MakeCsvSafe(Person.RawName),
                                     (int) NewsItemType,
                                     ReputationChange,
                                     MakeCsvSafe(BuildDefinitionId),
                                     MakeCsvSafe(BuildId),
                                     RemoveNewlines(Title));
            } 
            catch (Exception ex)
            {
                _log.Error("Failed to serialize news item", ex);
                return null;
            }
        }

        public static NewNewsItemEventArgs FromCommaSeparated(string commaSeparated, SirenOfShameSettings settings)
        {
            try
            {
                var elements = commaSeparated.Split(',');
                if (elements.Length < 6)
                {
                    _log.Error("Found a news item with fewer than three elements" + commaSeparated);
                    return null;
                }
                var eventDate = GetEventDate(elements[0]);
                var person = GetPerson(settings, elements[1]);
                if (person == null) throw new Exception("Unable to find user " + elements[1]);
                var newsItemType = GetNewsItemType(elements[2]);
                var reputationChange = GetReputationChange(elements[3]);
                var buildDefinitionId = GetString(elements[4]);
                var buildId = GetString(elements[5]);
                var title = GetTitle(elements, 6);
                return new NewNewsItemEventArgs
                {
                    EventDate = eventDate,
                    Person = person,
                    Title = title,
                    NewsItemType = newsItemType,
                    ReputationChange = reputationChange,
                    BuildId = buildId,
                    BuildDefinitionId = buildDefinitionId
                };
            } 
            catch (Exception ex)
            {
                _log.Error("Error parsing news item: " + commaSeparated, ex);
                return null;
            }
        }

        private static string GetString(string element)
        {
            if (string.IsNullOrEmpty(element)) return null;
            return element;
        }

        private static int? GetReputationChange(string element)
        {
            var reputationChangeRaw = element;
            if (string.IsNullOrEmpty(reputationChangeRaw)) return null;
            return int.Parse(reputationChangeRaw);
        }

        private static NewsItemTypeEnum GetNewsItemType(string element)
        {
            var newsItemTypeRaw = element;
            var newsItemTypeInt = int.Parse(newsItemTypeRaw);
            return (NewsItemTypeEnum) newsItemTypeInt;
        }

        private static string GetTitle(IEnumerable<string> elements, int commentStart)
        {
            var title = string.Join(",", elements.Skip(commentStart));
            return title;
        }

        private static DateTime GetEventDate(string element)
        {
            var eventDateTicks = long.Parse(element);
            var eventDate = new DateTime(eventDateTicks);
            return eventDate;
        }

        public static PersonSetting GetPerson(SirenOfShameSettings settings, string element)
        {
            var rawName = element;
            var person = settings.FindPersonByRawName(rawName);
            if (person == null)
            {
                _log.Error("Unable to find person from news item: " + rawName);
                return null;
            }
            return person;
        }
    }
}