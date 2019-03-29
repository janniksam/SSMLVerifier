using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Amazon
{
    public class AmazonEffectStrategy : BaseTagStrategy
    {
        public AmazonEffectStrategy() : base("amazon:effect")
        {
        }

        public override VerificationResult Verify(XElement element)
        {
            var verificationResult = RequiresAttribute(element, "name", null, new List<string> {"whispered"});
            if (verificationResult != null)
            {
                return verificationResult;
            }

            return VerificationResult.Sucess;
        }
    }
}