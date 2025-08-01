namespace Auth.Application.Abstractions.Services
{
    public interface IRefreshService
    {
        Task<DTOs.AuthTokens> RefreshAsync(string refreshToken, string newPublicKey, long timestamp, string signature, string device, string ipAddress, CancellationToken cancellation = default);
    }
}
