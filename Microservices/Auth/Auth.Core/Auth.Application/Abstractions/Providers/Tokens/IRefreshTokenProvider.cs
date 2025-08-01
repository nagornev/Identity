using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Providers.Tokens
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
        string Create(KeyPair refreshKeyPair, User user, Session session, string publicKey);
    }
}
