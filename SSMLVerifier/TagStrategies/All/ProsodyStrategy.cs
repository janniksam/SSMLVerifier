using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class ProsodyStrategy : BaseTagStrategy
    {
        private const string AttributeNameRate = "rate";
        private const string AttributeNamePitch = "pitch";
        private const string AttributeNameVolume = "volume";

        private readonly string[] m_validRates = {"x-slow", "slow", "medium", "fast", "x-fast"};
        private const string RegExRate = "^([2-8][0-9]|9[0-9]|1[0-9]{2}|200)%$";

        private readonly string[] m_validPitches = {"x-low", "low", "medium", "high", "x-high"};
        private readonly string m_regExPitch;

        private readonly string[] m_validVolumes = {"silent", "x-soft", "soft", "medium", "loud", "x-loud"};
        private readonly string m_regExVolume;


        public ProsodyStrategy() : base("prosody")
        {
            m_regExPitch = $"^(([+-]\\d+)(\\.\\d{{1,2}})?%)|{string.Join("|", m_validPitches)}$";
            m_regExVolume = $"^([+-]\\d+(\\.\\d+)?dB)|{string.Join("|", m_validVolumes)}$";
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var verificationResult =
                VerifyHasOnlySpecificAttributes(element, null,
                    new[] {AttributeNameRate, AttributeNamePitch, AttributeNameVolume});
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = RequiresAttribute(element, AttributeNameRate, null, VerifyRate, true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = RequiresAttribute(element, AttributeNamePitch, null,
                a => VerifyMatchesRegEx(a, m_regExPitch), true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = RequiresAttribute(element, AttributeNameVolume, null,
                a => VerifyMatchesRegEx(a, m_regExVolume), true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            // todo: Check time format

            return VerificationResult.Valid;
        }

        private VerificationResult VerifyRate(XAttribute attribute)
        {
            if (m_validRates.Any(p => p == attribute.Value))
            {
                return VerificationResult.Valid;
            }

            return VerifyMatchesRegEx(attribute, RegExRate);
        }

        private VerificationResult VerifyPitch(XAttribute attribute)
        {
            if (m_validPitches.Any(p => p == attribute.Value))
            {
                return VerificationResult.Valid;
            }

            return VerifyMatchesRegEx(attribute, m_regExPitch);
        }
    }
}