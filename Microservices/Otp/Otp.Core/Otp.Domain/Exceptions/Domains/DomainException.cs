using Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Domain.Exceptions.Domains
{
    public abstract class DomainException : ResultException
    {
        public DomainException(int type, string message)
            : base(type, message)
        {
        }
    }
}
