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
        public void ReturnInvalidForInvalidSoundLevel()
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

        [TestMethod]
        public void ReturnInvalidForInvalidSoundLevelTooMuchDecimals()
        {
            var element = "<audio clipBegin=\"3s\" soundLevel=\"+30.003dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForSoundLevelWith2Decimals()
        {
            var element = "<audio clipBegin=\"3s\" soundLevel=\"+30.03dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForSoundLevelWith1Decimals()
        {
            var element = "<audio clipBegin=\"3s\" soundLevel=\"+30.3dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithValidClipBegin()
        {
            var element = "<audio src=\"http://test.com/test.mp3\" clipBegin=\"300.92ms\"/>".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidWithInvalidClipBeginComma()
        {
            var element = "<audio src=\"http://test.com/test.mp3\" clipBegin=\"300,92ms\"/>".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithClipBeginPlusValue()
        {
            var element = "<audio src=\"http://test.com/test.mp3\" clipBegin=\"+30ms\"/>".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithClipBeginMinusValue()
        {
            var element = "<audio src=\"http://test.com/test.mp3\" clipBegin=\"-30ms\"/>".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithValidClipEnd()
        {
            var element = "<audio src=\"http://test.com/test.mp3\" clipEnd=\"300.92ms\"/>".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidWithInvalidClipEndComma()
        {
            var element = "<audio src=\"http://test.com/test.mp3\" clipEnd=\"300,92ms\"/>".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithClipEndPlusValue()
        {
            var element = "<audio src=\"http://test.com/test.mp3\" clipEnd=\"+30ms\"/>".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithClipEndMinusValue()
        {
            var element = "<audio src=\"http://test.com/test.mp3\" clipEnd=\"-30ms\"/>".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithValidRepeatDur()
        {
            var element = "<audio src=\"http://test.com/test.mp3\" repeatDur=\"300.92ms\"/>".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidWithInvalidRepeatDurComma()
        {
            var element = "<audio src=\"http://test.com/test.mp3\" repeatDur=\"300,92ms\"/>".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithRepeatDurPlusValue()
        {
            var element = "<audio src=\"http://test.com/test.mp3\" repeatDur=\"+30ms\"/>".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithRepeatDurMinusValue()
        {
            var element = "<audio src=\"http://test.com/test.mp3\" repeatDur=\"-30ms\"/>".ToXElement();
            var strategy = new AudioStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }
    }
}