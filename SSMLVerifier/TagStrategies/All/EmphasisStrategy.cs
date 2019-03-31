using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class EmphasisStrategy : BaseTagStrategy
    {
        private const string AttributeNameLevel = "level";

        private readonly IReadOnlyCollection<string> m_validLevels = new List<string>
        {
            "strong", "moderate", "reduced"
        };

        public EmphasisStrategy() : base("emphasis")
        {
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var validLevels = m_validLevels.ToList();
            if (platform == SsmlPlatform.Google)
            {
                validLevels.Add("none");
            }

            var verificationResult = RequiresAttribute(element, AttributeNameLevel, null, a => VerifyValues(a, validLevels));
            if (verificationResult != null)
            {
                return verificationResult;
            }
            return VerificationResult.Valid;
        }
    }
}