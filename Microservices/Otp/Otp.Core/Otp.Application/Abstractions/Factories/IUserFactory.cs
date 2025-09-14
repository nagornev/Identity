using Otp.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Abstractions.Factories
{
    public interface IUserFactory
    {
        User Create(Guid userId, string email);
    }
}
