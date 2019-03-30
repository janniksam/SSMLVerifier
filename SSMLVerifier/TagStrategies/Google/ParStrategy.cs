using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Google
{
    public class ParStrategy : BaseTagStrategy
    {
        public ParStrategy() : base("par")
        {
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var validationResult = VerifyContainsOnlySpecificElements(element, new List<string>
            {
                "par", "seq", "media"
            });
            
            return validationResult ?? VerificationResult.Valid;
        }
    }
}