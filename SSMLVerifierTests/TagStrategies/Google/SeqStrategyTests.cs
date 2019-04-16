using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.Google;

namespace SSMLVerifierTests.TagStrategies.Google
{
    [TestClass]
    public class SeqStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<seq></seq>".ToXElement();
            var strategy = new SeqStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnValidForValidTagWithChildMedia()
        {
            var element = "<seq><media></media></seq>".ToXElement();
            var strategy = new SeqStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnValidForValidTagWithChildPar()
        {
            var element = "<seq><par></par></seq>".ToXElement();
            var strategy = new SeqStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnValidForValidTagWithChildAll()
        {
            var element = "<seq><seq></seq><par></par><media></media></seq>".ToXElement();
            var strategy = new SeqStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnInvalidForMissingAttribute()
        {
            var element = "<seq><media></media><invalidTag></invalidTag></seq>".ToXElement();
            var strategy = new SeqStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.ContainerContainsInvalidChilds, errors.First().State);
        }
    }
}