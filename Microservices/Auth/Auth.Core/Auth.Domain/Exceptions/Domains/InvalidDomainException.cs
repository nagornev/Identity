using OperationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Exceptions.Domains
{
    public abstract class InvalidDomainException : DomainException
    {
        protected InvalidDomainException(string message) 
            : base(ResultErrorTypes.Invalid, message)
        {
        }
    }
}
