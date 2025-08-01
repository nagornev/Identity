using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions.Applications.Sessions
{
    public class VersionInvalidApplicationException : InvalidApplicationException
    {
        private const string _message = "The session version is invalid.";

        public VersionInvalidApplicationException()
            : base(_message)
        {
        }
    }
}
