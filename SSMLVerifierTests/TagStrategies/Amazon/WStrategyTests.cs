using System.Linq;
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
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnInvalidForMissingAttribute()
        {
            var element = "<w>read</w>".ToXElement();
            var strategy = new WStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.MissingAttribute, errors.First().State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttributeValue()
        {
            var element = "<w role=\"amazon:UNKNOWN\">read</w>".ToXElement();
            var strategy = new WStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, errors.First().State);
        }
    }
}