using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Factories
{
    public interface IKeysFactory
    {
        KeyPairDto Create(TimeSpan timeToLive);
    }
}
