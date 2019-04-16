using System.Collections.Generic;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class SubStrategy : BaseTagStrategy
    {
        public SubStrategy() : base("sub", SsmlPlatform.All)
        {
        }

        public override IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var error = VerifyHasOnlySpecificAttributes(element, null, new []{ "alias" });
            if (error != null)
            {
                yield return error;
            }
        }
    }
}