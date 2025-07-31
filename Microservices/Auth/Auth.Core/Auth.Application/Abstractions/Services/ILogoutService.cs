using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Abstractions.Services
{
    public interface ILogoutService
    {
        Task LogoutAsync(Guid sessionId, CancellationToken cancellation = default);

        Task LogoutAllAsync(Guid userId, CancellationToken cancellation = default); 
    }
}
