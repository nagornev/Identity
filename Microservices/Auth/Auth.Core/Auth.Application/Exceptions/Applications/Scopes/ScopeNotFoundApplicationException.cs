using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions.Applications.Scopes
{
    public class ScopeNotFoundApplicationException : NotFoundApplicationException
    {
        public ScopeNotFoundApplicationException(Guid id) 
            : base($"No scopes were found with {id} id.")
        {
        }
    }
}
