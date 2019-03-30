using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.All;

namespace SSMLVerifierTests.TagStrategies.All
{
    [TestClass]
    public class EmphasisStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<emphasis level=\"strong\" />".ToXElement();
             var strategy = new EmphasisStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForNoneLevelOnGooglePlatform()
        {
            var element = "<emphasis level=\"none\" />".ToXElement();
            var strategy = new EmphasisStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForNoneLevelOnAmazonPlatform()
        {
            var element = "<emphasis level=\"none\" />".ToXElement();
            var strategy = new EmphasisStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Amazon);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForMissingLevelAttribute()
        {
            var element = "<emphasis test=\"none\" />".ToXElement();
            var strategy = new EmphasisStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.MissingAttribute, verificationResult.State);
        }
    }
}