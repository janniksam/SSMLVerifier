using System.Collections.Generic;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Google
{
    public class SeqStrategy : BaseTagStrategy
    {
        public SeqStrategy() : base("seq", SsmlPlatform.Google)
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