using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;
using SSMLVerifier.Extensions;
using SSMLVerifier.TagStrategies.All;

namespace SSMLVerifierTests.TagStrategies.All
{
    [TestClass]
    public class ProsodyStrategyTests
    {
        [TestMethod]
        public void ReturnValidForValidTagRate()
        {
            var element = "<prosody rate=\"x-slow\">I speak quite slowly</prosody>.".ToXElement();
            var strategy = new ProsodyStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnValidForValidTagRateInPercent()
        {
            var element = "<prosody rate=\"23%\">I am slow</prosody>.".ToXElement();
            var strategy = new ProsodyStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnValidForValidTagVolume()
        {
            var element = "<prosody volume=\"silent\">I am silent</prosody>.".ToXElement();
            var strategy = new ProsodyStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnValidForValidTagVolumeInDb()
        {
            var element = "<prosody volume=\"-2.33dB\">I am silent</prosody>.".ToXElement();
            var strategy = new ProsodyStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnValidForValidTagPitchInPercent()
        {
            var element = "<prosody pitch=\"-23.45%\">I am low pitched</prosody>.".ToXElement();
            var strategy = new ProsodyStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnValidForValidTagPitch()
        {
            var element = "<prosody pitch=\"x-low\">I am low pitched</prosody>.".ToXElement();
            var strategy = new ProsodyStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnValidForValidTagPitchInSemitones()
        {
            var element = "<prosody pitch=\"+2st\">I am low pitched</prosody>.".ToXElement();
            var strategy = new ProsodyStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ReturnInvalidForInvalidAttribute()
        {
            var element = "<prosody name=\"test\"></prosody>".ToXElement();
            var strategy = new ProsodyStrategy();
            var errors = strategy.Verify(element);
            Assert.AreEqual(VerificationState.InvalidAttribute, errors.First().State);
        }
    }
}