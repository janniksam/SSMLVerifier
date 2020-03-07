using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Amazon
{
    public class AmazonDomainStrategy : BaseTagStrategy
    {
        private const string AttributeNameName = "name";
        private static readonly string[] m_validSpeakingStyles =
        {
            "music",
            "news"
        };

        public AmazonDomainStrategy() : base("amazon:domain", SsmlPlatform.Amazon)
        {
        }

        public override IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var error = RequiresAttribute(element, AttributeNameName, null, v => VerifyValues(v, m_validSpeakingStyles));
            if (error != null)
            {
                yield return error;
            }
        }
    }
}