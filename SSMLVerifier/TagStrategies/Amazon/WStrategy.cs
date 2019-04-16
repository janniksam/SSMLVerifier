using System.Collections.Generic;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Amazon
{
    public class WStrategy : BaseTagStrategy
    {
        private static readonly string[] m_validRoles =
        {
            "amazon:VB", "amazon:VBD", "amazon:NN", "amazon:SENSE_1"
        };

        public WStrategy() : base("w", SsmlPlatform.Amazon)
        {
        }

        public override IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var error = RequiresAttribute(element, "role", null, a => VerifyValues(a, m_validRoles));
            if (error != null)
            {
                yield return error;
            }
        }
    }
}