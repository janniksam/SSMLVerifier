using System.Xml;
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

            var verify = m_verifier.Verify(testSsml, SsmlPlatform.Amazon);
            Assert.AreEqual(VerificationState.Valid, verify.State);
        }

        [TestMethod]
        public void ShouldReturnValidWithAnInvalidRoot()
        {
            const string testSsml = "<invalidTag></invalidTag>";

            var verify = m_verifier.Verify(testSsml);
            Assert.AreEqual(VerificationState.InvalidTag, verify.State);
        }

        [TestMethod]
        public void ShouldReturnValidWithOnlyAnInvalidPlatformSpecificTag()
        {
            const string testSsml = "<speak><lang xml:lang='de-DE'></lang></speak>";

            var verify = m_verifier.Verify(testSsml, SsmlPlatform.Google);
            Assert.AreEqual(VerificationState.InvalidTag, verify.State);
        }

        [TestMethod]
        public void ShouldReturnValidWithOnlyAValidPlatformSpecificTag()
        {
            const string testSsml = "<speak><lang xml:lang='de-DE'></lang></speak>";

            var verify = m_verifier.Verify(testSsml, SsmlPlatform.Amazon);
            Assert.AreEqual(VerificationState.Valid, verify.State);
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void ShouldReturnInvalidWithMalformedXml()
        {
            const string testSsml = "<speaks></speak>";
            m_verifier.Verify(testSsml);
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void ShouldReturnInvalidWithMalformedXml2()
        {
            const string testSsml = "speak>/speak>";
            m_verifier.Verify(testSsml);
        }
    }
}
