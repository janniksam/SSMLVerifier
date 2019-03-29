using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies
{
    public abstract class BaseTagStrategy : ITagStrategy    
    {
        protected BaseTagStrategy(string tagName)
        {
            TagName = tagName;
        }

        protected string TagName { get; }

        protected VerificationResult RequiresAttribute(XElement element, string attributeName, string prefix = null, IReadOnlyCollection<string> validValues = null)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            if (string.IsNullOrWhiteSpace(attributeName))
            {
                throw new ArgumentNullException(nameof(attributeName));
            }

            var xAttributes = element.Attributes();
            XAttribute attribute;
            if (prefix == null)
            {
                attribute = xAttributes.FirstOrDefault(p => p.Name.LocalName == attributeName);
            }
            else
            {
                var namespaceOfPrefix = element.GetNamespaceOfPrefix(prefix);
                attribute = xAttributes.FirstOrDefault(p => p.Name.LocalName == attributeName &&
                                                            p.Name.Namespace == namespaceOfPrefix);
            }

            if (attribute == null)
            {
                return new VerificationResult(VerificationState.MissingAttribute,
                    $"The tag {TagName} doesnt include a {attributeName}-attribute");
            }

            if (validValues != null &&
                !validValues.Contains(attribute.Value))
            {
                return new VerificationResult(VerificationState.InvalidAttributeValue,
                    $"The tag {TagName} does include a {attributeName}-attribute, but the value {attribute.Value} is invalid.\r\n" +
                    $"Valid values are: \"{string.Join(",", validValues)}\"");
            }

            return null;
        }

        public VerificationResult VerifyContainsOnlySpecificElements(XElement element, List<string> validTags)
        {
            var xElements = element.Elements();
            foreach (var xElement in xElements)
            {
                if (validTags.Contains(xElement.Name.LocalName))
                {
                    continue;
                }

                return new VerificationResult(
                    VerificationState.ContainerContainsInvalidChilds,
                    $"The attribute {TagName} can only the following elements: {string.Join(",", validTags)}, but there was a {xElement.Name.LocalName}");
            }

            return null;
        }

        public virtual bool IsResponsibleFor(string tag)
        {
            return TagName.Equals(tag);
        }

        public abstract VerificationResult Verify(XElement element);
    }
}