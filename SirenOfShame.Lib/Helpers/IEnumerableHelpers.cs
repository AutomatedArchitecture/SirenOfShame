using System.Collections.Generic;
using System.Linq;

namespace SirenOfShame.Lib.Helpers
{
    public static class IEnumerableHelpers
    {
        public static string JoinAsString(this IEnumerable<string> items, string separator)
        {
            return string.Join(separator, items.ToArray());
        }
    }
}
