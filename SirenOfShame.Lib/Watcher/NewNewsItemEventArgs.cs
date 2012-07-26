using System;
using System.Linq;
using SirenOfShame.Lib.Settings;
using log4net;

namespace SirenOfShame.Lib.Watcher
{
    public class NewNewsItemEventArgs
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(NewNewsItemEventArgs));
        
        public DateTime EventDate { get; set; }
        public PersonSetting Person { get; set; }
        public string Title { get; set; }

        public string AsCommaSeparated()
        {
            return string.Format("{0},{1},{2}", EventDate.Ticks, Person.RawName, Title);
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
                var eventDateTicks = long.Parse(elements[0]);
                var eventDate = new DateTime(eventDateTicks);
                var rawName = elements[1];
                var person = settings.FindPersonByRawName(rawName);
                var title = string.Join(",", elements.Skip(2));
                if (person == null)
                {
                    _log.Error("Unable to find person from news item: " + rawName);
                    return null;
                }
                return new NewNewsItemEventArgs
                {
                    EventDate = eventDate,
                    Person = person,
                    Title = title
                };
            } 
            catch (Exception ex)
            {
                _log.Error("Error parsing news item: " + commaSeparated, ex);
                return null;
            }
        }
    }
}