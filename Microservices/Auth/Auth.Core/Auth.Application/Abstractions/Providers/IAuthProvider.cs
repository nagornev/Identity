using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Providers
{
    public interface IAuthProvider
    {
        AuthDto Create(KeyPairDto accessKeyPair, KeyPairDto refreshKeyPair, User user, Session session, string publicKey);
    }
}
