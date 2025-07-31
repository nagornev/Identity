using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Domain.Exceptions.Domains.Otps
{
    public class SecretNullDomainException : NullDomainException
    {
        private const string _message = "The OTP secret cannot be null.";

        public SecretNullDomainException() 
            : base(_message)
        {
        }
    }
}
