using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies;
using SSMLVerifier.TagStrategies.All;
using SSMLVerifier.TagStrategies.Amazon;
using SSMLVerifier.TagStrategies.Google;

namespace SSMLVerifier
{
    public class Verifier
    {
        private const string RootTagName = "speak";

        private readonly List<ITagStrategy> m_strategies = new List<ITagStrategy>
        {
            new AmazonEffectStrategy(),
            new LangStrategy(),
            new PhonemeStrategy(),
            new VoiceStrategy(),
            new WStrategy(),
            new ParStrategy(),
            new SeqStrategy(),
            new PStrategy(),
            new SStrategy(),
            new SubStrategy(),
            new BreakStrategy(),
            new EmphasisStrategy(),
            new AudioStrategy()
        };

        /// <summary>
        /// Verifies the input ssml.
        /// </summary>
        /// <param name="input">The SSML to check</param>
        /// <param name="platform">The platform to eveluate the SSML against</param>
        /// <returns>The result of the verification</returns>
        public VerificationResult Verify(string input, SsmlPlatform platform = SsmlPlatform.All)
        {
            XElement xElement;
            try
            {
                xElement = input.ToXElement();
            }
            catch (XmlException ex)
            { 
                throw new XmlException("The given SSML is malformed.", ex);
            }

            var verificationResult = VerifyRootTag(xElement);
            if (verificationResult?.State != VerificationState.Valid)
            {
                return verificationResult;
            }

            foreach (var childElement in xElement.Elements())
            {
                var childVerificationResult = Verify(childElement, platform);
                if (childVerificationResult?.State != VerificationState.Valid)
                {
                    return childVerificationResult;
                }
            }

            return VerificationResult.Valid;
        }

        private VerificationResult VerifyRootTag(XElement element)
        {
            if (element.Name.LocalName != RootTagName)
            {
                return new VerificationResult(VerificationState.InvalidTag, $"The root-element has to be \"{RootTagName}\"");
            }

            if (element.Attributes().Any())
            {
                return new VerificationResult(VerificationState.InvalidAttribute,
                    $"The element with the name {RootTagName} should not have any attributes.");
            }

            return VerificationResult.Valid;
        }

        private VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var validTags = GetValidTags(platform);
            if (!validTags.Contains(element.Name.LocalName))
            {
                return new VerificationResult(VerificationState.InvalidTag, $"Invalid tag {element.Name.LocalName}");
            }

            var tagStrategy = m_strategies.FirstOrDefault(p => p.IsResponsibleFor(element.Name.LocalName));
            if (tagStrategy != null)
            {
                var verificationResult = tagStrategy.Verify(element, platform);
                if (verificationResult?.State != VerificationState.Valid)
                {
                    return verificationResult;
                }
            }

            foreach (var childElement in element.Elements())
            {
                var verificationResult = Verify(childElement, platform);
                if (verificationResult?.State != VerificationState.Valid)
                {
                    return verificationResult;
                }
            }
            
            return VerificationResult.Valid;
        }

        // todo Make this a responsibility of each strategy instead of deciding it here
        private static IEnumerable<string> GetValidTags(SsmlPlatform platform)
        {
            var validTags = new List<string>
            {
                "audio",
                "break",
                "emphasis",
                "p",
                "prosody",
                "s",
                "say-as",
                "speak",
                "sub"
            };

            if (platform == SsmlPlatform.Amazon)
            {
                validTags.AddRange(new[]
                {
                    "amazon:effect", "lang", "phoneme", "voice", "w"
                });
            }

            if (platform == SsmlPlatform.Google)
            {
                validTags.AddRange(new[]
                {
                    "par", "seq", "media"
                });
            }

            return validTags;
        }
    }
}