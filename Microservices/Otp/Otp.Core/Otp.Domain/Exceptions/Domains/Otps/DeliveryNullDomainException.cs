using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Domain.Exceptions.Domains.Otps
{
    public class DeliveryNullDomainException : NullDomainException
    {
        private const string _message = "The OTP delivery cannot be null.";

        public DeliveryNullDomainException() 
            : base(_message)
        {
        }
    }
}
