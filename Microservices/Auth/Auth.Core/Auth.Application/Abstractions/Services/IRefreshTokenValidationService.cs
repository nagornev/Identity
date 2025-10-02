using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Services
{
    public interface IRefreshTokenValidationService
    {
        RefreshTokenPayload Validate(string refreshToken, KeyPair key);
    }
}
