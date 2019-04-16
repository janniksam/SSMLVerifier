using System.Collections.Generic;
using System.Linq;
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

        public BreakStrategy() : base("break", SsmlPlatform.All)
        {
        }

        public override IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var error =
                VerifyHasOnlySpecificAttributes(element, null, new[] {AttributeNameStrength, AttributeNameTime});
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, AttributeNameStrength, null, a => VerifyValues(a, m_validStrenghts), true);
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, AttributeNameTime, null, VerifyTimeDesignation, true);
            if (error != null)
            {
                yield return error;
            }
        }
    }
}