using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.All;

namespace SSMLVerifierTests.TagStrategies.All
{
    [TestClass]
    public class SStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<s><p></p></s>".ToXElement();
            var strategy = new SStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnInvalidvForInvalidAttribute()
        {
            var element = "<s name=\"test\"></s>".ToXElement();
            var strategy = new SStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttribute, errors.First().State);
        }
    }
}