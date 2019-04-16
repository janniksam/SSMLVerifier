using System.Collections.Generic;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class SStrategy : BaseTagStrategy
    {
        public SStrategy() : base("s", SsmlPlatform.All)
        {
        }

        public override IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var error = VerifyNoAttributesAllowed(element);
            if (error != null)
            {
                yield return error;
            }
        }
    }
}