using System.Collections.Generic;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies
{
    public interface ITagStrategy
    {
        bool IsResponsibleFor(string tag);

        bool IsValidForPlatform(SsmlPlatform platform);

        IEnumerable<SSMLValidationError> Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All);
    }
}