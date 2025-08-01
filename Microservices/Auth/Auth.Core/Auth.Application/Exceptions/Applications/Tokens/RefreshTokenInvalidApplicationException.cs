using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions.Applications.Tokens
{
    public class RefreshTokenInvalidApplicationException : InvalidApplicationException
    {
        private const string _message = "The refresh token is invalid.";

        public RefreshTokenInvalidApplicationException() 
            : base(_message)
        {
        }
    }
}
