using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Google
{
    public class ParStrategy : BaseTagStrategy
    {
        public ParStrategy() : base("par", SsmlPlatform.Google)
        {
        }

        public override IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var error = VerifyContainsOnlySpecificElements(element, new List<string>
            {
                "par", "seq", "media"
            });

            if (error != null)
            {
                yield return error;
            }
        }
    }
}