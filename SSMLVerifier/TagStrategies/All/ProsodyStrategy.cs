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
        private readonly string m_regExRate;

        private readonly string[] m_validPitches = {"x-low", "low", "medium", "high", "x-high"};
        private readonly string m_regExPitch;

        private readonly string[] m_validVolumes = {"silent", "x-soft", "soft", "medium", "loud", "x-loud"};
        private readonly string m_regExVolume;


        public ProsodyStrategy() : base("prosody", SsmlPlatform.All)
        {
            m_regExRate = $"^(([2-8][0-9]|9[0-9]|1[0-9]{{2}}|200)%)|{string.Join("|", m_validRates)}$";
            m_regExPitch = $"^(([+-]\\d+)(\\.\\d{{1,2}})?%)|([+-]\\d+st)|{string.Join("|", m_validPitches)}$";
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

            verificationResult = RequiresAttribute(element, AttributeNameRate, null,
                a => VerifyMatchesRegEx(a, m_regExRate), true);
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

            return VerificationResult.Valid;
        }
    }
}