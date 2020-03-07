using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.Amazon;

namespace SSMLVerifierTests.TagStrategies.Amazon
{
    [TestClass]
    public class AmazonEmotionStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<amazon:emotion name=\"excited\" intensity=\"high\"/>".ToXElement();
            var strategy = new AmazonEmotionStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnInvalidForInvalidTag()
        {
            var element = "<amazon:effect name1=\"bla\" />".ToXElement();
            var strategy = new AmazonEmotionStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.MissingAttribute, errors.First().State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidNameAttributeValue()
        {
            var element = "<amazon:effect name=\"InvalidContent\" intensity=\"high\" />".ToXElement();
            var strategy = new AmazonEmotionStrategy();
            var errors = strategy.Verify(element).ToList();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, errors[0].State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidIntensityAttributeValue()
        {
            var element = "<amazon:effect name=\"excited\" intensity=\"higherThanNormal\" />".ToXElement();
            var strategy = new AmazonEmotionStrategy();
            var errors = strategy.Verify(element).ToList();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, errors[0].State);
        }
    }
}