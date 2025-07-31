using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Domain.Exceptions.Domains
{
    public abstract class NullDomainException : DomainException
    {
        public NullDomainException(string message)
            : base(DomainErrorTypes.Null, message)
        {
        }
    }
}
