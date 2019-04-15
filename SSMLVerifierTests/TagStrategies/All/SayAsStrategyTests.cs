using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.All;

namespace SSMLVerifierTests.TagStrategies.All
{
    [TestClass]
    public class SayAsStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<say-as interpret-as='time' format='hms12'>2:30pm</say-as>".ToXElement();
            var strategy = new SayAsStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttribute()
        {
            var element = "<say-as test='123' interpret-as='time' format='hms12'>2:30pm</say-as>".ToXElement();
            var strategy = new SayAsStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttribute, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttributeValueInterpretAs()
        {
            var element = "<say-as interpret-as='test' format='hms12'>2:30pm</say-as>".ToXElement();
            var strategy = new SayAsStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttributeValueDetail()
        {
            var element = "<say-as interpret-as='test' format='hms12' detail='-3'>2:30pm</say-as>".ToXElement();
            var strategy = new SayAsStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForPlatformSpecificAttributeDetail()
        {
            var element = "<say-as interpret-as='test' format='hms12' detail='3'>2:30pm</say-as>".ToXElement();
            var strategy = new SayAsStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttribute, verificationResult.State);
        }


        [TestMethod]
        public void ReturnInvalidForPlatformSpecificValueInterpretAs()
        {
            var element = "<say-as interpret-as='bleep' format='hms12'>2:30pm</say-as>".ToXElement();
            var strategy = new SayAsStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Amazon);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForPlatformSpecificValueInterpretAs()
        {
            var element = "<say-as interpret-as='bleep' format='hms12'>2:30pm</say-as>".ToXElement();
            var strategy = new SayAsStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWhenInterpretAsIsDateAndFormatIsSet()
        {
            var element = "<say-as interpret-as='date' format='mdy'>2:30pm</say-as>".ToXElement();
            var strategy = new SayAsStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidWhenInterpretAsIsNotDateAndFormatIsSet()
        {
            var element = "<say-as interpret-as='unit' format='mdy'>2:30pm</say-as>".ToXElement();
            var strategy = new SayAsStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttribute, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWhenInterpretAsIsNotDateAndFormatIsNotSet()
        {
            var element = "<say-as interpret-as='unit'>2:30pm</say-as>".ToXElement();
            var strategy = new SayAsStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }
    }
}