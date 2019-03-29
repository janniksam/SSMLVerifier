using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.Amazon;

namespace SSMLVerifierTests.TagStrategies.Amazon
{
    [TestClass]
    public class VoiceStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<voice name=\"Kendra\">I am not a real human.</voice>".ToXElement();
            var strategy = new VoiceStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForMissingAttribute()
        {
            var element = "<voice anoherName=\"Kendra\">I am not a real human.</voice>".ToXElement();
            var strategy = new VoiceStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.MissingAttribute, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttributeValue()
        {
            var element = "<voice name=\"Karlheinz\">I am not a real human.</voice>".ToXElement();
            var strategy = new VoiceStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }
    }
}