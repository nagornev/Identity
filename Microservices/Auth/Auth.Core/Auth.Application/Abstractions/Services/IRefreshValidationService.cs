using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Services
{
    public interface IRefreshValidationService
    {
        void ValidateWindow(long timestamp, int window);

        RefreshTokenPayload ValidateToken(string refreshToken, KeyPair refreshValidationKey);

        void ValidateSession(Session session, RefreshTokenPayload refreshTokenPayload);

        void ValidateFingerprint(string refreshToken, string newPublicKey, long timestamp, string signature, Session session);
    }
}
