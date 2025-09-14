using Otp.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Abstractions.Services
{
    public interface IUserQueryService
    {
        Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellation = default);

    }
}
