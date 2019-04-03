using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        protected VerificationResult RequiresAttribute(XElement element, string attributeName, string prefix = null, Func<XAttribute, VerificationResult> attributeValidationFunc = null, bool optional = false)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            if (string.IsNullOrWhiteSpace(attributeName))
            {
                throw new ArgumentNullException(nameof(attributeName));
            }

            var attribute = GetAttribute(element, attributeName, prefix);
            if (attribute == null && optional)
            {
                return null;
            }

            if (attribute == null)
            {
                return new VerificationResult(VerificationState.MissingAttribute,
                    $"The element {TagName} doesnt include a {attributeName}-attribute");
            }

            var attributeValidationResult = attributeValidationFunc?.Invoke(attribute);
            if (attributeValidationResult != null &&
                attributeValidationResult.State != VerificationState.Valid)
            {
                return attributeValidationResult;
            }

            return null;
        }

        private static XAttribute GetAttribute(XElement element, string attributeName, string prefix)
        {
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

            return attribute;
        }

        protected VerificationResult VerifyHasOnlySpecificAttributes(XElement element, string prefix, IReadOnlyCollection<string> validAttributeNames)
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
                        $"The element {TagName} can only have the following attributes: {string.Join(",", validAttributeNames)}, but there was a {xAttribute.Name.LocalName}");
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
                        $"The element {TagName} can only have the following attributes: {string.Join(",", validAttributeNames)}, but there was a {xAttribute.Name.LocalName}");
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
                    $"The element {TagName} can only contain the following elements: {string.Join(",", validTags)}, but there was a {xElement.Name.LocalName}");
            }

            return null;
        }

        protected VerificationResult VerifyNoAttributesAllowed(XElement element)
        {
            if (element.HasAttributes)
            {
                return new VerificationResult(VerificationState.InvalidAttribute,
                    $"The element with the name {TagName} should not have any attributes.");
            }

            return null;
        }

        protected VerificationResult VerifyValues(XAttribute attribute, IReadOnlyCollection<string> validValues)
        {
            if (!validValues.Contains(attribute.Value))
            {
                return CreateInvalidAttributeValueResult(attribute, validValues);
            }

            return null;
        }

        protected VerificationResult VerifyHasValidParent(XObject element, string validParent)
        {
            if (element.Parent == null || element.Parent.Name.LocalName != validParent)
            {
                return new VerificationResult(VerificationState.InvalidParent, $"The element {TagName} can only be used inside an {validParent} element.");
            }

            return null;
        }

        protected VerificationResult VerifyMatchesRegEx(XAttribute attribute, string regularExpression)
        {
            var regex = new Regex(regularExpression);
            if (regex.IsMatch(attribute.Value))
            {
                return VerificationResult.Valid;
            }

            return new VerificationResult(VerificationState.InvalidAttributeValue,
                $"The element {TagName} does include a {attribute.Name.LocalName}-attribute, but the value {attribute.Value} is invalid.\r\n" +
                $"The value has to match the regular expression: \"{regularExpression}\"");
        }


        private VerificationResult CreateInvalidAttributeValueResult(XAttribute attribute, IEnumerable<string> validAttributes)
        {
            return new VerificationResult(VerificationState.InvalidAttributeValue,
                $"The element {TagName} does include a {attribute.Name.LocalName}-attribute, but the value {attribute.Value} is invalid.\r\n" +
                $"Valid values are: \"{string.Join(",", validAttributes)}\"");
        }

        public virtual bool IsResponsibleFor(string tag)
        {
            return TagName.Equals(tag);
        }

        public abstract VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All);
    }
}