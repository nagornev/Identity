using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Domain.Exceptions.Domains
{
    public abstract class AlreadyDomainEvent : DomainException
    {
        public AlreadyDomainEvent(string message) 
            : base(DomainErrorTypes.Already, message)
        {
        }
    }
}
