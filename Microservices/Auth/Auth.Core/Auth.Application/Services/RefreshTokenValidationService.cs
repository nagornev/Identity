using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Security;

namespace Auth.Application.Services
{
    public class RefreshTokenValidationService : IRefreshTokenValidationService
    {
        private readonly IRefreshTokenValidator _refreshTokenValidator;

        private readonly IRefreshTokenPayloadProvider _refreshTokenPayloadProvider;

        public RefreshTokenValidationService(IRefreshTokenValidator refreshTokenValidator,
                                             IRefreshTokenPayloadProvider refreshTokenPayloadProvider)
        {
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenPayloadProvider = refreshTokenPayloadProvider;
        }

        public RefreshTokenPayload Validate(string refreshToken, KeyPair key)
        {
            if (!_refreshTokenValidator.Validate(refreshToken, key, out var claims))
                throw new RefreshTokenInvalidApplicationException();

            return _refreshTokenPayloadProvider.GetPayload(claims);
        }
    }
}
