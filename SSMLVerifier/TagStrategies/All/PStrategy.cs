using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.All
{
    public class PStrategy : BaseTagStrategy
    {
        public PStrategy() : base("p")
        {
        }

        public override VerificationResult Verify(XElement element)
        {
            if (element.HasAttributes)
            {
                return new VerificationResult(VerificationState.InvalidAttribute, $"The element with the tag {TagName} should not have any attributes.");
            }

            return VerificationResult.Valid;
        }
    }
}