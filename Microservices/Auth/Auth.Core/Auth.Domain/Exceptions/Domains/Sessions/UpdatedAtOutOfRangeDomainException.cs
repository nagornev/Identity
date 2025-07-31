using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Exceptions.Domains.Sessions
{
    public class UpdatedAtOutOfRangeDomainException : OutOfRangeDomainException
    {
        public UpdatedAtOutOfRangeDomainException(long updatedAt) 
            : base($"The session update time cannot be less than {updatedAt}.")
        {
        }
    }
}
