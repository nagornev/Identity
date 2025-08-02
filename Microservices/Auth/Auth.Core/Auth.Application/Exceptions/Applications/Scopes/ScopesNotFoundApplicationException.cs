using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions.Applications.Scopes
{
    public class ScopesNotFoundApplicationException : NotFoundApplicationException
    {
        public ScopesNotFoundApplicationException(string audience) 
            : base($"No scopes were found from '{audience}' audience.")
        {
        }
    }
}
