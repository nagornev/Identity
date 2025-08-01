using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions.Applications.Tokens
{
    public class SignatureInvalidApplicationException : InvalidApplicationException
    {
        private const string _message = "The signature is invalid.";

        public SignatureInvalidApplicationException() 
            : base(_message)
        {
        }
    }
}
