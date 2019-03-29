using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Amazon
{
    public class LangStrategy : BaseTagStrategy
    {
        private static readonly string[] m_validLocales =
        {
            "en-US", "en-GB", "en-IN", "en-AU", "en-CA", "de-DE", "es-ES", "it-IT", "ja-JP", "fr-FR"
        };
        
        public LangStrategy() : base("lang")
        {
        }

        public override VerificationResult Verify(XElement element)
        {
            var verificationResult = RequiresAttribute(element, "lang", "xml", m_validLocales);
            if (verificationResult != null)
            {
                return verificationResult;
            }

            return VerificationResult.Sucess;
        }
    }
}