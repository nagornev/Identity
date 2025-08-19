using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Providers
{
    public interface IRefreshTokenPayloadProvider : ITokenPayloadProvider<RefreshTokenPayload>
    {
    }
}
