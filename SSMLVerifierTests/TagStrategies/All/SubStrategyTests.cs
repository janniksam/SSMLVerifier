using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.All;

namespace SSMLVerifierTests.TagStrategies.All
{
    [TestClass]
    public class SubStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<sub><p></p></sub>".ToXElement();
            var strategy = new SubStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnValidForUsingOptionalAlias()
        {
            var element = "<sub alias=\"test\"><p></p></sub>".ToXElement();
            var strategy = new SubStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnInvalidvForInvalidAttribute()
        {
            var element = "<sub name=\"test\"></sub>".ToXElement();
            var strategy = new SubStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttribute, errors.First().State);
        }
    }
}