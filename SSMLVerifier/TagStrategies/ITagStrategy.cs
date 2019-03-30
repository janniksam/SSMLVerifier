using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies
{
    public interface ITagStrategy
    {
        bool IsResponsibleFor(string tag);
        VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All);
    }
}