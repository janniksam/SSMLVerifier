using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Amazon
{
    public class VoiceStrategy : BaseTagStrategy
    {
        private static readonly string[] m_validVoiceNames =
        {
            "Ivy", "Joanna", "Joey", "Justin", "Kendra", "Kimberly", "Matthew", "Salli",
            "Nicole", "Russell", "Amy", "Brian", "Emma", "Aditi", "Raveena",
            "Hans", "Marlene", "Vicki", "Conchita", "Enrique",
            "Carla", "Giorgio", "Mizuki", "Takumi", "Celine", "Lea", "Mathieu"
        };

        public VoiceStrategy() : base("voice")
        {
        }

        public override VerificationResult Verify(XElement element)
        {
            var verificationResult = RequiresAttribute(element, "name", null, m_validVoiceNames);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            return VerificationResult.Valid;
        }
    }
}