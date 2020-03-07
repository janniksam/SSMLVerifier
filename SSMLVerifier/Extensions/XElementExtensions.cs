using System.Xml.Linq;

namespace SSMLVerifier.Extensions
{
    public static class XElementExtensions
    {
        public static string GetNameWithAlias(this XElement element)
        {
            var elementLocalName = element.Name.LocalName;
            var elementNamespaceAlias = element.GetPrefixOfNamespace(element.Name.Namespace);
            if (string.IsNullOrEmpty(elementNamespaceAlias))
            {
                return elementLocalName;
            }

            return $"{elementNamespaceAlias}:{elementLocalName}";
        }
    }
}
