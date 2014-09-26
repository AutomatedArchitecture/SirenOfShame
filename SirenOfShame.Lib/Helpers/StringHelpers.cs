using System;

namespace SirenOfShame.Lib.Helpers
{
    public static class StringHelpers
    {
        public static string RemoveUrlPrefix(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;
            const int lengthOfHttpPrefix = 7;
            if (url.Length < lengthOfHttpPrefix) return url;
            return url.Substring(lengthOfHttpPrefix);
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
