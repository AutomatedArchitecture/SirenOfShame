using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Helpers
{
    public static class NewsItemHelper
    {
        public static IEnumerable<NewNewsItemEventArgs> FilterOutOvercomeByEventsNewsItems(this IEnumerable<NewNewsItemEventArgs> newsItemEvents)
        {
            foreach (IGrouping<string, NewNewsItemEventArgs> i1 in newsItemEvents.GroupBy(i => i.BuildId))
                yield return i1.First();
        }
    }
}
