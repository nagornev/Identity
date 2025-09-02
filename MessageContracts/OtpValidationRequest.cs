using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts
{
    public class OtpValidationRequest : IRequestMessageContract
    {
        public OtpValidationRequest(string token,
                                    string otp,
                                    string tag)
        {
            Token = token;
            Otp = otp;
            Tag = tag;
        }

        public string Token { get; }

        public string Otp { get; }

        public string Tag { get; }
    }
}
