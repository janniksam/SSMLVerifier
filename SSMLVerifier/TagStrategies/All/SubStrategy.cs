using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class SubStrategy : BaseTagStrategy
    {
        public SubStrategy() : base("sub", SsmlPlatform.All)
        {
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var validationResult = VerifyHasOnlySpecificAttributes(element, null, new []{ "alias" });
            if (validationResult != null)
            {
                return validationResult;
            }

            return VerificationResult.Valid;
        }
    }
}