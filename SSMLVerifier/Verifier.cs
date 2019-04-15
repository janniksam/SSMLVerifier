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
            new AudioStrategy(),
            new BreakStrategy(),
            new EmphasisStrategy(),
            new ProsodyStrategy(),
            new PStrategy(),
            new SayAsStrategy(),
            new SStrategy(),
            new SubStrategy(),
            //Amazon
            new AmazonEffectStrategy(),
            new LangStrategy(),
            new PhonemeStrategy(),
            new VoiceStrategy(),
            new WStrategy(),
            //Google
            new DescStrategy(),
            new MediaStrategy(),
            new ParStrategy(),
            new SeqStrategy()
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
            var tagStrategy = m_strategies.FirstOrDefault(
                p => p.IsResponsibleFor(element.Name.LocalName) &&
                     p.IsValidForPlatform(platform));
            if (tagStrategy == null)
            {
                return new VerificationResult(VerificationState.InvalidTag, $"Invalid tag {element.Name.LocalName}");
            }

            var verificationResult = tagStrategy.Verify(element, platform);
            if (verificationResult?.State != VerificationState.Valid)
            {
                return verificationResult;
            }

            foreach (var childElement in element.Elements())
            {
                var childVerificationResult = Verify(childElement, platform);
                if (childVerificationResult?.State != VerificationState.Valid)
                {
                    return childVerificationResult;
                }
            }
            
            return VerificationResult.Valid;
        }
    }
}