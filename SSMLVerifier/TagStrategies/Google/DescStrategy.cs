using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Google
{
    public class DescStrategy : BaseTagStrategy
    {
        private const string TagNameAudio = "audio";

        public DescStrategy() : base("desc")
        {
        }

        public override VerificationResult Verify(XElement element, SsmlPlatform platform = SsmlPlatform.All)
        {
            var verificationResult = VerifyHasValidParent(element, TagNameAudio);
            if (verificationResult != null)
            {
                return verificationResult;
            }
            return VerificationResult.Valid;
        }
    }
}