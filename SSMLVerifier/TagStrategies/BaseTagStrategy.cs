using System;
using System.Collections.Generic;
using System.Linq;
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

        protected VerificationResult RequiresAttribute(XElement element, string attributeName, string prefix = null, IReadOnlyCollection<string> validValues = null, bool optional = false)
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
                attribute = xAttributes.SingleOrDefault(p => p.Name.LocalName == attributeName);
            }
            else
            {
                var namespaceOfPrefix = element.GetNamespaceOfPrefix(prefix);
                attribute = xAttributes.SingleOrDefault(p => p.Name.LocalName == attributeName &&
                                                             p.Name.Namespace == namespaceOfPrefix);
            }

            if (attribute == null && optional)
            {
                return null;
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

        protected VerificationResult HasOnlySpecificAttributes(XElement element, string prefix, string[] validAttributeNames)
        {
            if (prefix == null)
            {
                foreach (var xAttribute in element.Attributes())
                {
                    if (validAttributeNames.Contains(xAttribute.Name.LocalName))
                    {
                        continue;
                    }

                    return new VerificationResult(
                        VerificationState.InvalidAttribute,
                        $"The attribute {TagName} can only have the following attributes: {string.Join(",", validAttributeNames)}, but there was a {xAttribute.Name.LocalName}");
                }
            }
            else
            {
                var namespaceOfPrefix = element.GetNamespaceOfPrefix(prefix);
                foreach (var xAttribute in element.Attributes())
                {
                    if (validAttributeNames.Contains(xAttribute.Name.LocalName) &&
                        xAttribute.Name.Namespace == namespaceOfPrefix)
                    {
                        continue;
                    }

                    return new VerificationResult(
                        VerificationState.InvalidAttribute,
                        $"The attribute {TagName} can only have the following attributes: {string.Join(",", validAttributeNames)}, but there was a {xAttribute.Name.LocalName}");
                }
            }

            return null;
        }

        protected VerificationResult VerifyContainsOnlySpecificElements(XElement element, List<string> validTags)
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