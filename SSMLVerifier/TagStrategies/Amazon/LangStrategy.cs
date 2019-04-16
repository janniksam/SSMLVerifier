using System.Collections.Generic;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Amazon
{
    public class LangStrategy : BaseTagStrategy
    {
        private static readonly string[] m_validLocales =
        {
            "en-US", "en-GB", "en-IN", "en-AU", "en-CA", "de-DE", "es-ES", "it-IT", "ja-JP", "fr-FR"
        };
        
        public LangStrategy() : base("lang", SsmlPlatform.Amazon)
        {
        }

        public override IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var error = RequiresAttribute(element, "lang", "xml", a => VerifyValues(a, m_validLocales));
            if (error != null)
            {
                yield return error;
            }
        }
    }
}