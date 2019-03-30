using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Amazon
{
    public class WStrategy : BaseTagStrategy
    {
        private static readonly string[] m_validRoles =
        {
            "amazon:VB", "amazon:VBD", "amazon:NN", "amazon:SENSE_1"
        };

        public WStrategy() : base("w")
        {
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var verificationResult = RequiresAttribute(element, "role", null, m_validRoles);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            return VerificationResult.Valid;
        }
    }
}