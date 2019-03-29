using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.Amazon;

namespace SSMLVerifierTests.TagStrategies.Amazon
{
    [TestClass]
    public class WStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<w role=\"amazon:VBD\">read</w>".ToXElement();
            var strategy = new WStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForMissingAttribute()
        {
            var element = "<w>read</w>".ToXElement();
            var strategy = new WStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.MissingAttribute, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttributeValue()
        {
            var element = "<w role=\"amazon:UNKNOWN\">read</w>".ToXElement();
            var strategy = new WStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }
    }
}