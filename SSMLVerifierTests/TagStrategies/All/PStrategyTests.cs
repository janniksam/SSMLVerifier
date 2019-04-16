using System.Linq;
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
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttribute()
        {
            var element = "<p name=\"test\"></p>".ToXElement();
            var strategy = new PStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttribute, errors.First().State);
        }
    }
}