using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.All;

namespace SSMLVerifierTests.TagStrategies.All
{
    [TestClass]
    public class BreakStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTag()
        {
            var element = "<break time=\"3s\" strength=\"weak\"/>".ToXElement();
            var strategy = new BreakStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnValidTimeAndStrengthNotSet()
        {
            var element = "<break></break>".ToXElement();
            var strategy = new BreakStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnInvalidUnknownAttribute()
        {
            var element = "<break strength=\"weak\" test=\"bla\"></break>".ToXElement();
            var strategy = new BreakStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttribute, errors.First().State);
        }

        [TestMethod]
        public void ReturnInvalidStengthNotKnown()
        {
            var element = "<break strength=\"superstrong\"></break>".ToXElement();
            var strategy = new BreakStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, errors.First().State);
        }

        [TestMethod]
        public void ReturnValidWithValidTime()
        {
            var element = "<break time=\"300.92ms\"/>".ToXElement();
            var strategy = new BreakStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnInvalidWithInvalidTimeComma()
        {
            var element = "<break time=\"300,92ms\"/>".ToXElement();
            var strategy = new BreakStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttributeValue, errors.First().State);
        }

        [TestMethod]
        public void ReturnValidWithTimePlusValue()
        {
            var element = "<break time=\"+30ms\"/>".ToXElement();
            var strategy = new BreakStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnValidWithTimeMinusValue()
        {
            var element = "<break time=\"-30ms\"/>".ToXElement();
            var strategy = new BreakStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }
    }
}