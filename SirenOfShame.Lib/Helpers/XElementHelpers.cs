using System;
using System.Xml.Linq;

namespace SirenOfShame.Lib.Helpers
{
    public static class XElementHelpers
    {
        public static string AttributeValueOrDefault(this XElement elem, XName name)
        {
            var attr = elem.Attribute(name);
            return attr == null ? null : attr.Value;
        }

        public static string ElementValueOrDefault(this XElement elem, XName name)
        {
            var childEleme = elem.Element(name);
            return childEleme == null ? null : childEleme.Value;
        }

        public static bool? ElementValueAsBool(this XElement elem, XName name, bool? defaultVal)
        {
            var val = ElementValueOrDefault(elem, name);
            if (string.IsNullOrWhiteSpace(val))
            {
                return defaultVal;
            }
            bool result;
            if (bool.TryParse(val, out result))
            {
                return result;
            }
            throw new Exception("Could not parse or read element " + name);
        }
    }
}
