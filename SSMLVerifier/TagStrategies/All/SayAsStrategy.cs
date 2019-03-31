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

        public SayAsStrategy() : base("say-as")
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

            verificationResult = RequiresAttribute(element, AttributeNameFormat, null, optional: true);
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

        /*
         // Attribute must be interpret-as or format
  attributes.forEach((attribute) => {
    if (attribute === 'interpret-as') {
      if (['characters', 'spell-out', 'cardinal', 'ordinal',
          'fraction', 'unit', 'date', 'time', 'telephone', 'expletive']
          .indexOf(element.attributes['interpret-as']) === -1) {
        // Some attributes are platform specific
        let supported = false;
        if ((platform === 'amazon') &&
          ['number', 'digits', 'address', 'interjection']
          .indexOf(element.attributes['interpret-as'] !== -1)) {
          supported = true;
        } else if ((platform === 'google') &&
          ['bleep', 'verbatim'].indexOf(element.attributes['interpret-as'] !== -1)) {
          supported = true;
        }

        if (!supported) {
          errors.push(createTagError(element, attribute));
        }
      }
    } else if (attribute === 'format') {
      if (['mdy', 'dmy', 'ymd', 'md', 'dm', 'ym',
          'my', 'd', 'm', 'y'].indexOf(element.attributes.format) === -1) {
        errors.push(createTagError(element, attribute));
      }
    } else if ((platform === 'google') && (attribute === 'detail')) {
      if (['1', '2'].indexOf(element.attributes.detail) === -1) {
        errors.push(createTagError(element, attribute));
      }
    } else {
      // Invalid attribute
      errors.push(createTagError(element, attribute, true));
    }
  });
break;
         */
    }
}