using System.Collections.Generic;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Google
{
    public class DescStrategy : BaseTagStrategy
    {
        private const string TagNameAudio = "audio";

        public DescStrategy() : base("desc", SsmlPlatform.Google)
        {
        }

        public override IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var error = VerifyHasValidParent(element, TagNameAudio);
            if (error != null)
            {
                yield return error;
            }
        }
    }
}