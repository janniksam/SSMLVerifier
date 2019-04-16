using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.Google;

namespace SSMLVerifierTests.TagStrategies.Google
{
    [TestClass]
    public class DescStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<audio><desc></desc></audio>".ToXElement().Elements().First();
            var strategy = new DescStrategy();
            var errors = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnInvalidWithInvalidParent()
        {
            var element = "<par><desc></desc></par>".ToXElement().Elements().First();
            var strategy = new DescStrategy();
            var errors = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidParent, errors.First().State);
        }

        [TestMethod]
        public void ReturnInvalidWithNoParent()
        {
            var element = "<desc></desc>".ToXElement();
            var strategy = new DescStrategy();
            var errors = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidParent, errors.First().State);
        }
    }
}