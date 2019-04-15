using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class SayAsStrategy : BaseTagStrategy
    {
        private const string AttributeNameDetail = "detail";
        private const string AttributeNameInterpretAs = "interpret-as";
        private const string AttributeNameFormat = "format";
        private readonly IReadOnlyCollection<string> m_validInterpretAsValues = new List<string>
        {
            "characters",
            "spell-out",
            "cardinal",
            "ordinal",
            "fraction",
            "unit",
            "date",
            "time",
            "telephone",
            "expletive"
        };

        private readonly IReadOnlyCollection<string> m_validFormatValuesWhenInterpretAsIsDate = new List<string>
        {
            "mdy",
            "dmy",
            "ymd",
            "md",
            "dm",
            "ym",
            "my",
            "d",
            "m",
            "y"
        };
           
        public SayAsStrategy() : base("say-as", SsmlPlatform.All)
        {
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var validInterpretAsValues = GetValidInterpretAsValues(platform);
            var allowedAttributes = GetAllowedAttributes(platform);

            var verificationResult = VerifyHasOnlySpecificAttributes(element, null, allowedAttributes);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = RequiresAttribute(element, AttributeNameInterpretAs,
                attributeValidationFunc: a => VerifyValues(a, validInterpretAsValues), optional: true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = RequiresAttribute(element, AttributeNameDetail,
                attributeValidationFunc: a => VerifyValues(a, new [] {"1", "2"}), optional: true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = VerifyFormat(element, platform);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            return VerificationResult.Valid;
        }

        private IReadOnlyCollection<string> GetAllowedAttributes(SsmlPlatform platform)
        {
            var allowedAttributes = new List<string>
            {
                AttributeNameInterpretAs,
                AttributeNameFormat
            };

            if (platform == SsmlPlatform.Google)
            {
                allowedAttributes.Add(AttributeNameDetail);
            }

            return allowedAttributes;
        }

        private IReadOnlyCollection<string> GetValidInterpretAsValues(SsmlPlatform platform)
        {
            var validValues = m_validInterpretAsValues.ToList();
            if (platform == SsmlPlatform.Amazon)
            {
                validValues.AddRange(new[]
                {
                    "number", "digits", "address", "interjection"
                });
            }
            else if (platform == SsmlPlatform.Google)
            {
                validValues.AddRange(new[]
                {
                    "bleep", "verbatim"
                });
            }

            return validValues;
        }

        public VerificationResult VerifyFormat(XElement element, SsmlPlatform platform)  
        {
            var containsInterpretAsWithDateValue = RequiresAttribute(element, AttributeNameInterpretAs, null, a => VerifyValues(a, new[] {"date"})) == null;
            if (containsInterpretAsWithDateValue)
            {
                return RequiresAttribute(element, AttributeNameFormat, null, a => VerifyValues(a, m_validFormatValuesWhenInterpretAsIsDate));
            }

            if (platform == SsmlPlatform.Google)
            {
                return RequiresAttribute(element, AttributeNameFormat, null,
                    a => VerifyMatchesRegEx(a, "^[hmsZ^\\s.!?:;(12|24)]*$"));
            }

            return RequiresAttribute(element, AttributeNameFormat, null,
                a => new VerificationResult(VerificationState.InvalidAttribute,
                    //It doesn't matter what value the attribute has, it's not allowed when interpret-as != date...
                    $"The element {AttributeNameFormat} can only be used, when the \"{AttributeNameInterpretAs}\"-attribute is set to \"date\""),
                true);
        }
    }
}