using System.Linq;
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
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnInvalidForMissingAttribute()
        {
            var element = "<voice anoherName=\"Kendra\">I am not a real human.</voice>".ToXElement();
            var strategy = new VoiceStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.MissingAttribute, errors.First().State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttributeValue()
        {
            var element = "<voice name=\"Karlheinz\">I am not a real human.</voice>".ToXElement();
            var strategy = new VoiceStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, errors.First().State);
        }
    }
}