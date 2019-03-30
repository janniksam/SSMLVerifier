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

        public PhonemeStrategy() : base("phoneme")
        {
            
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var verificationResult = RequiresAttribute(element, "alphabet", null, m_validAlphabets);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = RequiresAttribute(element, "ph");
            if (verificationResult != null)
            {
                return verificationResult;
            }

            return VerificationResult.Valid;
        }
    }
}