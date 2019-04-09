using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class AudioStrategy : BaseTagStrategy
    {
        private const string RegularExpressionSoundLevel = "^[+-](([0-9](\\.\\d{1,2})?)|([1-3][0-9](\\.\\d{1,2})?)|40)dB$";
        private const string RegularExpressionSpeed = "^\\d+%$";
        private const string RegularExpressionRepeatCount = "^[+]?\\d+(\\.\\d+)?$";

        public AudioStrategy() : base("audio")
        {
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var verificationResult = RequiresAttribute(element, "src");
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = VerifyGoogleSpecificAttributes(element, platform);
            if (verificationResult != null)
            {
                return verificationResult;
            }
            
            return VerificationResult.Valid;
        }

        private VerificationResult VerifyGoogleSpecificAttributes(XElement element, SsmlPlatform platform)
        {
            if (platform != SsmlPlatform.Google)
            {
                return null;
            }

            //todo Time Verification
            var verificationResult = RequiresAttribute(element, "clipBegin", optional: true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            //todo Time Verification
            verificationResult = RequiresAttribute(element, "clipEnd", optional: true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = RequiresAttribute(element, "speed",
                attributeValidationFunc: a => VerifyMatchesRegEx(a, RegularExpressionSpeed), optional: true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = RequiresAttribute(element, "repeatCount",
                attributeValidationFunc: a => VerifyMatchesRegEx(a, RegularExpressionRepeatCount), optional: true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            //todo Time Verification
            verificationResult = RequiresAttribute(element, "repeatDur", optional: true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult = RequiresAttribute(element, "soundLevel",
                attributeValidationFunc: a => VerifyMatchesRegEx(a, RegularExpressionSoundLevel), optional: true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            return null;
        }
    }
}