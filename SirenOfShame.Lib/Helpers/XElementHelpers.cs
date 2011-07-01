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
    }
}
