using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Amazon
{
    public class AmazonEffectStrategy : BaseTagStrategy
    {
        private const string AttributeNameName = "name";
        private static readonly string[] m_validEffects =
        {
            "whispered"
        };

        public AmazonEffectStrategy() : base("amazon:effect", SsmlPlatform.Amazon)
        {
        }

        public override IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var error = RequiresAttribute(element, AttributeNameName, null, v => VerifyValues(v, m_validEffects));
            if (error != null)
            {
                yield return error;
            }
        }
    }
}