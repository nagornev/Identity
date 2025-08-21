using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Exceptions.Applications.Security
{
    public class PendingEmailAddressVersionInvalidApplicationException : InvalidApplicationException
    {
        public PendingEmailAddressVersionInvalidApplicationException(Guid userId) 
            : base($"The pending email address version for user ({userId}) is invalid.")
        {
        }
    }
}
