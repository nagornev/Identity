using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Services
{
    public class RefreshValidationService : IRefreshValidationService
    {
        private readonly IWindowValidationService _windowValidationService;

        private readonly ISessionValidationService _sessionValidationService;

        private readonly IFingerprintValidationService _fingerprintValidationService;

        private readonly IRefreshTokenValidationService _refreshTokenValidationService;

        private readonly IFingerprintMessageProvider _fingerprintMessageProvider;

        public RefreshValidationService(IWindowValidationService windowValidationService,
                                        ISessionValidationService sessionValidationService,
                                        IFingerprintValidationService fingerprintValidationService,
                                        IRefreshTokenValidationService refreshTokenValidationService,
                                        IFingerprintMessageProvider fingerprintMessageProvider)
        {
            _windowValidationService = windowValidationService;
            _sessionValidationService = sessionValidationService;
            _fingerprintValidationService = fingerprintValidationService;
            _refreshTokenValidationService = refreshTokenValidationService;
            _fingerprintMessageProvider = fingerprintMessageProvider;
        }

        public void ValidateWindow(long timestamp, int window)
        {
            _windowValidationService.Validate(timestamp, window);
        }

        public void ValidateSession(Session session, RefreshTokenPayload refreshTokenPayload)
        {
            _sessionValidationService.Validate(session);
            _sessionValidationService.ValidateVersion(session, refreshTokenPayload.Version);
        }

        public void ValidateFingerprint(string refreshToken, string newPublicKey, long timestamp, string signature, Session session)
        {
            _fingerprintValidationService.Validate(new FingerprintValidationParameters(_fingerprintMessageProvider.GetMessage(refreshToken, newPublicKey, timestamp),
                                                                                       signature),
                                                   session.PublicKey);
        }

        public RefreshTokenPayload ValidateToken(string refreshToken, KeyPair refreshValidationKey)
        {
            return _refreshTokenValidationService.Validate(refreshToken, refreshValidationKey);
        }
    }
}
