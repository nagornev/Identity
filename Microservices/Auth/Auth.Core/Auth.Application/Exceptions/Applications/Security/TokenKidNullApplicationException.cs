using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions.Applications.Security
{
    public class TokenKidNullApplicationException : NullApplicationException
    {
        public TokenKidNullApplicationException(string token) 
            : base($"The kid was not found for the token: {token}.")
        {
        }
    }
}
