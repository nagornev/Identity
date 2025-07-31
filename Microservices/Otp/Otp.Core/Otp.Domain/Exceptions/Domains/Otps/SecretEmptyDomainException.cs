using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Domain.Exceptions.Domains.Otps
{
    public class SecretEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The OTP secret cannot be emtpy.";

        public SecretEmptyDomainException() 
            : base(_message)
        {
        }
    }
}
