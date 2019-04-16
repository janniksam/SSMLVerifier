using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.Amazon;

namespace SSMLVerifierTests.TagStrategies.Amazon
{
    [TestClass]
    public class LangStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<lang xml:lang=\"de-DE\" />".ToXElement();
            var strategy = new LangStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnInvalidForInvalidTag()
        {
            var element = "<lang lang=\"de-DE\" />".ToXElement();
            var strategy = new LangStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.MissingAttribute, errors.First().State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttributeValue()
        {
            var element = "<lang xml:lang=\"not-valid\" />".ToXElement();
            var strategy = new LangStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, errors.First().State);
        }
    }
}