﻿using System.Collections.Generic;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Google
{
    public class SeqStrategy : BaseTagStrategy
    {
        public SeqStrategy() : base("seq")
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