using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.Google;

namespace SSMLVerifierTests.TagStrategies.Google
{
    [TestClass]
    public class MediaStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<media xml:id=\"123\" repeatCount=\"3\" soundLevel=\"+2.28dB\" fadeInDur=\"2s\" fadeOutDur=\"0.2s\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithValidBeginTime()
        {
            var element = "<media begin=\"300.92ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidWithInvalidBeginTimeComma()
        {
            var element = "<media begin=\"300,92ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithBeginTimePlusValue()
        {
            var element = "<media begin=\"+30ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithBeginTimeMinusValue()
        {
            var element = "<media begin=\"-30ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithValidEndTime()
        {
            var element = "<media end=\"300.92ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidWithInvalidEndTimeComma()
        {
            var element = "<media end=\"300,92ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithEndTimePlusValue()
        {
            var element = "<media end=\"+30ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithEndTimeMinusValue()
        {
            var element = "<media end=\"-30ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithValidRepeatCount()
        {
            var element = "<media repeatCount=\"30\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidWithInvalidRepeatCount()
        {
            var element = "<media repeatCount=\"3s\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidWithInvalidRepeatCountMinusValue()
        {
            var element = "<media repeatCount=\"-3\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidWithInvalidRepeatCountPlusValue()
        {
            var element = "<media repeatCount=\"+3\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithValidRepeatDurTime()
        {
            var element = "<media repeatDur=\"300.92ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidWithInvalidRepeatDurTimeComma()
        {
            var element = "<media repeatDur=\"300,92ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithRepeatDurTimePlusValue()
        {
            var element = "<media repeatDur=\"+30ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithRepeatDurTimeMinusValue()
        {
            var element = "<media repeatDur=\"-30ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForValidSoundLevel()
        {
            var element = "<media soundLevel=\"+40dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForValidMinusSoundLevel()
        {
            var element = "<media soundLevel=\"-39dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidSoundLevel()
        {
            var element = "<media soundLevel=\"+40dBs\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForInvalidSoundLevelTooLow()
        {
            var element = "<media soundLevel=\"-41dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForInvalidSoundLevelTooHigh()
        {
            var element = "<media soundLevel=\"+41dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidSoundLevelTooMuchDecimals()
        {
            var element = "<media soundLevel=\"+30.003dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForSoundLevelWith2Decimals()
        {
            var element = "<media soundLevel=\"+30.03dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidForSoundLevelWith1Decimals()
        {
            var element = "<media soundLevel=\"+30.3dB\" src=\"http://test.com/test.mp3\" />".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithValidFadeInDurTime()
        {
            var element = "<media fadeInDur=\"300.92ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidWithInvalidFadeInDurTimeComma()
        {
            var element = "<media fadeInDur=\"300,92ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithFadeInDurTimePlusValue()
        {
            var element = "<media fadeInDur=\"+30ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithFadeInDurTimeMinusValue()
        {
            var element = "<media fadeInDur=\"-30ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithValidFadeOutDurTime()
        {
            var element = "<media fadeOutDur=\"300.92ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidWithInvalidFadeOutDurTimeComma()
        {
            var element = "<media fadeOutDur=\"300,92ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithFadeOutDurTimePlusValue()
        {
            var element = "<media fadeOutDur=\"+30ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnValidWithFadeOutDurTimeMinusValue()
        {
            var element = "<media fadeOutDur=\"-30ms\"/>".ToXElement();
            var strategy = new MediaStrategy();
            var verificationResult = strategy.Verify(element, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }
    }
}