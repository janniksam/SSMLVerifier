using System.Linq;
using System.Security.Cryptography;
using System.Xml;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSMLVerifier;

namespace SSMLVerifierTests
{
    [TestClass]
    public class VerifierIntegrationTests
    {
        private Verifier m_verifier;

        [TestInitialize]
        public void Setup()
        {
            m_verifier =  new Verifier();
        }

        [TestMethod]
        public void ShouldReturnValidWithASpeakTagAndALangTagOnAmazonPlatform()
        {
            const string testSsml = "<speak>Hello <lang xml:lang='de-DE'>Welt</lang></speak>";

            var errors = m_verifier.Verify(testSsml, SsmlPlatform.Amazon);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ShouldReturnValidWithDesc()
        {
            const string testSsml = "<speak>Hello <audio src=\"test\"><desc>Description here</desc></audio></speak>";

            var errors = m_verifier.Verify(testSsml, SsmlPlatform.Google);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void ShouldReturnValidWithAnInvalidRoot()
        {
            const string testSsml = "<invalidTag></invalidTag>";

            var errors = m_verifier.Verify(testSsml);
            Assert.AreEqual(VerificationState.InvalidTag, errors.First().State);
        }

        [TestMethod]
        public void ShouldReturnValidWithOnlyAnInvalidPlatformSpecificTag()
        {
            const string testSsml = "<speak><lang xml:lang='de-DE'></lang></speak>";

            var errors = m_verifier.Verify(testSsml, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidTag, errors.First().State);
        }

        [TestMethod]
        public void ShouldReturnValidWithOnlyAValidPlatformSpecificTag()
        {
            const string testSsml = "<speak>" +
                                    "   <lang xml:lang='de-DE'></lang>" +
                                    "   <amazon:emotion name=\"excited\" intensity=\"high\" />" +
                                    "   <amazon:effect name=\"whispered\" />" +
                                    "   <amazon:domain name=\"news\" />" +
                                    "</speak>";

            var errors = m_verifier.Verify(testSsml, SsmlPlatform.Amazon);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void ShouldReturnInvalidWithMalformedXml()
        {
            const string testSsml = "<speaks></speak>";
            var errors = m_verifier.Verify(testSsml);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void ShouldReturnInvalidWithMalformedXml2()
        {
            const string testSsml = "speak>/speak>";
            var errors = m_verifier.Verify(testSsml);
            Assert.AreEqual(0, errors.Count());
        }


        [TestMethod]
        public void ShouldReturnTwoErrorsOfSameStrategy()
        {
            const string testSsml = "<speak>Hello <audio clipBegin=\"aaa\"><desc>Description here</desc></audio></speak>";

            var errors = m_verifier.Verify(testSsml, SsmlPlatform.Google);
            Assert.AreEqual(2, errors.Count());
            Assert.AreEqual(1, errors.Count(p => p.State == VerificationState.MissingAttribute));
            Assert.AreEqual(1, errors.Count(p => p.State == VerificationState.InvalidAttributeValue));
        }
    }
}
