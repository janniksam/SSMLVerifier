using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.All;

namespace SSMLVerifierTests.TagStrategies.All
{
    [TestClass]
    public class PStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<p><p></p></p>".ToXElement();
            var strategy = new PStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttribute()
        {
            var element = "<p name=\"test\"></p>".ToXElement();
            var strategy = new PStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttribute, verificationResult.State);
        }
    }
}