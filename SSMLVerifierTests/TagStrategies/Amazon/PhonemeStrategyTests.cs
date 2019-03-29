﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.Amazon;

namespace SSMLVerifierTests.TagStrategies.Amazon
{
    [TestClass]
    public class PhonemeStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<phoneme alphabet=\"ipa\" ph=\"ˈpi.kæn\">pecan</phoneme>".ToXElement();
            var strategy = new PhonemeStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.Valid, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForMissingAttributeAlphabet()
        {
            var element = "<phoneme ph=\"ˈpi.kæn\">pecan</phoneme>".ToXElement();
            var strategy = new PhonemeStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.MissingAttribute, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForMissingAttributePh()
        {
            var element = "<phoneme alphabet=\"ipa\">pecan</phoneme>".ToXElement();
            var strategy = new PhonemeStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.MissingAttribute, verificationResult.State);
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttributeValue()
        {
            var element = "<phoneme alphabet=\"ipa2\" ph=\"ˈpi.kæn\">pecan</phoneme>".ToXElement();
            var strategy = new PhonemeStrategy();
            var verificationResult = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, verificationResult.State);
        }
    }
}