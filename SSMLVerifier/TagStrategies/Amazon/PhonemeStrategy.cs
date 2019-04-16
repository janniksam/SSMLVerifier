using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Amazon
{
    public class PhonemeStrategy : BaseTagStrategy
    {
        private static readonly string[] m_validAlphabets =
        {
            "ipa", "x-sampa"
        };

        public PhonemeStrategy() : base("phoneme", SsmlPlatform.Amazon)
        {
            
        }

        public override IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var error = RequiresAttribute(element, "alphabet", null, a => VerifyValues(a, m_validAlphabets));
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, "ph");
            if (error != null)
            {
                yield return error;
            }
        }
    }
}