using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Providers
{
    public interface IAccessTokenProvider
    {
        /// <summary>
        /// Creates access token for <paramref name="user"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        string Create(KeyDto accessPrivateKey, User user, Session session);
    }
}
