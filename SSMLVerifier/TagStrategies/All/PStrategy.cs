using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class PStrategy : BaseTagStrategy
    {
        public PStrategy() : base("p", SsmlPlatform.All)
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