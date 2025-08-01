using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions.Applications.Tokens
{
    public class RefreshWindowInvalidApplicationException : InvalidApplicationException
    {
        private const string _message = "The refresh window is invalid.";

        public RefreshWindowInvalidApplicationException()
            : base(_message)
        {
        }
    }
}
