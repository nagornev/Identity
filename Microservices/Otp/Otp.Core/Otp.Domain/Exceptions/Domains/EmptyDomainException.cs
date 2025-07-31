using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Domain.Exceptions.Domains
{
    public class EmptyDomainException : DomainException
    {
        public EmptyDomainException(string message)
            : base(DomainErrorTypes.Empty, message)
    {
    }
}
}
