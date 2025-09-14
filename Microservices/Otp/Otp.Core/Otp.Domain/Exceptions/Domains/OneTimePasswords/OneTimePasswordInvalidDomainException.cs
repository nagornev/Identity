using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Domain.Exceptions.Domains.OneTimePasswords
{
    public class OneTimePasswordInvalidDomainException : InvalidDomainException
    {
        private const string _message = "The one time password is invalid.";

        public OneTimePasswordInvalidDomainException() 
            : base(_message)
        {
        }
    }
}
