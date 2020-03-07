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
            new AmazonEmotionStrategy(),
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
        public IEnumerable<SSMLValidationError> Verify(string input, SsmlPlatform platform = SsmlPlatform.All)
        {
            XElement element;
            try
            {
                element = input.ToXElement();
            }
            catch (XmlException ex)
            { 
                throw new XmlException("The given SSML is malformed.", ex);
            }

            var error = VerifyRootTag(element);
            if (error != null)
            {
                yield return error;
            }

            foreach (var childElement in element.Elements())
            {
                foreach (var childError in Verify(childElement, platform))
                {
                    yield return childError;
                }
            }
        }

        private SSMLValidationError VerifyRootTag(XElement element)
        {
            if (element.Name.LocalName != RootTagName)
            {
                return new SSMLValidationError(VerificationState.InvalidTag, $"The root-element has to be \"{RootTagName}\"");
            }

            if (element.Attributes().Any())
            {
                return new SSMLValidationError(VerificationState.InvalidAttribute,
                    $"The element with the name {RootTagName} should not have any attributes.");
            }

            return null;
        }

        private IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var elementName = element.GetNameWithAlias();

            var tagStrategy = m_strategies.FirstOrDefault(
                p => p.IsResponsibleFor(elementName) &&
                     p.IsValidForPlatform(platform));
            if (tagStrategy == null)
            {
                yield return new SSMLValidationError(VerificationState.InvalidTag, $"Invalid tag {elementName}");
                yield break;
            }

            foreach (var error in tagStrategy.Verify(element, platform))
            {
                yield return error;
            }

            foreach (var childElement in element.Elements())
            {
                foreach (var childError in Verify(childElement, platform))
                {
                    yield return childError;
                }
            }
        }
    }
}