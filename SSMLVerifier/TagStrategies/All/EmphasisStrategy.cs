using System.Collections.Generic;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class EmphasisStrategy : BaseTagStrategy
    {
        private const string AttributeNameLevel = "level";

        private readonly List<string> m_validLevels = new List<string>
        {
            "strong", "moderate", "reduced"
        };

        public EmphasisStrategy() : base("emphasis")
        {
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            if (platform == SsmlPlatform.Google)
            {
                m_validLevels.Add("none");
            }

            var verificationResult = RequiresAttribute(element, AttributeNameLevel, null, a => VerifyValues(a, m_validLevels));
            if (verificationResult != null)
            {
                return verificationResult;
            }
            return VerificationResult.Valid;
        }
    }
}