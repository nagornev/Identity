using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Exceptions.Domains.Sessions
{
    public class SessionAlreadyClosedDomainException : AlreadyDomainException
    {
        private const string _message = "The session already closed.";

        public SessionAlreadyClosedDomainException() 
            : base(_message)
        {
        }
    }
}
