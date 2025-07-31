using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Providers
{
    public interface IAuthProvider
    {
        AuthDto Create(KeyDto accessPrivateKey, KeyDto refreshPrivateKey, User user, Session session, string publicKey);
    }
}
