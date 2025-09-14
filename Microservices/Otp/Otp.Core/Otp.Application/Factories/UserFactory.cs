using Otp.Application.Abstractions.Factories;
using Otp.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Factories
{
    public class UserFactory : IUserFactory
    {
        public User Create(Guid userId, string email)
        {
            return User.Create(userId, email);
        }
    }
}
