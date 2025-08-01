using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Providers.Tokens
{
    public interface IAccessTokenProvider
    {
        /// <summary>
        /// Creates access token for <paramref name="user"/>.
        /// </summary>
        /// <param name="accessKeyPair"></param>
        /// <param name="user"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        string Create(KeyPair accessKeyPair, User user, Session session);
    }
}
