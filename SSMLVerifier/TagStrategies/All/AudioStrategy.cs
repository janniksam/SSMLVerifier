using System.Collections.Generic;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class AudioStrategy : BaseTagStrategy
    {
        private const string RegularExpressionSoundLevel = "^[+-](([0-9](\\.\\d{1,2})?)|([1-3][0-9](\\.\\d{1,2})?)|40)dB$";
        private const string RegularExpressionSpeed = "^\\d+%$";
        private const string RegularExpressionRepeatCount = "^[+]?\\d+(\\.\\d+)?$";

        public AudioStrategy() : base("audio", SsmlPlatform.All)
        {
        }

        public override IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var error = RequiresAttribute(element, "src");
            if (error != null)
            {
                yield return error;
            }

            foreach (var result in VerifyGoogleSpecificAttributes(element, platform))
            {
                yield return result;
            }
        }

        private IEnumerable<SSMLValidationError> VerifyGoogleSpecificAttributes(XElement element, SsmlPlatform platform)
        {
            if (platform != SsmlPlatform.Google)
            {
                yield break;
            }

            var error = RequiresAttribute(element, "clipBegin", null, VerifyTimeDesignation, true);
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, "clipEnd", null, VerifyTimeDesignation, true);
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, "speed",
                attributeValidationFunc: a => VerifyMatchesRegEx(a, RegularExpressionSpeed), optional: true);
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, "repeatCount",
                attributeValidationFunc: a => VerifyMatchesRegEx(a, RegularExpressionRepeatCount), optional: true);
            if (error != null)
            {
                yield return error;
            }

            //todo Time Verification
            error = RequiresAttribute(element, "repeatDur", null, VerifyTimeDesignation, true);
            if (error != null)
            {
                yield return error;
            }

            error = RequiresAttribute(element, "soundLevel",
                attributeValidationFunc: a => VerifyMatchesRegEx(a, RegularExpressionSoundLevel), optional: true);
            if (error != null)
            {
                yield return error;
            }
        }
    }
}