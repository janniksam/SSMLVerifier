using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.Amazon;

namespace SSMLVerifierTests.TagStrategies.Amazon
{
    [TestClass]
    public class LangStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<lang xml:lang=\"de-DE\" />".ToXElement();
            var strategy = new LangStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidTag()
        {
            var element = "<lang lang=\"de-DE\" />".ToXElement();
            var strategy = new LangStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.MissingAttribute, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttributeValue()
        {
            var element = "<lang xml:lang=\"not-valid\" />".ToXElement();
            var strategy = new LangStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }
    }
}