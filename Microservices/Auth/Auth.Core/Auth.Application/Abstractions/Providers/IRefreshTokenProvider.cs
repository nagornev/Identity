using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Providers
{
    public interface IRefreshTokenProvider
    {
        /// <summary>
        /// Creates refresh token for <paramref name="user"/>.
        /// </summary>
        /// <param name="refreshKeyPair"></param>
        /// <param name="user"></param>
        /// <param name="session"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        string Create(KeyPairDto refreshKeyPair, User user, Session session, string publicKey);
    }
}
