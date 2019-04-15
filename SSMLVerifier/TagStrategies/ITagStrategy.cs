using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies
{
    public interface ITagStrategy
    {
        bool IsResponsibleFor(string tag);

        bool IsValidForPlatform(SsmlPlatform platform);

        VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All);
    }
}