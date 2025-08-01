using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Exceptions.Domains
{
    public class UnconfirmedDomainException : DomainException
    {
        public UnconfirmedDomainException(string message) 
            : base(DomainErrorTypes.Unconfirmed, message)
        {
        }
    }
}
