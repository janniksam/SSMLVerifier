﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SSMLVerifier.TagStrategies.Google
{
    public class SeqStrategy : BaseTagStrategy
    {
        public SeqStrategy() : base("seq")
        {
        }

        public override VerificationResult Verify(XElement element)
        {
            var validationResult = VerifyContainsOnlySpecificElements(element, new List<string>
            {
                "par", "seq", "media"
            });
            
            return validationResult ?? VerificationResult.Sucess;
        }
    }
}