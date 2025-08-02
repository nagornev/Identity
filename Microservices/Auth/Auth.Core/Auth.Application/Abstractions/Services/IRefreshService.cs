namespace Auth.Application.Abstractions.Services
{
    public interface IRefreshService
    {
        Task<DTOs.TokenPair> RefreshAsync(string refreshToken, string newPublicKey, long timestamp, string signature, string device, string ipAddress, CancellationToken cancellation = default);
    }
}
