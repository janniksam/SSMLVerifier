using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.All;

namespace SSMLVerifierTests.TagStrategies.All
{
    [TestClass]
    public class AudioStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<audio clipBegin=\"3s\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForMissingSrc()
        {
            var element = "<audio clipBegin=\"3s\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.MissingAttribute, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForValidRepeatCount()
        {
            var element = "<audio clipBegin=\"3s\" repeatCount=\"3\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForInvalidRepeatCount()
        {
            var element = "<audio clipBegin=\"3s\" repeatCount=\"3a\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }


        [TestMethod]
        public void ReturnValidForValidSpeed()
        {
            var element = "<audio clipBegin=\"3s\" speed=\"50%\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForInvalidSpeedWithoutPercentageSign()
        {
            var element = "<audio clipBegin=\"3s\" speed=\"50\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForInvalidSpeedWithMalformedPercentage()
        {
            var element = "<audio clipBegin=\"3s\" speed=\"50s\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForValidSoundLevel()
        {
            var element = "<audio clipBegin=\"3s\" soundLevel=\"+40dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForValidMinusSoundLevel()
        {
            var element = "<audio clipBegin=\"3s\" soundLevel=\"-39dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForInvalidSoundLevel()
        {
            var element = "<audio clipBegin=\"3s\" soundLevel=\"+40dBs\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForInvalidSoundLevelTooLow()
        {
            var element = "<audio clipBegin=\"3s\" soundLevel=\"-41dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForInvalidSoundLevelTooHigh()
        {
            var element = "<audio clipBegin=\"3s\" soundLevel=\"+41dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }
    }
}