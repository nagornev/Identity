using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Exceptions.Domains.Scopes
{
    public class AudienceNullDomainException : NullDomainException
    {
        private const string _message = "The audience can not be null.";

        public AudienceNullDomainException()
            : base(_message)
        {
        }
    }
}
