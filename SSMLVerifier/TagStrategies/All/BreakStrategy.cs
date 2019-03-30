using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class BreakStrategy : BaseTagStrategy
    {
        private static readonly string[] m_validStrenghts =
        {
            "none", "x-weak", "weak", "medium", "strong", "x-strong"
        };

        public BreakStrategy() : base("break")
        {
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var verificationResult = VerifyHasOnlySpecificAttributes(element, null, new []{ "strength", "time" });
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = RequiresAttribute(element, "strength", null, m_validStrenghts, true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = RequiresAttribute(element, "time", optional: true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            // todo: Check time format

            return VerificationResult.Valid;
        }
    }
}