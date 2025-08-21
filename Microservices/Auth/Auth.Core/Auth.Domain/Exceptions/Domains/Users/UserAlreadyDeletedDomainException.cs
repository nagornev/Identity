using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Exceptions.Domains.Users
{
    public class UserAlreadyDeletedDomainException : AlreadyDomainException
    {
        public UserAlreadyDeletedDomainException(Guid userId) 
            : base($"The user ({userId}) is already delete.")
        {
        }
    }
}
