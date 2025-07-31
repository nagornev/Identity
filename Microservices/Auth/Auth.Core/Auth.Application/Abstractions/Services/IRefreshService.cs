using Auth.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Abstractions.Services
{
    public interface IRefreshService
    {
        Task<AuthDto> RefreshAsync(string refreshToken, string newPublicKey, long timestamp, string signature, CancellationToken cancellation = default);
    }
}
