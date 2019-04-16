using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies
{
    public abstract class BaseTagStrategy : ITagStrategy    
    {
        protected BaseTagStrategy(string tagName, SsmlPlatform platform)
        {
            SupportedPlatform = platform;
            TagName = tagName;
        }

        protected SsmlPlatform SupportedPlatform { get; }

        protected string TagName { get; }

        public virtual bool IsValidForPlatform(SsmlPlatform platform)
        {
            return SupportedPlatform == SsmlPlatform.All ||
                   platform == SupportedPlatform;
        }

        public virtual bool IsResponsibleFor(string tag)
        {
            return TagName.Equals(tag);
        }

        protected SSMLValidationError RequiresAttribute(XElement element, string attributeName, string prefix = null, Func<XAttribute, SSMLValidationError> attributeValidationFunc = null, bool optional = false)
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
                return new SSMLValidationError(VerificationState.MissingAttribute,
                    $"The element {TagName} doesnt include a {attributeName}-attribute");
            }

            var attributeValidationResult = attributeValidationFunc?.Invoke(attribute);
            return attributeValidationResult;
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

        protected SSMLValidationError VerifyHasOnlySpecificAttributes(XElement element, string prefix, IReadOnlyCollection<string> validAttributeNames)
        {
            if (prefix == null)
            {
                foreach (var xAttribute in element.Attributes())
                {
                    if (validAttributeNames.Contains(xAttribute.Name.LocalName))
                    {
                        continue;
                    }

                    return new SSMLValidationError(
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

                    return new SSMLValidationError(
                        VerificationState.InvalidAttribute,
                        $"The element {TagName} can only have the following attributes: {string.Join(",", validAttributeNames)}, but there was a {xAttribute.Name.LocalName}");
                }
            }

            return null;
        }

        protected SSMLValidationError VerifyContainsOnlySpecificElements(XElement element, List<string> validTags)
        {
            var xElements = element.Elements();
            foreach (var xElement in xElements)
            {
                if (validTags.Contains(xElement.Name.LocalName))
                {
                    continue;
                }

                return new SSMLValidationError(
                    VerificationState.ContainerContainsInvalidChilds,
                    $"The element {TagName} can only contain the following elements: {string.Join(",", validTags)}, but there was a {xElement.Name.LocalName}");
            }

            return null;
        }

        protected SSMLValidationError VerifyNoAttributesAllowed(XElement element)
        {
            if (element.HasAttributes)
            {
                return new SSMLValidationError(VerificationState.InvalidAttribute,
                    $"The element with the name {TagName} should not have any attributes.");
            }

            return null;
        }

        protected SSMLValidationError VerifyValues(XAttribute attribute, IReadOnlyCollection<string> validValues)
        {
            if (!validValues.Contains(attribute.Value))
            {
                return CreateInvalidAttributeValueResult(attribute, validValues);
            }

            return null;
        }

        protected SSMLValidationError VerifyHasValidParent(XObject element, string validParent)
        {
            if (element.Parent == null || element.Parent.Name.LocalName != validParent)
            {
                return new SSMLValidationError(VerificationState.InvalidParent, $"The element {TagName} can only be used inside an {validParent} element.");
            }

            return null;
        }

        protected SSMLValidationError VerifyMatchesRegEx(XAttribute attribute, string regularExpression)
        {
            var regex = new Regex(regularExpression);
            if (regex.IsMatch(attribute.Value))
            {
                return null;
            }

            return new SSMLValidationError(VerificationState.InvalidAttributeValue,
                $"The element {TagName} does include a {attribute.Name.LocalName}-attribute, but the value {attribute.Value} is invalid.\r\n" +
                $"The value has to match the regular expression: \"{regularExpression}\"");
        }

        protected SSMLValidationError VerifyTimeDesignation(XAttribute attribute)
        {
            var verificationResult = VerifyMatchesRegEx(attribute, "^[+-]?\\d+(\\.\\d+)?(h|min|s|ms)$");
            if (verificationResult == null)
            {
                return null;
            }

            return VerifyMatchesRegEx(attribute,
                "^([-_#]|[a-z]|[A-Z]|ß|ö|ä|ü|Ö|Ä|Ü|æ|é|[0-9])+\\.(begin|end)[+-][0-9]+(\\.[0-9]+)?(h|min|s|ms)$");
        }


        private SSMLValidationError CreateInvalidAttributeValueResult(XAttribute attribute, IEnumerable<string> validAttributes)
        {
            return new SSMLValidationError(VerificationState.InvalidAttributeValue,
                $"The element {TagName} does include a {attribute.Name.LocalName}-attribute, but the value {attribute.Value} is invalid.\r\n" +
                $"Valid values are: \"{string.Join(",", validAttributes)}\"");
        }

        public abstract IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All);
    }
}