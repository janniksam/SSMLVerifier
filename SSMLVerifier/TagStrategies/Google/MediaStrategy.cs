using System.Collections.Generic;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Google
{
    public class MediaStrategy : BaseTagStrategy
    {
        private const string RegExId = "^([-_#]|[a-z]|[A-Z]|ß|ö|ä|ü|Ö|Ä|Ü|æ|é|[0-9])+$";
        private const string RegularExpressionRepeatCount = "^[+]?\\d+(\\.\\d+)?$";

        private const string RegularExpressionSoundLevel =
            "^[+-](([0-9](\\.\\d{1,2})?)|([1-3][0-9](\\.\\d{1,2})?)|40)dB$";

        public MediaStrategy() : base("media", SsmlPlatform.Google)
        {
        }

        public override IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var error = RequiresAttribute(element, "id", "xml", a => VerifyMatchesRegEx(a, RegExId), true);
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, "begin", null, VerifyTimeDesignation, true);
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, "end", null, VerifyTimeDesignation, true);
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, "repeatCount", null,
                    a => VerifyMatchesRegEx(a, RegularExpressionRepeatCount), true);
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, "repeatDur", null, VerifyTimeDesignation, true);
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, "soundLevel", null, a => VerifyMatchesRegEx(a, RegularExpressionSoundLevel),
                    true);
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, "fadeInDur", null, VerifyTimeDesignation, true);
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, "fadeOutDur", null, VerifyTimeDesignation, true);
            if (error != null)
            {
                yield return error;
            }
        }
    }
}