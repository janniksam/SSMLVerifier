using System.Collections.Generic;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Amazon
{
    public class AmazonEffectStrategy : BaseTagStrategy
    {
        public AmazonEffectStrategy() : base("amazon:effect")
        {
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var verificationResult = RequiresAttribute(element, "name", null, new List<string> {"whispered"});
            if (verificationResult != null)
            {
                return verificationResult;
            }

            return VerificationResult.Valid;
        }
    }
}