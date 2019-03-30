using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class SStrategy : BaseTagStrategy
    {
        public SStrategy() : base("s")
        {
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var validationResult = VerifyNoAttributesAllowed(element);
            if (validationResult != null)
            {
                return validationResult;
            }

            return VerificationResult.Valid;
        }
    }
}