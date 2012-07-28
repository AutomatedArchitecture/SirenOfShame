using System;
using System.Xml.Linq;

namespace SirenOfShame.Lib.Helpers
{
    public static class XElementHelpers
    {
        public static string AttributeValueOrDefault(this XElement elem, XName name)
        {
            if (elem == null) return null;
            var attr = elem.Attribute(name);
            return attr == null ? null : attr.Value;
        }

        public static string AttributeValue(this XElement elem, XName name)
        {
            if (elem == null) return null;
            var attr = elem.Attribute(name);
            if (attr == null)
            {
                throw new Exception("Attribute '" + name + "' does not exist");
            }
            return attr.Value;
        }

        public static string ElementValueOrDefault(this XElement elem, XName name)
        {
            if (elem == null) return null;
            var childEleme = elem.Element(name);
            return childEleme == null ? null : childEleme.Value;
        }

        public static bool? ElementValueAsBool(this XElement elem, XName name, bool? defaultVal)
        {
            if (elem == null) return null;
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

        public static int AttributeValueAsInt(this XElement elem, XName name)
        {
            if (elem == null) throw new Exception("Could not find " + name + " because its parent was null");
            var val = AttributeValueOrDefault(elem, name);
            if (string.IsNullOrWhiteSpace(val)) throw new Exception(name + " did not contain a value");
            int result;
            if (int.TryParse(val, out result))
                return result;
            throw new Exception("Could not parse " + name + " because " + val + " was not an integer");
        }
    }
}
