using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Factories.Keys
{
    public interface IKeyFactory
    {
        KeyPair Create();
    }
}
