using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.Google;

namespace SSMLVerifierTests.TagStrategies.Google
{
    [TestClass]
    public class ParStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<par></par>".ToXElement();
            var strategy = new ParStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForValidTagWithChildMedia()
        {
            var element = "<par><media></media></par>".ToXElement();
            var strategy = new ParStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForValidTagWithChildSeq()
        {
            var element = "<par><seq></seq></par>".ToXElement();
            var strategy = new ParStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForValidTagWithChildAll()
        {
            var element = "<par><seq></seq><par></par><media></media></par>".ToXElement();
            var strategy = new ParStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForMissingAttribute()
        {
            var element = "<par><media></media><invalidTag></invalidTag></par>".ToXElement();
            var strategy = new ParStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.ContainerContainsInvalidChilds, verificationResult.State);
        }
    }
}