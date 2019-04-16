using System.Linq;
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
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnInvalidForInvalidTag()
        {
            var element = "<amazon:effect name1=\"bla\" />".ToXElement();
            var strategy = new AmazonEffectStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.MissingAttribute, errors.First().State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttributeValue()
        {
            var element = "<amazon:effect name=\"InvalidContent\" />".ToXElement();
            var strategy = new AmazonEffectStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, errors.First().State);
        }
    }
}