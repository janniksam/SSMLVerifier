using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.Amazon;

namespace SSMLVerifierTests.TagStrategies.Amazon
{
    [TestClass]
    public class AmazonEffectStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<amazon:effect name=\"whispered\" />".ToXElement();
            var strategy = new AmazonEffectStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidTag()
        {
            var element = "<amazon:effect name1=\"bla\" />".ToXElement();
            var strategy = new AmazonEffectStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.MissingAttribute, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttributeValue()
        {
            var element = "<amazon:effect name=\"InvalidContent\" />".ToXElement();
            var strategy = new AmazonEffectStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }
    }
}