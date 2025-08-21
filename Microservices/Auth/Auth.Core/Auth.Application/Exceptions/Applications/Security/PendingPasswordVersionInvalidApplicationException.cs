using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions.Applications.Security
{
    public class PendingPasswordVersionInvalidApplicationException : InvalidApplicationException
    {
        public PendingPasswordVersionInvalidApplicationException(Guid userId) 
            : base($"The pending password version for user ({userId}) is invalid.")
        {
        }
    }
}
