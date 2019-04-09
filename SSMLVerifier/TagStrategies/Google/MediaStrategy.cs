using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Google
{
    public class MediaStrategy : BaseTagStrategy
    {
        private const string RegExId = "^([-_#]|[a-z]|[A-Z]|ß|ö|ä|ü|Ö|Ä|Ü|æ|é|[0-9])+$";
        private const string RegularExpressionRepeatCount = "^[+]?\\d+(\\.\\d+)?$";

        private const string RegularExpressionSoundLevel =
            "^[+-](([0-9](\\.\\d{1,2})?)|([1-3][0-9](\\.\\d{1,2})?)|40)dB$";

        public MediaStrategy() : base("media")
        {
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var verificationResult = RequiresAttribute(element, "id", "xml", a => VerifyMatchesRegEx(a, RegExId), true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult =
                RequiresAttribute(element, "begin", null, VerifyTimeDesignation, true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult =
                RequiresAttribute(element, "end", null, VerifyTimeDesignation, true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult =
                RequiresAttribute(element, "repeatCount", null,
                    a => VerifyMatchesRegEx(a, RegularExpressionRepeatCount), true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult =
                RequiresAttribute(element, "repeatDur", null, VerifyTimeDesignation, true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult =
                RequiresAttribute(element, "soundLevel", null, a => VerifyMatchesRegEx(a, RegularExpressionSoundLevel),
                    true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult =
                RequiresAttribute(element, "fadeInDur", null, VerifyTimeDesignation, true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            verificationResult =
                RequiresAttribute(element, "fadeOutDur", null, VerifyTimeDesignation, true);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            return VerificationResult.Valid;
        }
    }
}