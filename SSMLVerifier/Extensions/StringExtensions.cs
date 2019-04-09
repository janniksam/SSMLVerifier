using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace SSMLVerifier.Extensions
{
    public static class StringExtensions
    {
        public static XElement ToXElement(this string parsableXml)
        {
            var element = XElement.Parse($"<root xmlns:amazon='http://test.com'>{parsableXml}</root>");
            var elements = element.Elements().FirstOrDefault();
            return elements ?? throw new XmlException($"Could not parse SSML {parsableXml}");
        }
    }
}