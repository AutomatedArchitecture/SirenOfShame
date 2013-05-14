using System;

namespace SirenOfShame.Lib.Helpers
{
    public static class PrettyDateHelper
    {
        public static string PrettyDate(this DateTime dateTime)
        {
            return PrettyDate(dateTime, DateTime.Now);
        }
        
        public static string PrettyDate(this DateTime dateTime, DateTime now)
        {
            var distance = now - dateTime;
            if (distance.TotalMinutes < 1) return "just now";
            if (distance.TotalMinutes < 2) return "1 minute ago";
            if (distance.TotalHours < 1) return (int)distance.TotalMinutes + " minutes ago";
            if (distance.TotalHours < 2) return "1 hour ago";
            if (distance.TotalDays < 1) return (int)distance.TotalHours + " hours ago";
            if (distance.TotalDays < 2) return "yesterday";
            if (distance.TotalDays < 7) return (int) distance.TotalDays + " days ago";
            if (distance.TotalDays < 14) return "last week";
            if (distance.TotalDays < 35) return (int)(distance.TotalDays / 7) + " weeks ago";
            if (distance.TotalDays < 365)
            {
                var monthsAgo = (int) (distance.TotalDays/30);
                return string.Format("{0} month{1} ago", monthsAgo, monthsAgo == 1 ? "" : "s");
            }
            return dateTime.ToString("d");
        }
    }
}
