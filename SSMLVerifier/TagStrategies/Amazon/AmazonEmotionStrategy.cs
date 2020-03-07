using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Amazon
{
    public class AmazonEmotionStrategy : BaseTagStrategy
    {
        private const string AttributeNameName = "name";
        private const string AttributeNameIntensity = "intensity";
        private static readonly string[] m_validEmotions =
        {
            "excited",
            "disappointed"
        };
        private static readonly string[] m_validIntensity =
        {
            "low",
            "medium",
            "high"
        };

        public AmazonEmotionStrategy() : base("amazon:emotion", SsmlPlatform.Amazon)
        {
        }

        public override IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var error = RequiresAttribute(element, AttributeNameName, null, v => VerifyValues(v, m_validEmotions));
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, AttributeNameIntensity, null, v => VerifyValues(v, m_validIntensity));
            if (error != null)
            {
                yield return error;
            }
        }
    }
}