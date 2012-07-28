using System;
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
        
        public DateTime EventDate { get; set; }
        public PersonBase Person { get; set; }
        public string Title { get; set; }
        public ImageList AvatarImageList { get; set; }
        public NewsItemTypeEnum NewsItemType { get; set; }
        public int? ReputationChange { get; set; }

        public string AsCommaSeparated()
        {
            return string.Format("{0},{1},{2},{3},{4}", EventDate.Ticks, Person.RawName, (int)NewsItemType, ReputationChange, Title);
        }

        public static NewNewsItemEventArgs FromCommaSeparated(string commaSeparated, SirenOfShameSettings settings)
        {
            try
            {
                var elements = commaSeparated.Split(',');
                if (elements.Length < 3)
                {
                    _log.Error("Found a news item with fewer than three elements" + commaSeparated);
                    return null;
                }
                var eventDate = GetEventDate(elements);
                var person = GetPerson(settings, elements);
                if (person == null) return null;
                var newsItemType = GetNewsItemType(elements);
                var reputationChange = GetReputationChange(elements);
                var title = GetTitle(elements);
                return new NewNewsItemEventArgs
                {
                    EventDate = eventDate,
                    Person = person,
                    Title = title,
                    NewsItemType = newsItemType,
                    ReputationChange = reputationChange,
                };
            } 
            catch (Exception ex)
            {
                _log.Error("Error parsing news item: " + commaSeparated, ex);
                return null;
            }
        }

        private static int? GetReputationChange(string[] elements)
        {
            var reputationChangeRaw = elements[3];
            if (string.IsNullOrEmpty(reputationChangeRaw)) return null;
            return int.Parse(reputationChangeRaw);
        }

        private static NewsItemTypeEnum GetNewsItemType(string[] elements)
        {
            var newsItemTypeRaw = elements[2];
            var newsItemTypeInt = int.Parse(newsItemTypeRaw);
            return (NewsItemTypeEnum) newsItemTypeInt;
        }

        private static string GetTitle(string[] elements)
        {
            var title = string.Join(",", elements.Skip(4));
            return title;
        }

        private static DateTime GetEventDate(string[] elements)
        {
            var eventDateTicks = long.Parse(elements[0]);
            var eventDate = new DateTime(eventDateTicks);
            return eventDate;
        }

        private static PersonSetting GetPerson(SirenOfShameSettings settings, string[] elements)
        {
            var rawName = elements[1];
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