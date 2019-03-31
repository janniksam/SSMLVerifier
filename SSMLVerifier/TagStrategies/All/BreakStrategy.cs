﻿using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class BreakStrategy : BaseTagStrategy
    {
        private const string AttributeNameStrength = "strength";
        private const string AttributeNameTime = "time";

        private static readonly string[] m_validStrenghts =
        {
            "none", "x-weak", "weak", "medium", "strong", "x-strong"
        };

        public BreakStrategy() : base("break")
        {
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var verificationResult =
                VerifyHasOnlySpecificAttributes(element, null, new[] {AttributeNameStrength, AttributeNameTime});
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = RequiresAttribute(element, AttributeNameStrength, null, a => VerifyValues(a, m_validStrenghts), true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = RequiresAttribute(element, AttributeNameTime, optional: true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            // todo: Check time format

            return VerificationResult.Valid;
        }
    }
}