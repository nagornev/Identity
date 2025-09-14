using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Exceptions.Domains.Sessions
{
    public class SessionAlreadyActivatedDomainException : AlreadyDomainException
    {
        private const string _message = "The session already activated.";

        public SessionAlreadyActivatedDomainException()
            : base(_message)
        {
        }
    }
}
