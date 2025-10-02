using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Services
{
    public interface IRefreshService
    {
        Task<TokenPair> RefreshAsync(string refreshToken, string newPublicKey, long timestamp, string signature, CancellationToken cancellation = default);
    }
}
