using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies;
using SSMLVerifier.TagStrategies.Amazon;

namespace SSMLVerifier
{
    public class Verifier
    {
        private readonly List<ITagStrategy> m_strategies = new List<ITagStrategy>
        {
            new AmazonEffectStrategy(),
            new LangStrategy(),
            new PhonemeStrategy(),
            new VoiceStrategy(),
            new WStrategy(),
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

            return Verify(xElement, platform);
        }

        private VerificationResult Verify(XElement ssmlElement, SsmlPlatform platform = SsmlPlatform.All)
        {
            var validTags = GetValidTags(platform);
            if (!validTags.Contains(ssmlElement.Name.LocalName))
            {
                return new VerificationResult(VerificationState.InvalidTag, $"Invalid tag {ssmlElement.Name.LocalName}");
            }

            var tagStrategy = m_strategies.FirstOrDefault(p => p.IsResponsibleFor(ssmlElement.Name.LocalName));
            if (tagStrategy != null)
            {
                var verificationResult = tagStrategy.Verify(ssmlElement);
                if (verificationResult != VerificationResult.Sucess)
                {
                    return verificationResult;
                }
            }

            foreach (var childElement in ssmlElement.Elements())
            {
                var verificationResult = Verify(childElement);
                if (verificationResult != VerificationResult.Sucess)
                {
                    return verificationResult;
                }
            }
            
            return VerificationResult.Sucess;
        }

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

            if (platform == SsmlPlatform.All || platform == SsmlPlatform.Amazon)
            {
                validTags.AddRange(new[]
                {
                    "amazon:effect", "lang", "phoneme", "voice", "w"
                });
            }

            if (platform == SsmlPlatform.All || platform == SsmlPlatform.Google)
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