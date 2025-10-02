using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Exceptions.Domains.Sessions
{
    public class PublicKeyInvalidDomainException : InvalidDomainException
    {
        private const string _messsage = "The invalid public key.";

        public PublicKeyInvalidDomainException() 
            : base(_messsage)
        {
        }
    }
}
